using DataAccess.DTO;
using InventoryManagerService.Interface;
using InventoryManagerWebsite.Models.Invoice;
using InventoryManagerWebsite.Models.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagerWebsite.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IProductService productService;
        private readonly IInvoiceService invoiceService;
        private readonly ISupplierService supplierService;

        public PurchaseOrderController(IProductService productService, IInvoiceService invoiceService, ISupplierService supplierService)
        {
            this.productService = productService;
            this.invoiceService = invoiceService;
            this.supplierService = supplierService;
        }

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            var purchaseOrderViewModel = new PurchaseOrderViewModel
            {
                PurchaseOrders = AutoMapper.Mapper.Map<ICollection<InvoiceModel>>(invoiceService.GetInvoices(string.Empty))
            };

            return View("View", purchaseOrderViewModel );
        }

        public ActionResult New()
        {
            var model = new AddPurchaseOrderViewModel
            {
                Suppliers = AutoMapper.Mapper.Map<List<SupplierModel>>(supplierService.GetSuppliers(1)),
                Products = AutoMapper.Mapper.Map<List<ProductModel>>(productService.GetProductItems(null)),
            };

            return View("New", model);
        }

        public ActionResult Save(AddPurchaseOrderViewModel model)
        {
            InvoiceDto invoice;
            try
            {
                invoice = invoiceService.SaveNewPurchaseOrder(model.SelectedSupplierId);
                model.InvoiceId = invoice.Id;
                foreach (var product in model.InvoiceProducts)
                {
                    try
                    {
                        invoiceService.SaveNewPurchaseOrderProduct(invoice.Id, product.ProductId, product.TotalCost, product.OrderedQuantity);
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
                model.Suppliers = AutoMapper.Mapper.Map<List<SupplierModel>>(supplierService.GetSuppliers(1));
                model.Products = AutoMapper.Mapper.Map<List<ProductModel>>(productService.GetProductItems(null));
                return View("New", model);
            }

            TempData["ToastType"] = "Success";
            TempData["Toast"] = "Invoice Saved Successfully.";
            return Index();
        }
    }
}