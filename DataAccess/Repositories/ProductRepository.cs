using System.Collections.Generic;
using System.Linq;
using DataAccess.DTO;
using DataAccess.Interface;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public List<ProductDto> GetProducts(string searchText, bool includeDisabledProducts)
        {
            List<Product> productItems;
            using (var ctx = new StoreEntities())
            {
                var query = ctx.Products.AsQueryable();

                if (!includeDisabledProducts)
                {
                    query = query.Where(x => x.IsActive == true);
                }
                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(x => x.Name.Contains(searchText) || x.Barcode.Contains(searchText));
                }

                productItems = query.ToList();
            }

            return AutoMapper.Mapper.Map<List<ProductDto>>(productItems);
        }

        public ProductDto GetProduct(int id)
        {
            Product productItem;
            using (var ctx = new StoreEntities())
            {
                productItem = ctx.Products.Single(x => x.Id == id);
            }

            return AutoMapper.Mapper.Map<ProductDto>(productItem);
        }

        public ProductDto GetProduct(string barcode)
        {
            Product productItem;
            using (var ctx = new StoreEntities())
            {
                productItem = ctx.Products.Single(x => x.Barcode == barcode);
            }

            return AutoMapper.Mapper.Map<ProductDto>(productItem);
        }

        public ProductDto CreateProduct(ProductDto product)
        {
            Product productItem;
            using (var ctx = new StoreEntities())
            {
                productItem = new Product
                {
                    Barcode = product.Barcode,
                    Name = product.Name,
                    Weight = product.Weight,
                    IsActive = true
                };

                ctx.Products.Add(productItem);
                ctx.SaveChanges();
            }

            return AutoMapper.Mapper.Map<ProductDto>(productItem);
        }

        public ProductDto UpdateProduct(ProductDto product)
        {
            Product productItem;
            using (var ctx = new StoreEntities())
            {
                productItem = ctx.Products.Single(x => x.Id == product.Id);
                productItem.Name = product.Name;
                productItem.Weight = product.Weight;
                productItem.Barcode = product.Barcode;

                ctx.SaveChanges();
            }

            return AutoMapper.Mapper.Map<ProductDto>(productItem);
        }

        public void DeleteProduct(int id)
        {
            using (var ctx = new StoreEntities())
            {
                var productItem = ctx.Products.Single(x => x.Id == id);
                productItem.IsActive = false;

                ctx.SaveChanges();
            }
        }

        public List<LocationsWithProductDto> GetInternalLocationsWithProduct(int inventoryId = 0, int locationId = 0)
        {
            List<LocationsWithProductDto> items;
            using (var ctx = new StoreEntities())
            {
                var query = (from il in ctx.ProductLocations
                         join i in ctx.Products on il.ProductId equals i.Id
                         join l in ctx.Locations on il.LocationId equals l.Id
                         where l.LocationType.Description == "Warehouse"
                         select new LocationsWithProductDto
                         {
                             ProductId = il.ProductId,
                             ProductName = i.Name,
                             LocationId = il.LocationId,
                             LocationDescription = l.Description,
                             QuantityOnHand = il.OnHand
                         });

                if (inventoryId > 0)
                {
                    query = query.Where(x => x.ProductId == inventoryId);
                }
                if (locationId > 0)
                {
                    query = query.Where(x => x.LocationId == locationId);
                }
                items = query.ToList();
            }

            return items;
        }

        public List<LocationsWithProductDto> GetInternalAndExternalLocationsWithProduct(int productId = 0, int locationId = 0)
        {
            List<LocationsWithProductDto> items;
            using (var ctx = new StoreEntities())
            {
                var query = (from il in ctx.ProductLocations
                             join i in ctx.Products on il.ProductId equals i.Id
                             join l in ctx.Locations on il.LocationId equals l.Id
                             select new LocationsWithProductDto
                             {
                                 ProductId = il.ProductId,
                                 ProductName = i.Name,
                                 LocationId = il.LocationId,
                                 LocationDescription = l.Description,
                                 QuantityOnHand = il.OnHand
                             });

                if (productId > 0)
                {
                    query = query.Where(x => x.ProductId == productId);
                }
                if (locationId > 0)
                {
                    query = query.Where(x => x.LocationId == locationId);
                }
                items = query.ToList();
            }

            return items;
        }

    }
}
