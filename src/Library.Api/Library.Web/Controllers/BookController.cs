﻿using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7147/api");
        private readonly HttpClient _httpClient;

        public BookController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<BookViewModel> bookList = new List<BookViewModel>();
            var response = _httpClient
                .GetAsync($"{_httpClient.BaseAddress}/Book/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                var bookData = response.Content.ReadAsStringAsync().Result;
                bookList = JsonConvert.DeserializeObject<List<BookViewModel>>(bookData);
            }
            return View(bookList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadAuthorData();
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            try
            {
                var inpData = JsonConvert.SerializeObject(model);
                var strData = new StringContent(inpData, Encoding.UTF8,
                    "application/json");
                var response = _httpClient
                    .PostAsync($"{_httpClient.BaseAddress}/Book/Post", strData).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Book Created Success.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Book Created Failed. {ex.Message}";
                return View();
            }
            return View();
        }

        [NonAction]
        private void LoadAuthorData()
        {
            var authList = new List<AuthorViewModel>();
            var response = _httpClient
                .GetAsync($"{_httpClient.BaseAddress}/Authors/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                var authData = response.Content.ReadAsStringAsync().Result;
                authList = JsonConvert.DeserializeObject<List<AuthorViewModel>>(authData);
            }
            ViewBag.Authors = new SelectList(authList, "AuthorID", "AuthorName");
        }
    }
}
