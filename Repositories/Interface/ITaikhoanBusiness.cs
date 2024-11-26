using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ITaikhoanBusiness
    {
        Task<bool> SignUpAsync(Taikhoan model);
        Task<bool> LogInAsync(string username, string password);
        Task<Taikhoan> GetAccountAsync(string username);
    }
}
