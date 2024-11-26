using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using BusinessObjects.DTO;
using book_store_client.Models;
using System.Diagnostics;
using Azure.Core;
namespace book_store_client.Controllers
{
    public class SachController : Controller
    {
        private readonly ILogger<SachController> _logger;
        private readonly HttpClient? _client = null;
        private string url = "";

        public SachController(ILogger<SachController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
          
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "admin" || HttpContext.Session.GetString("Role") == null)
            {
                return Redirect("/Auth/LogIn");
            }
            else
            {
                url = $"https://localhost:7120/api/Sach/List\r\n";
                List<SachDto> sachDtos = new List<SachDto>();
                HttpResponseMessage response = await _client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    sachDtos = response.Content.ReadFromJsonAsync<List<SachDto>>().Result;
                }
                return View(sachDtos);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<List<TheLoaiDto>> GetTheloai()
        {
            url = $"https://localhost:7120/api/Sach/TheLoai";
            List<TheLoaiDto> theloaiDtos = new List<TheLoaiDto>();
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                theloaiDtos = response.Content.ReadFromJsonAsync<List<TheLoaiDto>>().Result;
            }
            return theloaiDtos;
        }

        public async Task<List<TacGiaDto>> GetTacGia()
        {
            url = $"https://localhost:7120/api/Sach/TacGia";
            List<TacGiaDto> tacgiaDtos = new List<TacGiaDto>();
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                tacgiaDtos = response.Content.ReadFromJsonAsync<List<TacGiaDto>>().Result;
            }
            return tacgiaDtos;
        }
        public async Task<List<NhaXuatBanDto>> GetNXB()
        {
            url = $"https://localhost:7120/api/Sach/NhaXuatBan";
            List<NhaXuatBanDto> nhaxuatbanDtos = new List<NhaXuatBanDto>();
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                nhaxuatbanDtos = response.Content.ReadFromJsonAsync<List<NhaXuatBanDto>>().Result;
            }
            return nhaxuatbanDtos;
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.TheLoai = await GetTheloai();
            ViewBag.TacGia = await GetTacGia();
            ViewBag.NhaXuatBan = await GetNXB();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] SachDto model, IFormFile anhBia)
        {
            ViewBag.TheLoai = await GetTheloai();
            ViewBag.TacGia = await GetTacGia();
            ViewBag.NhaXuatBan = await GetNXB();
            if (!ModelState.IsValid)
            {
                ViewBag.TheLoai = await GetTheloai();
                ViewBag.TacGia = await GetTacGia();
                ViewBag.NhaXuatBan = await GetNXB();
                return View(model);
            }

            // Nếu có ảnh bìa được tải lên, xử lý và lưu ảnh
            if (anhBia != null && anhBia.Length > 0)
            {
                // Lưu ảnh vào thư mục trên server
               var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data");
                if (!Directory.Exists(uploadFolder))
               {
                   Directory.CreateDirectory(uploadFolder);
               }

               var fileName = Guid.NewGuid().ToString() + Path.GetExtension(anhBia.FileName);
                var filePath = Path.Combine(uploadFolder, fileName);

               using (var stream = new FileStream(filePath, FileMode.Create))
                {
                   await anhBia.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn ảnh bìa vào model
                model.AnhBia = "/Data/" + fileName;
            }

            url = $"https://localhost:7120/api/Sach/Create\r\n";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, model);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewData["msg"] = "Create Succsess!";
            }
            else
            {
                ViewData["msg"] = "Create Faild. Try again!";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DaleteAsync(int? id)
        {
            var sach = await GetByIdAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            url = $"https://localhost:7120/api/Sach/Delete/{id}";
            HttpResponseMessage response = await _client.DeleteAsync(url);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK && result)
            {
                TempData["msg"] = "Delete Success!";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else

            {
                TempData["msg"] = "Delete Failed. Try again!";
            }

            return RedirectToAction("Index");

        }
        private async Task<SachDto?> GetByIdAsync(int? id)
        {
            if (id == null) return null;

            url = $"https://localhost:7120/api/Sach/{id}";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.ReadFromJsonAsync<SachDto>().Result;
            }
            return null;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int? id)
        {
            var product = await GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.TheLoai = await GetTheloai();
            ViewBag.TacGia = await GetTacGia();
            ViewBag.NhaXuatBan = await GetNXB();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromForm] SachDto model)
        {
            ViewBag.TheLoai = await GetTheloai();
            ViewBag.TacGia = await GetTacGia();
            ViewBag.NhaXuatBan = await GetNXB();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            url = "https://localhost:7120/api/Sach/Update";
            HttpResponseMessage response = await _client.PutAsJsonAsync(url, model);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK && result)
            {
                ViewData["msg"] = "Update Success!";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                ViewData["msg"] = "Update Failed. Try again!";
            }

            return View(model);
        }
    }
}