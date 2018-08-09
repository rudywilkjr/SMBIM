using InventoryManagerWebsite.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerWebsite.Models.PurchaseOrder
{
    public class AddPurchaseOrderViewModel
    {
        public int SelectedSupplierId { get; set; }

        public ICollection<SupplierModel> Suppliers { get; set; }

        public SelectList SuppliersList => new SelectList(Suppliers, "Id", "Name");

        public int SelectedProductId { get; set; }

        public ICollection<ProductModel> Products { get; set; }

        public SelectList ProductList => new SelectList(Products, "Id", "Name");

        public ICollection<InvoiceProductModel> InvoiceProducts { get; set; }

        public decimal? TotalCost { get; set; }

        public short? Quantity { get; set; }
    }

    public class SupplierModel
    {
        public short Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}