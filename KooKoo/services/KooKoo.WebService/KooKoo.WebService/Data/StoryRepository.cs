using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KooKoo.WebService.Data {

    internal class StoryRepository : IStoryRepository {

        private readonly CloudTable m_table;
        private readonly CloudBlobContainer m_blobContainer;

        public StoryRepository() {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                  CloudConfigurationManager.GetSetting("StorageConnectionString")
            );

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            m_table = tableClient.GetTableReference("stories");
            m_table.CreateIfNotExists();

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            m_blobContainer = blobClient.GetContainerReference("stories");
            if (!m_blobContainer.Exists()) {
                // configure for public access
                m_blobContainer.Create();
                var permissions = m_blobContainer.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                m_blobContainer.SetPermissions(permissions);
                
            }

        }

        IEnumerable<StoryEntity> IReadonlyRepository<StoryEntity>.GetAll() {

            TableQuery<StoryEntity> getAll = new TableQuery<StoryEntity>();

            return m_table.ExecuteQuery<StoryEntity>(getAll).OrderByDescending(s => s.CreationTime);
        }

        StoryEntity IReadonlyRepository<StoryEntity>.Get(Guid id) {

            TableOperation retrieveOperation = TableOperation.Retrieve<StoryEntity>("1", id.ToString());
            StoryEntity result = m_table.Execute(retrieveOperation).Result as StoryEntity;
            if (result == null) {
                throw new KeyNotFoundException(id.ToString());
            }
            return result;
        }

        void IRepository<StoryEntity>.Save(StoryEntity story) {
            if (story.Id == null) {
                story.Id = Guid.NewGuid();
                story.RowKey = story.Id.ToString();
                story.PartitionKey = "1";
                story.CreationTime = DateTimeOffset.UtcNow;
            }
            TableOperation addOpt = TableOperation.InsertOrMerge(story);
            m_table.Execute(addOpt);
        }

        void IRepository<StoryEntity>.Delete(StoryEntity story) {
            TableOperation addOpt = TableOperation.Delete(story);
            m_table.Execute(addOpt);
        }

        void IRepository<StoryEntity>.DeleteAll() {
            IStoryRepository @this = this;

            TableBatchOperation batchDelete = new TableBatchOperation();
            @this.GetAll().ToList().ForEach(e => batchDelete.Delete(e));
            if (batchDelete.Any()) {
                m_table.ExecuteBatch(batchDelete);
            }

        }

        void IStoryRepository.UploadImage(Guid storyId, Stream stream) {

            CloudBlockBlob blockBlob = m_blobContainer.GetBlockBlobReference(storyId.ToString());
            blockBlob.UploadFromStream(stream);
            IStoryRepository @this = this;
            StoryEntity s = @this.Get(storyId);
            s.HasImage = true;
            @this.Save(s);

        }

    }
}