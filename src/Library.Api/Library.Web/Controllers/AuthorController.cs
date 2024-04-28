using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Library.Web.Controllers
{
    public class AuthorController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7147/api");
        private readonly HttpClient _httpClient;

        public AuthorController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<AuthorViewModel> authorList = new List<AuthorViewModel>();
            var response = _httpClient
                .GetAsync($"{_httpClient.BaseAddress}/authors/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                var authData = response.Content.ReadAsStringAsync().Result;
                authorList = JsonConvert.DeserializeObject<List<AuthorViewModel>>(authData);
            }
            return View(authorList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AuthorViewModel model)
        {
            try
            {
                var inpData = JsonConvert.SerializeObject(model);
                var strData = new StringContent(inpData, Encoding.UTF8,
                    "application/json");
                var response = _httpClient
                    .PostAsync($"{_httpClient.BaseAddress}/Authors/Post", strData).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Author Created Success.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Author Created Failed. {ex.Message}";
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                AuthorViewModel authorData = new AuthorViewModel();
                var response = _httpClient
                    .GetAsync($"{_httpClient.BaseAddress}/Authors/Get/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    authorData = JsonConvert.DeserializeObject<AuthorViewModel>(data);
                }
                return View(authorData);

            }
            catch (Exception ex)
            {

                TempData["error"] = $"Something went wrong. {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(AuthorViewModel model)
        {
            try
            {
                var data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, Encoding.UTF8,
                    "application/json");
                var response = _httpClient
                    .PutAsync($"{_httpClient.BaseAddress}/Authors/Put", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Author Update Success.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["error"] = $"Author Update Failed. {ex.Message}";
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                AuthorViewModel authorData = new AuthorViewModel();
                var response = _httpClient
                    .GetAsync($"{_httpClient.BaseAddress}/Authors/Get/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    authorData = JsonConvert.DeserializeObject<AuthorViewModel>(data);
                }
                return View(authorData);
            }
            catch (Exception ex)
            {

                TempData["error"] = $"Author Data not found. {ex.Message}";
                return View();
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var response = _httpClient
                       .DeleteAsync($"{_httpClient.BaseAddress}/Authors/Delete/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Author Deleted Success.";
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
