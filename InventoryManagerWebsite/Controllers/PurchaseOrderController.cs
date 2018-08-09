using InventoryManagerService.Inventory;
using InventoryManagerService.Invoice;
using InventoryManagerWebsite.Models.Invoice;
using InventoryManagerWebsite.Models.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagerWebsite.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly ProductService _inventoryService = new ProductService();
        private readonly InvoiceService _invoiceService = new InvoiceService();
        private readonly SupplierService _supplierService = new SupplierService();

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            var purchaseOrderViewModel = new PurchaseOrderViewModel
            {
                PurchaseOrders = AutoMapper.Mapper.Map<ICollection<InvoiceModel>>(_invoiceService.GetInvoices(string.Empty))
            };

            return View("View", purchaseOrderViewModel );
        }

        public ActionResult Add()
        {
            var model = new AddPurchaseOrderViewModel
            {
                Suppliers = AutoMapper.Mapper.Map<List<SupplierModel>>(_supplierService.GetSuppliers(1)),
                Products = AutoMapper.Mapper.Map<List<ProductModel>>(_inventoryService.GetProductItems(null)),
            };

            return View("Add", model);
        }

        public ActionResult Save(AddPurchaseOrderViewModel model)
        {
            return Index();
        }
    }
}