using System;
using System.Collections.Generic;
using DataAccess.DTO;
using DataAccess.Repositories;

namespace InventoryManagerService.Inventory
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository = new ProductRepository();

        public List<ProductDto> GetProductItems(string searchText, bool includeDisabledProducts = false)
        {
            return _productRepository.GetProducts(searchText, includeDisabledProducts);
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
            if (String.IsNullOrEmpty(item.Name)) throw new ApplicationException("Invalid Item. Missing Name.");
            if (String.IsNullOrEmpty(item.Barcode)) throw new ApplicationException("Invalid Item. Missing Barcode.");
            return item.Id > 0 ? _productRepository.UpdateProduct(item) : _productRepository.CreateProduct(item);
        }

        public ProductDto CreateProductItem(ProductDto item)
        {
            return _productRepository.CreateProduct(item);
        }

        public void DeleteProductItem(int inventoryItemId)
        {
            _productRepository.DeleteProduct(inventoryItemId);
        }
    }
}
