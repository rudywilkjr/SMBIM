using System.Collections.Generic;
using System.Web.Mvc;
using DataAccess.DTO;

namespace InventoryManagerWebsite.Models.Report
{
    public class ProductReportModel
    {
        public IEnumerable<LocationsWithProductDto> Items { get; set; }

        public List<ProductDto> Products { get; set; }

        public int SelectedProductId { get; set; }

        public SelectList ProductsList => new SelectList(Products, "Id", "Name");

        public List<LocationDto> Locations { get; set; }

        public int SelectedLocationId { get; set; }

        public SelectList LocationsList => new SelectList(Locations, "Id", "Description");
    }
}