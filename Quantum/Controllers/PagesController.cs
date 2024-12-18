using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Quantum.Models;

namespace Quantum.Controllers;

public class PagesController : Controller
{
		public IActionResult ErrorPage()
		{
				return View();
		}
}
