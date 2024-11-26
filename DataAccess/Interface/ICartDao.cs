using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DTO;
namespace DataAccess.Interface
{
    public interface ICartDao
    {
        Task<bool> CreateOrderAsync(Address address);
    }
}
