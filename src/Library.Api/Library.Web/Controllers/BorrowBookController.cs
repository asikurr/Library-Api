using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace Library.Web.Controllers
{
    public class BorrowBookController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7147/api");
        private readonly HttpClient _httpClient;

        public BorrowBookController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<BorrowBookViewModel> boroBookList = new List<BorrowBookViewModel>();
            var response = _httpClient
                .GetAsync($"{_httpClient.BaseAddress}/BookBorroweds/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                var bookData = response.Content.ReadAsStringAsync().Result;
                boroBookList = JsonConvert.DeserializeObject<List<BorrowBookViewModel>>(bookData);
            }
            return View(boroBookList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadMemberData();
            LoadBookData();
            return View();
        }
        [HttpPost]
        public IActionResult Create(BorrowBookViewModel model)
        {
            try
            {
                var inpData = JsonConvert.SerializeObject(model);
                var strData = new StringContent(inpData, Encoding.UTF8,
                    "application/json");
                var response = _httpClient
                    .PostAsync($"{_httpClient.BaseAddress}/BookBorroweds/Post", strData).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Borrowed Book Created Success.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Borrowed Book Created Failed. {ex.Message}";
                return View();
            }
            return View();
        }

        [NonAction]
        private void LoadBookData()
        {
            var bookList = new List<BookViewModel>();
            var response = _httpClient
                .GetAsync($"{_httpClient.BaseAddress}/Books/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                var bookData = response.Content.ReadAsStringAsync().Result;
                bookList = JsonConvert.DeserializeObject<List<BookViewModel>>(bookData);
            }
            ViewBag.Books = new SelectList(bookList, "BookID", "Title");

        }

        [NonAction]
        private void LoadMemberData()
        {
            var membList = new List<MemberViewModel>();
            var res = _httpClient
                .GetAsync($"{_httpClient.BaseAddress}/Members/Get").Result;
            if (res.IsSuccessStatusCode)
            {
                var membData = res.Content.ReadAsStringAsync().Result;
                membList = JsonConvert.DeserializeObject<List<MemberViewModel>>(membData);
            }
            ViewBag.Members = new SelectList(membList, "MemberID", "FirstName");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                BorrowBookViewModel boroBookData = new BorrowBookViewModel();
                var response = _httpClient
                    .GetAsync($"{_httpClient.BaseAddress}/BookBorroweds/Get/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    boroBookData = JsonConvert.DeserializeObject<BorrowBookViewModel>(data);
                }
                LoadMemberData();
                LoadBookData();
                return View(boroBookData);

            }
            catch (Exception ex)
            {

                TempData["error"] = $"Something went wrong. {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(BorrowBookViewModel model)
        {
            try
            {
                var data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, Encoding.UTF8,
                    "application/json");
                var response = _httpClient
                    .PutAsync($"{_httpClient.BaseAddress}/BookBorroweds/Put", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Borrower Book info Update Success.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["error"] = $"Borrower Book info Update Failed. {ex.Message}";
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                BorrowBookViewModel boroBookData = new BorrowBookViewModel();
                var response = _httpClient
                    .GetAsync($"{_httpClient.BaseAddress}/BookBorroweds/Get/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    boroBookData = JsonConvert.DeserializeObject<BorrowBookViewModel>(data);
                }
                LoadMemberData();
                LoadBookData();
                return View(boroBookData);
            }
            catch (Exception ex)
            {

                TempData["error"] = $"Borrower Book Data not found. {ex.Message}";
                return View();
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var response = _httpClient
                       .DeleteAsync($"{_httpClient.BaseAddress}/BookBorroweds/Delete/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Borrower Book information Deleted Success.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Operation Failed. {ex.Message}";
                return View();
            }
            return View();
        }
    }
}
