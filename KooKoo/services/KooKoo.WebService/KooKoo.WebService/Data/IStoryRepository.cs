using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooKoo.WebService.Data {

    public interface IStoryRepository : IRepository<StoryEntity> {

        void UploadImage(Guid storyId, Stream stream);
    }
}
