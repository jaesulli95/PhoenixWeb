using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public async Task<IActionResult> Index()
        {
            List<Item>? Items = new List<Item>();
            var Client = new HttpClient();
            try
            {
                var response = await Client.GetAsync($"{this.ApiBaseUrl}/Items");
                string apiResponse = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<Item>>(apiResponse);
                Debug.WriteLine(response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception occured");
            }
            ViewData["Items"] = Items;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult?> CreateItem(Item item)
        {

            try
            {
                var Client = new HttpClient();
                //HERE CREATE THE CLIENT TO SEND TO AN API ENDPOINT
                HttpResponseMessage response = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Items/Create", item);
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction(nameof(this.Index));
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public IActionResult EditItem()
        {
            return RedirectToAction(nameof(this.Index));
        }
    }
}
