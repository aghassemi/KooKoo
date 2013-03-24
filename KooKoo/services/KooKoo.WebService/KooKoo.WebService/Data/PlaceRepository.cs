using KooKoo.WebService.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KooKoo.WebService.Data {

    internal class PlaceRepository : IPlaceRepository {

        IEnumerable<PlaceEntity> IReadonlyRepository<PlaceEntity>.GetAll() {
            throw new NotSupportedException();
        }

        PlaceEntity IReadonlyRepository<PlaceEntity>.Get(Guid id) {
            throw new NotImplementedException();
        }

        IEnumerable<PlaceEntity> IPlaceRepository.GetRadius(double longitude, double latitude, double radius) {
            yield return new PlaceEntity() {
                Id = Guid.NewGuid(),
                PlaceName = "Pizza Pizza",
                Longitude = longitude,
                Latitude = latitude
            };

            yield return new PlaceEntity() {
                Id = Guid.NewGuid(),
                PlaceName = "Hogtown Bar and Grild",
                Longitude = longitude,
                Latitude = latitude
            };

            yield return new PlaceEntity() {
                Id = Guid.NewGuid(),
                PlaceName = "Starbucks",
                Longitude = longitude,
                Latitude = latitude
            };

            yield return new PlaceEntity() {
                Id = Guid.NewGuid(),
                PlaceName = "King and Spadian Intersection",
                Longitude = longitude,
                Latitude = latitude
            };
        }
    }
}