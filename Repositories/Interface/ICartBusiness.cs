using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ICartBusiness
    {  
            Task<Cart> GetCartAsync();
            Task SaveCartAsync(Cart cart);
            Task AddToCartAsync(CartItem item);
            Task RemoveFromCartAsync(int maSach);
            Task ClearCartAsync();
            Task ReduceItemAsync(int maSach);
            Task<bool> CreateOrderAsync(Address address);
    }
}
