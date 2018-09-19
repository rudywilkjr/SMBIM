using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface ILocationRepository
    {
        List<LocationDto> GetLocations();
        List<LocationDto> GetLocations(int type);
        List<LocationDto> GetExternalLocations();
        LocationDto GetLocation(int id);
        LocationDto CreateLocation(LocationDto location);
        LocationDto UpdateLocation(LocationDto location);
        bool DeleteLocation(int id);
        List<LocationsWithProductDto> GetProductsAtLocation(int locationId);

    }
}
