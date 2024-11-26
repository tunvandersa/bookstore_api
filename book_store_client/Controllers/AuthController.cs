using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using BusinessObjects.Models;
using BusinessObjects.DTO;
using book_store_client.Models;
using System.Diagnostics;
using Newtonsoft.Json;
namespace book_store_client.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient? _client;
        private readonly ILogger<AuthController> _logger;
        private string url = "";

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        [HttpPost]
        public async Task<IActionResult> LogInAsync([FromForm] string username, [FromForm] string password)
        {
            var url = "https://localhost:7120/api/Auth/LogIn";
            var logindata = new loginData { username = username, password = password };

            HttpResponseMessage response = await _client.PostAsJsonAsync(url, logindata);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize phản hồi JSON thành đối tượng dynamic
                var result = JsonConvert.DeserializeObject<dynamic>(responseBody);

                // Lấy giá trị từ đối tượng dynamic
                string userName = result.username;
                string role = result.role;

                // Kiểm tra HttpContext.Session không null
                if (HttpContext.Session != null)
                {
                    HttpContext.Session.SetString("Username", username);
                    HttpContext.Session.SetString("Role", role);
                }
                else
                {
                    return View();
                }

                if (role == "admin")
                {
                    return Redirect("/Sach/Index");
                }
                else
                {
                    return Redirect("/Home/Index"); // Thay đổi điều hướng nếu cần thiết
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return View();
            }
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromForm] Taikhoan model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            url = $"https://localhost:7120/api/Auth/SignUp";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, model);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewData["msg"] = "Đăng ký thành công!";
            }
            else
            {
                ViewData["msg"] = "Tên tài khoản đã tồn tại!";
            }
            return View(model);
        }
       
    }
}
