using System.Web.Mvc;

namespace InventoryManagerWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}