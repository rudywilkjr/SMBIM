using System;
using System.Collections.Generic;
using DataAccess.DTO;
using DataAccess.Repositories;

namespace InventoryManagerService.Inventory
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository = new ProductRepository();

        public List<ProductDto> GetProductItems(string searchText)
        {
            return _productRepository.GetProducts(searchText);
        }

        public ProductDto GetProductItem(int productItemId = 0, string itemBarcode = null)
        {
            if (productItemId > 0)
            {
                return _productRepository.GetProduct(productItemId);
            }
            if (itemBarcode != null)
            {
                return _productRepository.GetProduct(itemBarcode);
            }

            throw new ApplicationException("Unable to retrieve inventory item. Missing item identifier");
            
        }

        public List<LocationsWithProductDto> GetLocationsWithProduct(int inventoryItemId, int locationId = 0)
        {
            return _productRepository.GetInternalLocationsWithProduct(inventoryItemId, locationId);
        }

        public List<LocationsWithProductDto> GetAllLocationsWithProduct()
        {
            return _productRepository.GetInternalLocationsWithProduct();
        }

        public ProductDto UpdateProductItem(ProductDto item)
        {
            return item.Id > 0 ? _productRepository.UpdateProduct(item) : _productRepository.CreateProduct(item);
        }

        public ProductDto CreateProductItem(ProductDto item)
        {
            return _productRepository.CreateProduct(item);
        }

        public bool DeleteProductItem(int inventoryItemId)
        {
            return _productRepository.DeleteProduct(inventoryItemId);
        }
    }
}
