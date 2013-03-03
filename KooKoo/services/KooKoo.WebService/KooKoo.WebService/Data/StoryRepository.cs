using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KooKoo.WebService.Data {

    internal class StoryRepository : IStoryRepository {

        private readonly CloudTable m_table;

        public StoryRepository() {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                  CloudConfigurationManager.GetSetting("StorageConnectionString")
            );

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            m_table = tableClient.GetTableReference("stories");
            m_table.CreateIfNotExists();

        }

        IEnumerable<Story> IRepository<Story>.GetAll() {

            TableQuery<Story> getAll = new TableQuery<Story>();

            return m_table.ExecuteQuery<Story>(getAll);
        }

        void IRepository<Story>.Save(Story story) {
            TableOperation addOpt = TableOperation.InsertOrMerge(story);
            m_table.Execute(addOpt);
        }

        void IRepository<Story>.Delete(Story story) {
            TableOperation addOpt = TableOperation.Delete(story);
            m_table.Execute(addOpt);
        }

        void IRepository<Story>.DeleteAll() {
            IStoryRepository @this = this;

            TableBatchOperation batchDelete = new TableBatchOperation();
            @this.GetAll().ToList().ForEach(e => batchDelete.Delete(e));
            if (batchDelete.Any()) {
                m_table.ExecuteBatch(batchDelete);
            }

        }
    }
}