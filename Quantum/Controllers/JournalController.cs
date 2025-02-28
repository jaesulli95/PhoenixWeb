using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quantum.Models;

namespace PhoenixWeb.Controllers
{
    public class JournalController : Controller
    {
        private IConfiguration configuration;
        private string? ApiBaseUrl;

        public JournalController(IConfiguration configuration)
        {
            this.configuration = configuration;
            ApiBaseUrl = this.configuration["PhoenixLifeApiBase"];
        }

        public async Task<IActionResult> Index()
        {
            List<JournalEntry>? Entries = new List<JournalEntry>();
            var Client = new HttpClient();
            try
            {
                var response = await Client.GetAsync($"{this.ApiBaseUrl}/Entries");
                string apiResponse = await response.Content.ReadAsStringAsync();
                Entries = JsonConvert.DeserializeObject<List<JournalEntry>>(apiResponse);
                Debug.WriteLine(response.StatusCode.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception occured");
            }

            ViewData["Entries"] = Entries;

            return View();
        }

        public async Task<IActionResult> CreateEntry([Bind()] JournalEntry journalEntry)
        {

            try
            {
                var Client = new HttpClient();
                HttpResponseMessage response = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Entries/Create", journalEntry);
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    
                    //return RedirectToAction(nameof(this.Index));
                }
            }
            catch (Exception ex)
            {

            }
            return Ok();
            //return RedirectToAction(nameof(this.Index));
        }
    }
}
