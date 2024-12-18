using Microsoft.AspNetCore.Mvc;

namespace Quantum.Controllers
{
	public class ChoresController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
