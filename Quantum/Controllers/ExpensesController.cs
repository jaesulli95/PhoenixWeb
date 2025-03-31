using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quantum.Models;
using System.Diagnostics;

namespace PhoenixWeb.Controllers
{
    public class ExpensesController : Controller
    {
        string[] ExpenseCategories = { "Rent", "Food", "Phone", "Car Insurance", "Taekwondo","Utilities", "Misc" };
        private IConfiguration configuration;
        private string? ApiBaseUrl;

        public ExpensesController(IConfiguration configuration) {
            this.configuration = configuration;
            ApiBaseUrl = this.configuration["PhoenixLifeApiBase"];
        }

        public async Task<IActionResult> Index()
        {

            List<Expense>? Expenses = new List<Expense>();
            var Client = new HttpClient();
            try
            {
                var response = await Client.GetAsync($"{this.ApiBaseUrl}/Expenses");
                string apiResponse = await response.Content.ReadAsStringAsync();
                Expenses = JsonConvert.DeserializeObject<List<Expense>>(apiResponse);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception occured");
            }

            ViewBag.Expenses = Expenses;
            ViewBag.ExpenseCategories = ExpenseCategories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(Expense expense)
        {
            try
            {
                var Client = new HttpClient();
                HttpResponseMessage response = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Expenses/Add", expense);
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
    }
}
