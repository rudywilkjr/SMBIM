using System;
using System.Linq;
using System.Web.Mvc;
using InventoryManagerService.Interface;
using InventoryManagerWebsite.Models.Invoice;
using InventoryManagerWebsite.Models.Transfer;

namespace InventoryManagerWebsite.Controllers
{
    [RoutePrefix("Transfer")]
    public class TransferController : Controller
    {
        private readonly IProductService productService;
        private readonly ILocationService locationService;
        private readonly ITransferService transferService;
        private readonly IInvoiceService invoiceService;

        public TransferController(IProductService productService, ILocationService locationService, ITransferService transferService, IInvoiceService invoiceService)
        {
            this.productService = productService;
            this.locationService = locationService;
            this.transferService = transferService;
            this.invoiceService = invoiceService;
        }

        [Route("Move/{locationId=locationId}/{inventoryItemId=inventoryItemId}")]
        public ActionResult Move(int inventoryItemId, int locationId)
        {
            var model = new MoveModel
            {
                ItemsAtLocation =
                    productService.GetLocationsWithProduct(inventoryItemId)
                        .Where(x => x.LocationId == locationId)
                        .ToList(),
                Locations = locationService.GetLocations(1), //1:Internal
                SelectedDepartureLocationId = locationId,
                SelectedInventoryId = inventoryItemId
            };

            return View("Move", model);
        }

        [Route("Ship/{locationId=locationId}/{inventoryItemId=inventoryItemId}")]
        public ActionResult Ship(int inventoryItemId, int locationId)
        {
            var model = new MoveModel
            {
                ItemsAtLocation =
                    productService.GetLocationsWithProduct(inventoryItemId)
                        .Where(x => x.LocationId == locationId)
                        .ToList(),
                Locations = locationService.GetLocations(),
                SelectedDepartureLocationId = locationId,
                SelectedInventoryId = inventoryItemId,
                SelectedArrivingLocationId = 4 //4: Shipped
            };

            return View("Move", model);
        }

        [Route("Search")]
        public ActionResult Search(SearchInputModel model)
        {
            var viewModel = new InvoiceSearchViewModel
            {
                Invoices = invoiceService.GetInvoices(model.InvoiceSearchText)
            };
            
            return View("Search", viewModel);
        }

        [Route("Invoice/{invoiceId}")]
        public ActionResult Invoice(int invoiceId)
        {
            var invoice = invoiceService.GetInvoice(invoiceId);
            var model = new ReceiveInvoiceViewModel
            {
                SelectedInvoice = AutoMapper.Mapper.Map<InvoiceModel>(invoice),
                Locations = locationService.GetLocations(3),
            };
            return View("Invoice", model);
        }

        [Route("Receive")]
        public ActionResult Receive(ReceiveInvoiceViewModel model)
        {
            foreach(var product in model.SelectedInvoice.InvoiceProducts)
            {
                try
                {
                    if (product.ReceivedQuantity != invoiceService.GetInvoiceProduct(product.Id).ReceivedQuantity)
                    {
                        transferService.ReceiveInvoiceProduct(product.Id, model.SelectedInvoice.InvoiceType == "Purchase Order" ? 4 : 5, model.SelectedLocationId, product.ReceivedQuantity);
                        product.IsSynced = true;
                        product.SyncMessage = "Update saved successfully.";
                    }
                }
                catch (Exception e)
                {
                    product.IsSynced = false;
                    product.SyncMessage = e.Message;
                }
            }
            
            if (!model.SelectedInvoice.InvoiceProducts.Any(x => x.IsSynced == false))
            {
                TempData["ToastType"] = "Success";
                TempData["Toast"] = "Invoice ID " + model.SelectedInvoice.Id + " Saved Successfully.";
            } else
            {
                TempData["Toast"] = "Invoice ID " + model.SelectedInvoice.Id + " Not Saved Successfully.";
                TempData["ToastType"] = "Error";
            }


            model.Locations = locationService.GetLocations(3);
            return View("Invoice", model);
        }

        [HttpPost]
        [Route("SendTransfer")]
        public ActionResult SendTransfer(MoveModel model)
        {
            try
            {
                //model.Locations = _locationService.GetLocations();
                //model.TransferComplete = _transferService.TransferOut(model.SelectedInventoryId, model.SelectedDepartureLocationId,
                //    model.SelectedArrivingLocationId, model.SelectedQuantity);

                //if (!model.TransferComplete.GetValueOrDefault())
                //{
                //    ModelState.AddModelError("GENERALERR-1", "Transfer Incomplete. General Error");
                //}

                //model.ItemsAtLocation =
                //    _inventoryService.GetAllLocationsWithProduct()
                //        .ToList();
                //model.SelectedQuantity = 0;
                return View("Move", model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("CAUGHTERR-1", e.Message);
                model.TransferComplete = false;
                model.ItemsAtLocation =
                    productService.GetLocationsWithProduct(model.SelectedInventoryId)
                        .Where(x => x.LocationId == model.SelectedDepartureLocationId)
                        .ToList();
                return View("Move", model);
            }

        }

        [HttpPost]
        [Route("ReceiveProduct")]
        public ActionResult ReceiveProduct(ReceiveModel model)
        {
            try
            {
                model.DestinationLocations = locationService.GetLocations(1); //1: Internal
                model.SourceLocations = locationService.GetExternalLocations();
                model.Products = productService.GetProductItems(string.Empty);
                //model.TransferComplete = _transferService.TransferOut(model.SelectedProductId, 
                //    model.SelectedSourceLocationId, model.SelectedDestinationLocationId, model.SelectedQuantity);

                if (!model.TransferComplete.GetValueOrDefault())
                {
                    ModelState.AddModelError("GENERALERR-1", "Receipt Incomplete. General Error");
                }

                model.SelectedQuantity = 0;
                return View("Receive", model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("CAUGHTERR-1", e.Message);
                model.TransferComplete = false;
                return View("Receive", model);
            }

        }
    }
}