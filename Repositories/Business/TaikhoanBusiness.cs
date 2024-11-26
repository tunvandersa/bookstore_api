using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interface;
using BusinessObjects.Models;
using DataAccess.Interface;
namespace Repositories.Business
{
    public class TaikhoanBusiness : ITaikhoanBusiness
    {
        private readonly ITaiKhoanDao _TaikhoanDao;
        public TaikhoanBusiness (ITaiKhoanDao taikhoanDao)
        {
            _TaikhoanDao = taikhoanDao;
        }
        public async Task<bool> SignUpAsync(Taikhoan model)
        {
            return await _TaikhoanDao.SignUpAsync(model);
        }
        public async Task<bool> LogInAsync(string username, string password)
        {
            return await _TaikhoanDao.LogInAsync(username, password);
        }
        public async Task<Taikhoan> GetAccountAsync(string username)
        {
            return await _TaikhoanDao.GetAccountAsync(username);    
        }
    } 
}
