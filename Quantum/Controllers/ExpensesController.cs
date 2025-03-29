using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.Controllers
{
    public class ExpensesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
