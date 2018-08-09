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
        private readonly InvoiceService _invoiceService = new InvoiceService();

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            var purchaseOrderViewModel = new PurchaseOrderViewModel
            {
                PurchaseOrders = AutoMapper.Mapper.Map<ICollection<InvoiceModel>>(_invoiceService.GetInvoices(string.Empty))
            };

            return View("View", purchaseOrderViewModel );
        }
    }
}