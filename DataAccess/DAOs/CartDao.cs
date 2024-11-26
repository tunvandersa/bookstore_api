using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Data;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
namespace DataAccess.DAOs
{
    public class CartDao : ICartDao
    {
        private readonly qlbsDbContext _dbContext;
        private readonly ILogger<Cart> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartSession";
        public CartDao(qlbsDbContext dbContext, ILogger<Cart> logger, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CreateOrderAsync(Address address)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            Cart cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            string username = session.GetString("Username");
            KhachHang kh = await _dbContext.KhachHangs.FirstOrDefaultAsync(s => s.Taikhoan == username);
            if(kh == null)
            {
                return false;
            }
            try
            {
                var donhang = new DonHang
                {
                    DaThanhToan = 0,
                    TinhTrangGiaoHang = 0,
                    NgayDat = DateTime.Now,
                    NgayGiao = null,
                    MaKh = kh.MaKh,
                    Xa = address.Xa,
                    Huyen = address.Huyen,
                    ThanhPho = address.ThanhPho,
                    Nuoc = address.Nuoc,
                    SDT = address.SDT,
                    HoVaTen = address.HoVaTen,
                    ThanhTien = Convert.ToDecimal(cart.TotalAmount),
                };

                await _dbContext.DonHangs.AddAsync(donhang);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    foreach (var item in cart.Items)
                    {
                        var orderdetail = new ChiTietDonHang
                        {
                            MaDonHang = donhang.MaDonHang,
                            MaSach = item.MaSach,
                            SoLuong = item.SoLuong,
                            DonGia = item.GiaBan,
                        };
                        await _dbContext.ChiTietDonHangs.AddAsync(orderdetail); 
                    }
                    if(await _dbContext.SaveChangesAsync() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
