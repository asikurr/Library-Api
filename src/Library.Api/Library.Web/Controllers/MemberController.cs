using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Library.Web.Controllers
{
    public class MemberController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7147/api");
        private readonly HttpClient _httpClient;

        public MemberController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseUrl;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<MemberViewModel> memberList = new List<MemberViewModel>();
            var response = _httpClient
                .GetAsync($"{_httpClient.BaseAddress}/Member/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                var membData = response.Content.ReadAsStringAsync().Result;
                memberList = JsonConvert.DeserializeObject<List<MemberViewModel>>(membData);
            }
            return View(memberList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MemberViewModel model)
        {
            try
            {
                var inpData = JsonConvert.SerializeObject(model);
                var strData = new StringContent(inpData, Encoding.UTF8,
                    "application/json");
                var response = _httpClient
                    .PostAsync($"{_httpClient.BaseAddress}/Member/Post", strData).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Member Created Success.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Member Created Failed. {ex.Message}";
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                MemberViewModel memberData = new MemberViewModel();
                var response = _httpClient
                    .GetAsync($"{_httpClient.BaseAddress}/Member/Get/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    memberData = JsonConvert.DeserializeObject<MemberViewModel>(data);
                }
                return View(memberData);

            }
            catch (Exception ex)
            {

                TempData["error"] = $"Something went wrong. {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(MemberViewModel model)
        {
            try
            {
                var data = JsonConvert.SerializeObject(model);
                var content = new StringContent(data, Encoding.UTF8,
                    "application/json");
                var response = _httpClient
                    .PutAsync($"{_httpClient.BaseAddress}/Member/Put", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Member Update Success.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["error"] = $"Member Update Failed. {ex.Message}";
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                MemberViewModel memberData = new MemberViewModel();
                var response = _httpClient
                    .GetAsync($"{_httpClient.BaseAddress}/Member/Get/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    memberData = JsonConvert.DeserializeObject<MemberViewModel>(data);
                }
                return View(memberData);
            }
            catch (Exception ex)
            {

                TempData["error"] = $"Member Data not found. {ex.Message}";
                return View();
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var response = _httpClient
                       .DeleteAsync($"{_httpClient.BaseAddress}/Member/Delete/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Member Deleted Success.";
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
