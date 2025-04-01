using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.Controllers
{
	public class ErrorController : Controller
	{
		[Route("Error/Timeout")]
		public IActionResult Timeout()
		{
			return View();
		}

		[Route("Error/{statusCode}")]
		public IActionResult HttpStatusCodeHandler(int statusCode)
		{
			if (statusCode == 504 || statusCode == 408) // Gateway Timeout
			{
				return View("Timeout");
			}

			return View("Error");
		}
	}
}
