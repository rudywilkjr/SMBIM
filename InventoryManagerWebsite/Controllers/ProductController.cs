using System;
using System.Web.Mvc;
using DataAccess.DTO;
using InventoryManagerService.Inventory;
using InventoryManagerWebsite.Models.Product;

namespace InventoryManagerWebsite.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService = new ProductService();
        //private LocationService _locationService = new LocationService();

        public ActionResult Index()
        {
            return View("Lookup", new ProductLookupModel());
        }

        [Route("Item/{id}")]
        public ActionResult Item(int? id)
        {
            ProductModel model;
            if (id != null)
            {
                model = new ProductModel
                {
                    ProductItem = _productService.GetProductItem(id.Value),
                    Locations = _productService.GetLocationsWithProduct(id.Value)
                };
            }
            else
            {
                model = new ProductModel
                {
                    ProductItem = new ProductDto()
                };
            }


            return View("Item", model);
        }

        [Route("Item/New")]
        public ActionResult New(ProductModel model)
        {
            return View("Item", model);
        }

        [Route("Lookup")]
        public ActionResult Lookup(ProductLookupModel searchModel)
        {
            var searchedProductsModel = new ProductLookupModel
            {
                ProductItems = _productService.GetProductItems(searchModel.SearchText, searchModel.includeInactive),
                includeInactive = searchModel.includeInactive
            };
            

            return View("Lookup", searchedProductsModel);
        }

        [Route("Item/Update")]
        [HttpPost]
        public ActionResult Save(ProductModel model)
        {
            try
            {
                model.ProductItem = _productService.UpdateProductItem(model.ProductItem);
                TempData["ToastType"] = "Success";
                TempData["Toast"] = "New Product Saved Successfully.";
            }
            catch (Exception e)
            {
                TempData["ToastType"] = "Error";
                TempData["Toast"] = e.Message;
                return New(model);
            }
            
            return Lookup(new ProductLookupModel { SearchText = model.ProductItem.Name });
        }

        [HttpPost]
        public ActionResult Delete(ProductModel model)
        {
            try
            {
                _productService.DeleteProductItem(model.ProductItem.Id);
                TempData["ToastType"] = "Success";
                TempData["Toast"] = "Product Deleted Successfully.";
            }
            catch (Exception e)
            {
                TempData["ToastType"] = "Error";
                TempData["Toast"] = e.Message;
                return Item(model.ProductItem.Id);
            }

            return Lookup(new ProductLookupModel { SearchText = null });
        }

    }
}