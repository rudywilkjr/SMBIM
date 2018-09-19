using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DTO;
using DataAccess.Interface;
using DataAccess.Repositories;
using InventoryManagerService.Interface;

namespace InventoryManagerService.Location
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public List<LocationDto> GetLocations()
        {
            return locationRepository.GetLocations();
        }

        public List<LocationDto> GetLocations(int locationType)
        {
            return locationRepository.GetLocations(locationType);
        }

        public List<LocationDto> GetExternalLocations()
        {
            return locationRepository.GetExternalLocations();
        }

        public LocationDto GetLocation(int locationId = 0)
        {
            if (locationId > 0)
            {
                return locationRepository.GetLocation(locationId);
            }

            throw new ApplicationException("Unable to retrieve location. Missing location identifier");

        }

        public List<LocationsWithProductDto> GetInventoryAtLocation(int locationId)
        {
            return locationRepository.GetProductsAtLocation(locationId);
        }

        public LocationDto UpdateLocationItem(LocationDto location)
        {
            return locationRepository.UpdateLocation(location);
        }

        public LocationDto CreateLocationItem(LocationDto location)
        {
            return locationRepository.CreateLocation(location);
        }

        public bool DeleteLocationItem(int locationId)
        {
            return locationRepository.DeleteLocation(locationId);
        }
    }
}
