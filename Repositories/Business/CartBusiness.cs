using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using Repositories.Interface;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using DataAccess.Interface;

namespace Repositories.Business
{
    public class CartBusiness : ICartBusiness
    {
            private readonly ICartDao _cartDao;
            private readonly qlbsDbContext _dbcontext;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private const string CartSessionKey = "CartSession";
            public CartBusiness(IHttpContextAccessor httpContextAccessor, qlbsDbContext context, ICartDao cartDao)
            {
                _httpContextAccessor = httpContextAccessor;
                _dbcontext = context;
                _cartDao = cartDao;
            }
            public async Task SaveCartAsync(Cart cart)
            {
                var session = _httpContextAccessor.HttpContext.Session;
                var cartJson = JsonConvert.SerializeObject(cart);
                session.SetString(CartSessionKey, cartJson);
        }
        public async Task<Cart> GetCartAsync()
        {

            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            if (cartJson == null)
            {
                return new Cart();
            }
            else
            {
                return JsonConvert.DeserializeObject<Cart>(cartJson);
            }
        }
            public async Task AddToCartAsync(CartItem item)
            {
                var cart = await GetCartAsync();
                var existingItem = cart.Items.FirstOrDefault(i => i.MaSach == item.MaSach);

                // Kiểm tra số lượng tồn kho
                var sach = await _dbcontext.Saches.FindAsync(item.MaSach);
                
                if (existingItem != null)
                {
                    existingItem.SoLuong += item.SoLuong;
                    if (existingItem.SoLuong > sach.Soluongton)
                    {
                        existingItem.SoLuong = sach.Soluongton;
                    }
                }
                else
                {
                    cart.Items.Add(item);
                }
                await SaveCartAsync(cart);
            }

            public async Task RemoveFromCartAsync(int maSach)
            {
                var cart = await GetCartAsync();
                var item = cart.Items.FirstOrDefault(i => i.MaSach == maSach);
                if (item != null)
                {
                    cart.Items.Remove(item);
                    await SaveCartAsync(cart);
                }
            }
            public async Task ReduceItemAsync(int maSach)
            {
                var cart = await GetCartAsync();
                var item = cart.Items.FirstOrDefault(i => i.MaSach == maSach);
                item.SoLuong = item.SoLuong - 1;
                if(item.SoLuong == 0)
                {
                    cart.Items.Remove(item);   
                }
                await SaveCartAsync(cart);
            }
            public async Task ClearCartAsync()
            {
                await SaveCartAsync(new Cart());
            }
        public async Task<bool> CreateOrderAsync(Address address)
        {
            return await _cartDao.CreateOrderAsync(address);
        }
    }
}