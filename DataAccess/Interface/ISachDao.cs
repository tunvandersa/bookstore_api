using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DTO;
using BusinessObjects.Models;
namespace DataAccess.Interface
{
    public interface ISachDao
    {
        Task<bool> CreateAsync(SachDto model);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(SachDto model);
        Task<List<SachDto>> GetAllAsync();
        Task<Sach> GetByIdAsync(int id);
        Task<List<TheLoaiDto>> GetTLAsync();
        Task<List<TacGiaDto>> GetTacGiaAsync();
        Task<List<NhaXuatBanDto>> GetNXBAsync();
    }
}
