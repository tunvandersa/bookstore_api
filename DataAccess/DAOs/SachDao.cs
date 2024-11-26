using BusinessObjects.DTO;
using BusinessObjects.Models;
using DataAccess.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class SachDao : ISachDao
    {
        private readonly qlbsDbContext _dbContext;
        private readonly ILogger<Sach> _logger;

        public SachDao(qlbsDbContext dbContext, ILogger<Sach> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(SachDto model)
        {                 
            try
            {
                var sach = new Sach
                {
                    TenSach = model.TenSach,
                    MaTl = model.MaTl,
                    MoTa = model.MoTa,
                    Soluongton = model.Soluongton,
                    MaNxb = model.MaNxb,
                    NamXuatBan = model.NamXuatBan,
                    SoTrang = model.SoTrang,
                    Gia = model.Gia,
                    MaTacGia = model.MaTacGia,
                    AnhBia = model.AnhBia,      
                };
                await _dbContext.Saches.AddAsync(sach);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var sach = await GetByIdAsync(id);
            try
            {
                _dbContext.Remove(sach);
                var response = await _dbContext.SaveChangesAsync() > 0 ? true : false;
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }

        }
        public async Task<List<SachDto>> GetAllAsync()
        {
            var saches = await _dbContext.Saches.Include(x => x.MaTacGiaNavigation)
                                                .Include(x => x.MaNxbNavigation)
                                                .Include(x => x.MaTlNavigation)
                                                .ToListAsync();
            var response = saches.Select(e => new SachDto
            {
                MaSach = e.MaSach,
                TenSach = e.TenSach,
                NamXuatBan = e.NamXuatBan,
                MaTl = e.MaTl,
                Gia = e.Gia,
                AnhBia =e.AnhBia,
                Soluongton = e.Soluongton,
                SoTrang = e.SoTrang,
                MaNxb = e.MaNxb,
                MaTacGia = e.MaTacGia,
                MaTlNavigation = new TheLoaiDto
                {
                    MaTl = e.MaTlNavigation!.MaTl,
                    TenTl = e.MaTlNavigation!.TenTl,
                },
                MaTacGiaNavigation = new TacGiaDto
                {
                    MaTacGia = e.MaTacGiaNavigation!.MaTacGia,
                    TenTacGia = e.MaTacGiaNavigation!.TenTacGia,
                },
                MaNxbNavigation = new NhaXuatBanDto
                {
                    MaNxb = e.MaNxbNavigation!.MaNxb,
                    TenNxb = e.MaNxbNavigation!.TenNxb,
                },
            }).ToList();

            return response;
        }
        public async Task<Sach> GetByIdAsync(int id)
        {
            var sach = await _dbContext.Saches
                .Include(x => x.MaTlNavigation)
                .Include(x => x.MaTacGiaNavigation)
                .Include(x => x.MaNxbNavigation)
                .AsNoTracking().FirstOrDefaultAsync(x => x.MaSach == id);
            return sach;
        }
        public async Task<List<TheLoaiDto>> GetTLAsync()
        {
            var response = await _dbContext.TheLoais.ToListAsync();
            return response.Select(e =>
            new TheLoaiDto
            {
                MaTl = e.MaTl,
                TenTl = e.TenTl,
            }).ToList();
        }
        public async Task<List<TacGiaDto>> GetTacGiaAsync()
        {
            var response = await _dbContext.TacGia.ToListAsync();
            return response.Select(e =>
            new TacGiaDto
            {
                MaTacGia = e.MaTacGia,
                TenTacGia = e.TenTacGia,
            }).ToList();
        }
        public async Task<List<NhaXuatBanDto>> GetNXBAsync()
        {
            var response = await _dbContext.NhaXuatBans.ToListAsync();
            return response.Select(e =>
            new NhaXuatBanDto
            {
                MaNxb = e.MaNxb,
                TenNxb = e.TenNxb,
            }).ToList();
        }

        public async Task<bool> UpdateAsync(SachDto model)
        {
            var sach = new Sach 
            {
                TenSach = model.TenSach,
                MaTl = model.MaTl,
                MoTa = model.MoTa,
                Soluongton = model.Soluongton,
                MaNxb = model.MaNxb,
                NamXuatBan = model.NamXuatBan,
                SoTrang = model.SoTrang,
                Gia = model.Gia,
                MaTacGia = model.MaTacGia,
            };
            try
            {
                _dbContext.Update(sach);
                var response = await _dbContext.SaveChangesAsync() > 0 ? true : false;
                return response;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
    

}
