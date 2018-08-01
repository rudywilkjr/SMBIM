using System;
using System.Linq;
using System.Web.Mvc;
using InventoryManagerService.Inventory;
using InventoryManagerService.Location;
using InventoryManagerService.Transfer;
using InventoryManagerWebsite.Models.Transfer;

namespace InventoryManagerWebsite.Controllers
{
    [RoutePrefix("Transfer")]
    public class TransferController : Controller
    {
        private readonly ProductService _inventoryService = new ProductService();
        private readonly LocationService _locationService = new LocationService();
        private readonly TransferService _transferService = new TransferService();

        [Route("Move/{locationId=locationId}/{inventoryItemId=inventoryItemId}")]
        public ActionResult Move(int inventoryItemId, int locationId)
        {
            var model = new MoveModel
            {
                ItemsAtLocation =
                    _inventoryService.GetLocationsWithProduct(inventoryItemId)
                        .Where(x => x.LocationId == locationId)
                        .ToList(),
                Locations = _locationService.GetLocations(1), //1:Internal
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
                    _inventoryService.GetLocationsWithProduct(inventoryItemId)
                        .Where(x => x.LocationId == locationId)
                        .ToList(),
                Locations = _locationService.GetLocations(),
                SelectedDepartureLocationId = locationId,
                SelectedInventoryId = inventoryItemId,
                SelectedArrivingLocationId = 4 //4: Shipped
            };

            return View("Move", model);
        }


        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("Receive")]
        public ActionResult Receive()
        {
            var model = new ReceiveModel
            {
                Products = 
                    _inventoryService.GetProductItems(string.Empty),
                DestinationLocations = _locationService.GetLocations(1).ToList(),
                SourceLocations = _locationService.GetExternalLocations()
            };

            return View("Receive", model);
        }

        [HttpPost]
        [Route("SendTransfer")]
        public ActionResult SendTransfer(MoveModel model)
        {
            try
            {
                model.Locations = _locationService.GetLocations();
                model.TransferComplete = _transferService.TransferOut(model.SelectedInventoryId, model.SelectedDepartureLocationId,
                    model.SelectedArrivingLocationId, model.SelectedQuantity);

                if (!model.TransferComplete.GetValueOrDefault())
                {
                    ModelState.AddModelError("GENERALERR-1", "Transfer Incomplete. General Error");
                }

                model.ItemsAtLocation =
                    _inventoryService.GetAllLocationsWithProduct()
                        .ToList();
                model.SelectedQuantity = 0;
                return View("Move", model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("CAUGHTERR-1", e.Message);
                model.TransferComplete = false;
                model.ItemsAtLocation =
                    _inventoryService.GetLocationsWithProduct(model.SelectedInventoryId)
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
                model.DestinationLocations = _locationService.GetLocations(1); //1: Internal
                model.SourceLocations = _locationService.GetExternalLocations();
                model.Products = _inventoryService.GetProductItems(string.Empty);
                model.TransferComplete = _transferService.TransferOut(model.SelectedProductId, 
                    model.SelectedSourceLocationId, model.SelectedDestinationLocationId, model.SelectedQuantity);

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