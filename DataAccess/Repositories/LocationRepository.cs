using System.Collections.Generic;
using System.Linq;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class LocationRepository
    {
        public List<LocationDto> GetLocations()
        {
            List<Location> locationItems;
            using (var ctx = new StoreEntities())
            {
                locationItems = ctx.Locations.Include("LocationType").ToList();
            }

            return AutoMapper.Mapper.Map<List<LocationDto>>(locationItems);
        }

        public List<LocationDto> GetLocations(int type)
        {
            List<Location> locationItems;
            using (var ctx = new StoreEntities())
            {
                locationItems = ctx.Locations.Include("LocationType")
                    .Where(x => x.Type == type).ToList();
            }

            return AutoMapper.Mapper.Map<List<LocationDto>>(locationItems);
        }

        public List<LocationDto> GetExternalLocations()
        {
            List<Location> locationItems;
            using (var ctx = new StoreEntities())
            {
                locationItems = ctx.Locations.Include("LocationType")
                    .Where(x => x.LocationType.Description == "Supplier" || x.LocationType.Description == "Customer").ToList();
            }

            return AutoMapper.Mapper.Map<List<LocationDto>>(locationItems);
        } 

        public LocationDto GetLocation(int id)
        {
            Location locationItem;
            using (var ctx = new StoreEntities())
            {
                locationItem = ctx.Locations.Include("LocationType").Single(x => x.Id == id);
            }

            return AutoMapper.Mapper.Map<LocationDto>(locationItem);
        }

        public LocationDto CreateLocation(LocationDto location)
        {
            Location locationItem;
            using (var ctx = new StoreEntities())
            {
                locationItem = new Location
                {
                    Description = location.Description,
                    IsActive = location.IsActive
                };

                ctx.Locations.Add(locationItem);
                ctx.SaveChanges();
            }

            return AutoMapper.Mapper.Map<LocationDto>(locationItem);
        }

        public LocationDto UpdateLocation(LocationDto location)
        {
            Location locationItem;
            using (var ctx = new StoreEntities())
            {
                locationItem = ctx.Locations.Single(x => x.Id == location.Id);
                locationItem.Description = location.Description;

                ctx.SaveChanges();
            }

            return AutoMapper.Mapper.Map<LocationDto>(locationItem);
        }

        public bool DeleteLocation(int id)
        {
            using (var ctx = new StoreEntities())
            {
                var locationItem = ctx.Locations.Single(x => x.Id == id);
                ctx.Locations.Remove(locationItem);

                ctx.SaveChanges();
            }

            return true;
        }

        public List<LocationsWithProductDto> GetProductsAtLocation(int locationId)
        {
            List<LocationsWithProductDto> items;
            using (var ctx = new StoreEntities())
            {
                items = (from il in ctx.ProductLocations
                         join i in ctx.Products on il.InventoryId equals i.Id
                    join l in ctx.Locations on il.LocationId equals l.Id
                    where il.LocationId == locationId
                    select new LocationsWithProductDto
                    {
                        ProductId = il.InventoryId,
                        ProductName = i.Name,
                        LocationId = il.LocationId,
                        LocationDescription = l.Description,
                        QuantityOnHand = il.OnHand
                    }).ToList();
            }

            return items;
        }
    }
}
