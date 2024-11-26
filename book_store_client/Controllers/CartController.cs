using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using BusinessObjects.Models;
using BusinessObjects.DTO;
using book_store_client.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using System.Net;
namespace book_store_client.Controllers
{

    public class CartController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AuthController> _logger;
        private string url = "";
      
        public CartController(ILogger<AuthController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {         
            url = $"https://localhost:7120/api/Cart/List";
            Cart cart = new Cart();
            var client = _clientFactory.CreateClient(); 
            client.DefaultRequestHeaders.Add("Cookie", Request.Headers["Cookie"].ToString());
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                cart = await response.Content.ReadFromJsonAsync<Cart>();
            }
            return View(cart);
        }
        public async Task<IActionResult> SaveCart([FromForm] Address address)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("LogIn", "Auth");
            }
            try
            {
                var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Cookie", Request.Headers["Cookie"].ToString());
                // Gửi yêu cầu POST để lưu giỏ hàng
                var url = "https://localhost:7120/api/Cart/Save";
                var response = await client.PostAsJsonAsync(url, address);

                if (response.IsSuccessStatusCode)
                {
                    await client.PostAsync("https://localhost:7120/api/Cart/clear", null);
                    ViewBag.Message = "Đặt hàng thành công!";
                    return RedirectToAction("Index"); ;
                }
                else
                {
                    ViewBag.Message = "Đặt hàng không thành công!";
                    return RedirectToAction("Index"); ;
                }
            }
            catch (Exception ex)
           {
                ViewBag.Message = ex.Message;
                return RedirectToAction("Index"); ;
            }
        }
    }
}
