﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quantum.Models;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;

namespace Quantum.Controllers
{
	public class ProjectsController : Controller
	{
		private IConfiguration configuration;
		private string? ApiBaseUrl;

		public ProjectsController(IConfiguration configuration)
		{
			this.configuration = configuration;
			ApiBaseUrl = this.configuration["PhoenixLifeApiBase"];
		}
		public async Task<IActionResult> Index()
		{
			string[] Categories = { "Unreal Engine", "C#", "Python", "AI", "Data Science"};

			List<Project>? Projects = new List<Project>();
			var Client = new HttpClient();
			try
			{
				var response = await Client.GetAsync($"{this.ApiBaseUrl}/Projects");
				string apiResponse = await response.Content.ReadAsStringAsync();
				Projects = JsonConvert.DeserializeObject<List<Project>>(apiResponse);
				Debug.WriteLine(response.StatusCode.ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Exception occured");
			}
			ViewData["Projects"] = Projects;
			ViewData["Categories"] = Categories;
			return View();
		}
		public async Task<IActionResult> Edit(int ProjectId)
		{

			Project? project = new Project();
			ProjectEditData projectEditData = new ProjectEditData();
			try
			{
				var Client = new HttpClient();
				var Resp = await Client.GetAsync($"{this.ApiBaseUrl}/Projects/{ProjectId}");
				string ApiResp = await Resp.Content.ReadAsStringAsync();
				project = JsonConvert.DeserializeObject<Project>(ApiResp);

				if(project != null)
				{
					projectEditData.project = project;
				}

				List<ProjectTask>? ProjectTasks = new List<ProjectTask>();
				var response = await Client.GetAsync($"{this.ApiBaseUrl}/Projects/Tasks/{ProjectId}");
				string apiResponse = await response.Content.ReadAsStringAsync();
				ProjectTasks = JsonConvert.DeserializeObject<List<ProjectTask>>(apiResponse);

				if(ProjectTasks != null)
				{
					projectEditData.projectTasks = ProjectTasks;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			ViewData["ProjectEditData"] = projectEditData;
			return View(projectEditData);
		}
		public async Task<IActionResult> EditProject([Bind("Id,Description,VCLink,Status")] Project project)
		{
			try
			{
				Project? UpdatedProject = new Project();
				var Client = new HttpClient();
				var Resp = await Client.GetAsync($"{this.ApiBaseUrl}/Projects/{project.Id}");
				string ApiResp = await Resp.Content.ReadAsStringAsync();
				UpdatedProject = JsonConvert.DeserializeObject<Project>(ApiResp);
				if (UpdatedProject != null)
				{
					UpdatedProject.Description = project.Description;
					UpdatedProject.VCLink = project.VCLink;
					UpdatedProject.Status = project.Status;

					HttpResponseMessage UpdResp = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Projects/Edit", UpdatedProject);
				}
			}
			catch (Exception ex)
			{

			}
			return RedirectToAction(nameof(this.Index));
		}
		[HttpPost]
		public async Task<IActionResult?> Create([Bind("Name,Description,VCLink,Category,Status")] Project project)
		{

			try
			{
				var Client = new HttpClient();
				//HERE CREATE THE CLIENT TO SEND TO AN API ENDPOINT
				HttpResponseMessage response = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Projects/Create", project);
				string apiResponse = await response.Content.ReadAsStringAsync();
				if(response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return RedirectToAction(nameof(this.Index));
				}
			}
			catch(Exception ex)
			{

			}

			return RedirectToAction(nameof(this.Index));
		}
		public async Task<IActionResult> DeleteProject([Bind("ProjectId,ProjectName")] int ProjectId, string ProjectName)
		{
			Project? project = new Project();
			try
			{
				var Client = new HttpClient();
				var Resp = await Client.GetAsync($"{this.ApiBaseUrl}/Projects/{ProjectId}");
				string ApiResp = await Resp.Content.ReadAsStringAsync();
				project = JsonConvert.DeserializeObject<Project>(ApiResp);
				if (project != null)
				{
					if(project.Name == ProjectName)
					{
						HttpResponseMessage resp = await Client.DeleteAsync($"{this.ApiBaseUrl}/Projects/Delete/{ProjectId}");
						if (resp.StatusCode == System.Net.HttpStatusCode.OK)
						{
							return RedirectToAction(nameof(this.Index));
						}
					}
					else
					{
						return RedirectToAction(nameof(this.Index));
					}
				}

			}
			catch (Exception e)
			{
				//Do Nothing Here
			}
			return RedirectToAction(nameof(this.Edit));
		}
		public async Task<IActionResult> CreateProjectTask([Bind("ProjectId,Name,Description,Type")] ProjectTask pTask)
		{
			var Client = new HttpClient();
			try
			{
				HttpResponseMessage Response = await Client.PostAsJsonAsync($"{this.ApiBaseUrl}/Projects/Tasks/Create", pTask);
				if (Response.IsSuccessStatusCode)
				{
					//Do someething when we are OK
					return RedirectToAction(nameof(this.Index));
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message.ToString());
			}
			return RedirectToAction(nameof(this.Index));
		}
	}
}