using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quantum.Models;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Identity.Client;
using static System.Reflection.Metadata.BlobBuilder;
using System.Net;

namespace Quantum.Controllers
{
	public class BooksController : Controller
	{
		private IConfiguration configuration;
		private string? ApiBaseUrl;

		public BooksController(IConfiguration configuration)
		{
			this.configuration = configuration;
			ApiBaseUrl = this.configuration["PhoenixLifeApiBase"];
		}
		public async Task<IActionResult> Index()
		{

			List<Book>? books = new List<Book>();
			var Client = new HttpClient();
			try
			{
				var response = await Client.GetAsync($"{this.ApiBaseUrl}/Books");
				string apiResponse = await response.Content.ReadAsStringAsync();
				books = JsonConvert.DeserializeObject<List<Book>>(apiResponse);
				books = books.OrderBy(b => b.Status).ToList();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Exception occured");
			}
			ViewData["Books"] = books;

			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> Create([Bind("Name,Author,Status")] Book book)
		{
			try
			{
				var Client = new HttpClient();
				//HERE CREATE THE CLIENT TO SEND TO AN API ENDPOINT
				HttpResponseMessage response = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Books/Create", book);
				string apiResponse = await response.Content.ReadAsStringAsync();
			}
			catch (Exception ex)
			{
			}

			return RedirectToAction(nameof(this.Index));
		}
		
		public async Task<IActionResult> Edit(int BookId)
		{

			Book? b = new Book();
			var Client = new HttpClient();
			try
			{
				var response = await Client.GetAsync($"{this.ApiBaseUrl}/Books/{BookId}");
				string apiResponse = await response.Content.ReadAsStringAsync();
				b = JsonConvert.DeserializeObject<Book>(apiResponse);
				if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					return RedirectToAction(nameof(this.Index));
				}
			}
			catch (Exception ex)
			{

			}
			return View(b);

		}

		[HttpPost]
		public async Task<IActionResult> EditBook([Bind("id,Name,Author,Status")] Book UpdatedBookInfo)
		{
			try
			{
				var Client = new HttpClient();
				Book? b = new Book();
				var response = await Client.GetAsync($"{this.ApiBaseUrl}/Books/{UpdatedBookInfo.id}");
				string apiResponse = await response.Content.ReadAsStringAsync();
				b = JsonConvert.DeserializeObject<Book>(apiResponse);

				//UPDATE THE INFO
				b.Name = UpdatedBookInfo.Name;
				b.Author = UpdatedBookInfo.Author;
				b.Status = UpdatedBookInfo.Status;

				//Edit the book Info
				HttpResponseMessage editResp = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Books/Edit", b);
			}
			catch(Exception e)
			{

			}
			return RedirectToAction(nameof(this.Index));
		}

		public async Task<IActionResult> EditStatus(int BookId, int Status)
		{
			//I Need Get Book And Update
			Book? b = new Book();
			var Client = new HttpClient();
			try
			{
				var response = await Client.GetAsync($"{this.ApiBaseUrl}/Books/{BookId}");
				string apiResponse = await response.Content.ReadAsStringAsync();
				b = JsonConvert.DeserializeObject<Book>(apiResponse);
				if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					return RedirectToAction(nameof(this.Index));
				}

				if(b != null)
				{
					b.Status = Status;
					HttpResponseMessage editResp = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Books/Edit", b);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Exception occured");
			}
			return RedirectToAction(nameof(this.Index));
		}

		public async Task<IActionResult> DeleteBook(int BookId)
		{
			try
			{
				var Client = new HttpClient();
				HttpResponseMessage resp = await Client.DeleteAsync($"{this.ApiBaseUrl}/Books/Delete/{BookId}");
				if (resp.StatusCode == System.Net.HttpStatusCode.OK)
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
