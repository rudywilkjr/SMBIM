using System.Web.Mvc;
using DataAccess.DTO;
using InventoryManagerService.Inventory;
using InventoryManagerService.Location;
using InventoryManagerService.Transfer;
using InventoryManagerWebsite.Models.Report;

namespace InventoryManagerWebsite.Controllers
{
    [RoutePrefix("Report")]
    public class ReportController : Controller
    {
        private readonly ProductService _productService = new ProductService();
        private readonly TransferService _transferService = new TransferService();
        private readonly LocationService _locationService = new LocationService();

        // GET: Report
        public ActionResult Index()
        {
            return Product(new ProductReportModel());
        }

        public ActionResult Product(ProductReportModel model)
        {

            model.Items = _productService.GetLocationsWithProduct(model.SelectedProductId, model.SelectedLocationId);
            model.Products = _productService.GetProductItems(string.Empty);
            model.Locations = _locationService.GetLocations(3); //3: Warehouse

            model.Products.Insert(0, new ProductDto { Id = 0, Name = "Select a Product"});
            model.Locations.Insert(0, new LocationDto {Id = 0, Description = "Select a Location"});

            return View("Product", model);
        }

        public ActionResult Transfer(TransferReportModel model)
        {

            model.Items = _transferService.GetTransfers(model.SelectedBeginDate, model.SelectedEndDate);

            return View("Transfer", model);
        }
    }
}