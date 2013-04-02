using KooKoo.WebService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KooKoo.WebService.Controllers
{
    
    public class StoriesController : ApiController
    {
        private readonly IStoryRepository m_storyRepo;

        public StoriesController() {

            //TODO: injection
            m_storyRepo = new StoryRepository();
        }

        // GET api/values
        public IEnumerable<StoryEntity> Get()
        {

            IEnumerable<StoryEntity> all = m_storyRepo.GetAll();

            return m_storyRepo.GetAll().Take( 20 );
            
        }

        // POST api/values
        public StoryEntity Post(
                StoryEntity story
            ) {

            m_storyRepo.Save(story);
            return story;

        }

    }
}