using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using BusinessObjects.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace book_store_api.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness _cartBusiness;

        public CartController(ICartBusiness cartBusiness)
        {
            _cartBusiness = cartBusiness;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartBusiness.GetCartAsync();
            return Ok(cart);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem item)
        {
            await _cartBusiness.AddToCartAsync(item);
            return Ok();
        }

        [HttpPost]
        [Route("remove/{maSach}")]
        public async Task<IActionResult> RemoveFromCart(int maSach)
        {
            await _cartBusiness.RemoveFromCartAsync(maSach);
            return Ok();
        }

        [HttpPost]
        [Route("clear")]
        public async Task<IActionResult> ClearCart()
        {
            await _cartBusiness.ClearCartAsync();
            return Ok();
        }
        [HttpPost]
        [Route("reduce/{maSach}")]
        public async Task<IActionResult> ReduceItem(int maSach)
        {
            await _cartBusiness.ReduceItemAsync(maSach);
            return Ok();
        }
        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> SaveCartAsync([FromBody] Address address)
        {
            await _cartBusiness.CreateOrderAsync(address);
            return Ok();
        }
         
    }
}

