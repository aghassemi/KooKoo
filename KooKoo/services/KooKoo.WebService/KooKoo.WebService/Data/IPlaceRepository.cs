using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooKoo.WebService.Data {

    interface IPlaceRepository : IReadonlyRepository<PlaceEntity> {

        IEnumerable<PlaceEntity> GetRadius(double longitude, double latitude, double radius);

    }
}
