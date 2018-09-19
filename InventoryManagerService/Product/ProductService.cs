using System;
using System.Collections.Generic;
using DataAccess.DTO;
using DataAccess.Interface;
using DataAccess.Repositories;
using InventoryManagerService.Interface;

namespace InventoryManagerService.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<ProductDto> GetProductItems(string searchText, bool includeDisabledProducts = false)
        {
            return productRepository.GetProducts(searchText, includeDisabledProducts);
        }

        public ProductDto GetProductItem(int productItemId = 0, string itemBarcode = null)
        {
            if (productItemId > 0)
            {
                return productRepository.GetProduct(productItemId);
            }
            if (itemBarcode != null)
            {
                return productRepository.GetProduct(itemBarcode);
            }

            throw new ApplicationException("Unable to retrieve inventory item. Missing item identifier");
            
        }

        public List<LocationsWithProductDto> GetLocationsWithProduct(int inventoryItemId, int locationId = 0)
        {
            return productRepository.GetInternalLocationsWithProduct(inventoryItemId, locationId);
        }

        public List<LocationsWithProductDto> GetAllLocationsWithProduct()
        {
            return productRepository.GetInternalLocationsWithProduct();
        }

        public ProductDto UpdateProductItem(ProductDto item)
        {
            if (String.IsNullOrEmpty(item.Name)) throw new ApplicationException("Invalid Item. Missing Name.");
            if (String.IsNullOrEmpty(item.Barcode)) throw new ApplicationException("Invalid Item. Missing Barcode.");
            return item.Id > 0 ? productRepository.UpdateProduct(item) : productRepository.CreateProduct(item);
        }

        public ProductDto CreateProductItem(ProductDto item)
        {
            return productRepository.CreateProduct(item);
        }

        public void DeleteProductItem(int inventoryItemId)
        {
            productRepository.DeleteProduct(inventoryItemId);
        }
    }
}
