using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
