using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerService.Interface
{
    public interface IProductService
    {
        List<ProductDto> GetProductItems(string searchText, bool includeDisabledProducts = false);
        ProductDto GetProductItem(int productItemId = 0, string itemBarcode = null);
        List<LocationsWithProductDto> GetLocationsWithProduct(int inventoryItemId, int locationId = 0);
        List<LocationsWithProductDto> GetAllLocationsWithProduct();
        ProductDto UpdateProductItem(ProductDto item);
        ProductDto CreateProductItem(ProductDto item);
        void DeleteProductItem(int inventoryItemId);

    }
}
