using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
namespace DataAccess.Interface
{
    public interface ITaiKhoanDao
    {
        Task<bool> SignUpAsync(Taikhoan model);
        Task<bool> LogInAsync(string username, string password);
        Task<Taikhoan> GetAccountAsync(string username);
    }
}
