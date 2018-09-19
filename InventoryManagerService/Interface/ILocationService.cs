using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerService.Interface
{
    public interface ILocationService
    {
        List<LocationDto> GetLocations();
        List<LocationDto> GetLocations(int locationType);
        List<LocationDto> GetExternalLocations();
        LocationDto GetLocation(int locationId = 0);
        List<LocationsWithProductDto> GetInventoryAtLocation(int locationId);
        LocationDto UpdateLocationItem(LocationDto location);
        LocationDto CreateLocationItem(LocationDto location);
        bool DeleteLocationItem(int locationId);

    }
}
