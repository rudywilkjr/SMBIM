using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IProductRepository
    {
        List<ProductDto> GetProducts(string searchText, bool includeDisabledProducts);
        ProductDto GetProduct(int id);
        ProductDto GetProduct(string barcode);
        ProductDto CreateProduct(ProductDto product);
        ProductDto UpdateProduct(ProductDto product);
        void DeleteProduct(int id);
        List<LocationsWithProductDto> GetInternalLocationsWithProduct(int inventoryId = 0, int locationId = 0);
        List<LocationsWithProductDto> GetInternalAndExternalLocationsWithProduct(int productId = 0, int locationId = 0);

    }
}
