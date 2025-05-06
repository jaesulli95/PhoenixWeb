using Microsoft.AspNetCore.Mvc;

namespace PhoenixWeb.Controllers
{
    public class ArtController : Controller
    {
        public IActionResult Gallery()
        {
            return View("~/Views/Art/Gallery.cshtml");
        }


        public IActionResult ThreeD()
        {
            return View("~/Views/Art/ThreeD.cshtml");
        }
    }
}
