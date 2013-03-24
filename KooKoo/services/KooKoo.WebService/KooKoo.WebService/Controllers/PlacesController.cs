using KooKoo.WebService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KooKoo.WebService.Controllers
{
    public class PlacesController : ApiController
    {

        private readonly IPlaceRepository m_placeRepo;

        public PlacesController() {

            //TODO: injection
            m_placeRepo = new PlaceRepository();
        }


        // GET api/values
        public IEnumerable<PlaceEntity> Get(
                double longitude,
                double latitude
            ) {

                return m_placeRepo.GetRadius(longitude, latitude, 4);

        }
    }
}
