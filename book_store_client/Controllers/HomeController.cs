using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using BusinessObjects.DTO;
using book_store_client.Models;
using System.Diagnostics;
namespace book_store_client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient? _client = null;
        private string url = "";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> Index()
        {
            url = $"https://localhost:7120/api/Sach/List\r\n";
            List<SachDto> sachDtos = new List<SachDto>();
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                sachDtos = response.Content.ReadFromJsonAsync<List<SachDto>>().Result;
            }
            var sach = sachDtos.Where(s => s.Soluongton > 0).ToList();
            return View(sach);
        }
    }
}
