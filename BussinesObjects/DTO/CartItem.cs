using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public double? TotalAmount => Items.Sum(item => item.SoLuong * item.GiaBan);
    }
    public class CartItem
    {
        public int MaSach { get; set; }
        public string? TenSach { get; set; }
        public int? SoLuong { get; set; }
        public double? GiaBan { get; set; }
        public string? AnhBia { get; set; }
    }
}
