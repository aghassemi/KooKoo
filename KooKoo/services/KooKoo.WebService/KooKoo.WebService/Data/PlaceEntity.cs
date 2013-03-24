using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KooKoo.WebService.Data
{
    public class PlaceEntity {

        public Guid? Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PlaceName { get; set; }

    }
}
