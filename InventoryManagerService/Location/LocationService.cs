using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DTO;
using DataAccess.Repositories;

namespace InventoryManagerService.Location
{
    public class LocationService
    {
        private readonly LocationRepository _locationRepository = new LocationRepository();

        public List<LocationDto> GetLocations()
        {
            return _locationRepository.GetLocations();
        }

        public List<LocationDto> GetLocations(int locationType)
        {
            return _locationRepository.GetLocations(locationType);
        }

        public List<LocationDto> GetExternalLocations()
        {
            return _locationRepository.GetExternalLocations();
        }

        public LocationDto GetLocation(int locationId = 0)
        {
            if (locationId > 0)
            {
                return _locationRepository.GetLocation(locationId);
            }

            throw new ApplicationException("Unable to retrieve location. Missing location identifier");

        }

        public List<LocationsWithProductDto> GetInventoryAtLocation(int locationId)
        {
            return _locationRepository.GetProductsAtLocation(locationId);
        }

        public LocationDto UpdateLocationItem(LocationDto location)
        {
            return _locationRepository.UpdateLocation(location);
        }

        public LocationDto CreateLocationItem(LocationDto location)
        {
            return _locationRepository.CreateLocation(location);
        }

        public bool DeleteLocationItem(int locationId)
        {
            return _locationRepository.DeleteLocation(locationId);
        }
    }
}
