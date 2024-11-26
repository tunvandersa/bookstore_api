using BusinessObjects.Models;
using DataAccess.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace DataAccess.DAOs
{
    public class TaiKhoanDao : ITaiKhoanDao
    {
        private readonly qlbsDbContext _dbContext;
        private readonly ILogger<Sach> _logger;

        public TaiKhoanDao(qlbsDbContext dbContext, ILogger<Sach> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<bool> SignUpAsync(Taikhoan model)
        {
            try
            {
                if (model.Password == null || model.Username == null)
                {
                    return false;
                }
                var existingAccount = await GetAccountAsync(model.Username);
                if (existingAccount != null)
                {
                    return false;
                }
                model.Quyen = "user";
                var hashedpassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                model.Password = hashedpassword;
                await _dbContext.Taikhoans.AddAsync(model);
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
        public async Task<Taikhoan> GetAccountAsync(string username)
        {
            var taikhoan = await _dbContext.Taikhoans.FirstOrDefaultAsync(x => x.Username == username);
            return taikhoan;
        }
        public async Task<bool> LogInAsync(string username, string password)
        {
            try
            {
                var taikhoan = await GetAccountAsync(username);
                if (taikhoan == null)
                {
                    return false;
                }
                if (BCrypt.Net.BCrypt.Verify(password, taikhoan.Password))
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
    }
}
