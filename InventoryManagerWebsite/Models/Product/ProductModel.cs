using System.Collections.Generic;
using DataAccess.DTO;

namespace InventoryManagerWebsite.Models.Product
{
    public class ProductModel
    {
        public ProductDto ProductItem { get; set; }

        public string LookupBarcode { get; set; }

        public string LookupItemName { get; set; }

        public List<LocationsWithProductDto> Locations { get; set; }

        public bool? SaveSuccessful { get; set; }
    }

    public class ProductLookupModel
    {
        public List<ProductDto> ProductItems { get; set; }

        public string SearchText { get; set; }

    }
}