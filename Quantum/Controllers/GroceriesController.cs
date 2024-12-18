using Microsoft.AspNetCore.Mvc;

namespace Quantum.Controllers
{
	public class GroceriesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
