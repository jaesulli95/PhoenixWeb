using Microsoft.AspNetCore.Mvc;

namespace Quantum.Controllers
{
	public class GitController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
