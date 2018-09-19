using System.Web.Mvc;
using DataAccess.DTO;
using InventoryManagerService.Interface;
using InventoryManagerWebsite.Models.Report;

namespace InventoryManagerWebsite.Controllers
{
    [RoutePrefix("Report")]
    public class ReportController : Controller
    {
        private readonly IProductService productService;
        private readonly ITransferService transferService;
        private readonly ILocationService locationService;

        public ReportController(IProductService productService, ITransferService transferService, ILocationService locationService)
        {
            this.productService = productService;
            this.transferService = transferService;
            this.locationService = locationService;
        }

        // GET: Report
        public ActionResult Index()
        {
            return Product(new ProductReportModel());
        }

        public ActionResult Product(ProductReportModel model)
        {

            model.Items = productService.GetLocationsWithProduct(model.SelectedProductId, model.SelectedLocationId);
            model.Products = productService.GetProductItems(string.Empty);
            model.Locations = locationService.GetLocations(3); //3: Warehouse

            model.Products.Insert(0, new ProductDto { Id = 0, Name = "Select a Product"});
            model.Locations.Insert(0, new LocationDto {Id = 0, Description = "Select a Location"});

            return View("Product", model);
        }

        public ActionResult Transfer(TransferReportModel model)
        {

            model.Items = transferService.GetTransfers(model.SelectedBeginDate, model.SelectedEndDate);

            return View("Transfer", model);
        }
    }
}