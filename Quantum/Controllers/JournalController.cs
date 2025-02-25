using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.Controllers
{
    public class JournalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
