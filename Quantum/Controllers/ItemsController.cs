using Microsoft.AspNetCore.Mvc;
using Quantum.Models;

namespace PhoenixWeb.Controllers
{
    public class ItemsController : Controller
    {
        private IConfiguration configuration;
        private string? ApiBaseUrl;

        public ItemsController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ApiBaseUrl = this.configuration["PhoenixLifeApiBase"];
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult?> CreateItem(Item item)
        {

            try
            {
                /*var Client = new HttpClient();
                //HERE CREATE THE CLIENT TO SEND TO AN API ENDPOINT
                HttpResponseMessage response = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Projects/Create", project);
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction(nameof(this.Index));
                }*/
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction(nameof(this.Index));
        }
    }
}
