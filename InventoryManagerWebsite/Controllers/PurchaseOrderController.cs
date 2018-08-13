using DataAccess.DTO;
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

        public ActionResult New()
        {
            var model = new AddPurchaseOrderViewModel
            {
                Suppliers = AutoMapper.Mapper.Map<List<SupplierModel>>(_supplierService.GetSuppliers(1)),
                Products = AutoMapper.Mapper.Map<List<ProductModel>>(_inventoryService.GetProductItems(null)),
            };

            return View("New", model);
        }

        public ActionResult Save(AddPurchaseOrderViewModel model)
        {
            InvoiceDto invoice;
            try
            {
                invoice = _invoiceService.SaveNewPurchaseOrder(model.SelectedSupplierId);
                model.InvoiceId = invoice.Id;
                foreach (var product in model.InvoiceProducts)
                {
                    try
                    {
                        _invoiceService.SaveNewPurchaseOrderProduct(invoice.Id, product.ProductId, product.TotalCost, product.OrderedQuantity);
                        product.IsSynced = true;
                    }
                    catch (Exception e)
                    {
                        product.IsSynced = false;
                        product.SyncMessage = e.Message;
                    }
                }
                if (model.InvoiceProducts.Any(x => x.IsSynced == false))
                {
                    throw new ApplicationException("Unable to save Invoice.");
                }
            }
            catch (Exception e)
            {
                TempData["Toast"] = e.Message;
                TempData["ToastType"] = "Error";
                model.Suppliers = AutoMapper.Mapper.Map<List<SupplierModel>>(_supplierService.GetSuppliers(1));
                model.Products = AutoMapper.Mapper.Map<List<ProductModel>>(_inventoryService.GetProductItems(null));
                return View("New", model);
            }

            TempData["ToastType"] = "Success";
            TempData["Toast"] = "Invoice Saved Successfully.";
            return Index();
        }
    }
}