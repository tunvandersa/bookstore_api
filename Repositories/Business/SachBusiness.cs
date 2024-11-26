using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using BusinessObjects.DTO;
using Repositories.Interface;
using DataAccess.Interface;
namespace Repositories.Business
{
    public class SachBusiness : ISachBusiness
    {
        private readonly ISachDao _sachDao;

        public SachBusiness (ISachDao sachDao)
        {
            _sachDao = sachDao;  
        }

        public async Task<bool> CreateAsync (SachDto model)
        {
            return await _sachDao.CreateAsync (model);
        }

        public async Task<bool> DeleteAsync (int id)
        {
            return await _sachDao.DeleteAsync(id);
        }
        public async Task<bool> UpdateAsync (SachDto model)
        {
            return await _sachDao.UpdateAsync(model);  
        }
        public async Task<List<SachDto>> GetAllAsync()
        {
            return await _sachDao.GetAllAsync();
        }
        public async Task<SachDto> GetByIdAsync(int id)
        {
            var sach = await _sachDao.GetByIdAsync(id);
            var result = sach == null ? null : new SachDto
            {
                MaSach = sach.MaSach,
                TenSach = sach.TenSach,
                MaTl = sach.MaTl,
                Gia = sach.Gia,
                Soluongton = sach.Soluongton,
                SoTrang = sach.SoTrang,
                MaNxb = sach.MaNxb,
                MaTacGia = sach.MaTacGia,
                MaTlNavigation = new TheLoaiDto
                {
                    MaTl = sach.MaTlNavigation!.MaTl,
                    TenTl = sach.MaTlNavigation!.TenTl,
                },
                MaTacGiaNavigation = new TacGiaDto
                {
                    MaTacGia = sach.MaTacGiaNavigation!.MaTacGia,
                    TenTacGia = sach.MaTacGiaNavigation!.TenTacGia,
                },
                MaNxbNavigation = new NhaXuatBanDto
                {
                    MaNxb = sach.MaNxbNavigation!.MaNxb,
                    TenNxb = sach.MaNxbNavigation!.TenNxb,
                },

            };
            return result;
        }
        public async Task<List<TheLoaiDto>> GetTLAsync()
        {
            return await _sachDao.GetTLAsync();
        }
        public async Task<List<TacGiaDto>> GetTacGiaAsync()
        {
            return await _sachDao.GetTacGiaAsync();
        }
        public async Task<List<NhaXuatBanDto>> GetNXBAsync()
        {
            return await _sachDao.GetNXBAsync();
        }
    }
}
