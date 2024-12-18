using Microsoft.AspNetCore.Mvc;

namespace Quantum.Controllers
{
	public class SettingsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
