using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Models;
using Repositories.Interface;
using Repositories.Business;
using Microsoft.AspNetCore.Identity.Data;
using BusinessObjects.DTO;
using static System.Net.WebRequestMethods;
namespace book_store_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITaikhoanBusiness _TaikhoanBusiness;
        public AuthController(ITaikhoanBusiness taikhoanBusiness)
        {
            _TaikhoanBusiness = taikhoanBusiness;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] loginData model)
        {
            var isLoggedIn = await _TaikhoanBusiness.LogInAsync(model.username, model.password);
            if (isLoggedIn)
            {
                // Lưu quyền vào Session
                var account = await _TaikhoanBusiness.GetAccountAsync(model.username);
                HttpContext.Session.SetString("Username", account.Username);
                 
                HttpContext.Session.SetString("Role", account.Quyen);

                var role = HttpContext.Session.GetString("Role");

                return Ok(new
                {
                    message = "Đăng nhập thành công.",
                    username = account.Username,
                    role = account.Quyen
                });
            }

            return Unauthorized("Tên đăng nhập hoặc mật khẩu không chính xác.");
        }
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] Taikhoan model)
        {
            var response = await _TaikhoanBusiness.SignUpAsync(model);
            return Ok(response);
        
        }


    }
}
