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
        public IEnumerable<Story> Get()
        {
            return m_storyRepo.GetAll();
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public void SampleData() {

            m_storyRepo.DeleteAll();

            Story s = new Story();
            s.StoryText = "this is sample story";

            Story s2 = new Story();
            s2.StoryText = "Man this is cold in here!";

            Story s3 = new Story();
            s3.StoryText = "Best lunch ever. I wish I could come here everyday for the rest of my life";

            Story s4 = new Story();
            s4.StoryText = "What is this crap anyway";

            m_storyRepo.Save(s);
            m_storyRepo.Save(s2);
            m_storyRepo.Save(s3);
            m_storyRepo.Save(s4);
        }

    }
}