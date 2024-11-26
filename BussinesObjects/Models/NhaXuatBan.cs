using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class NhaXuatBan
{
    public int MaNxb { get; set; }

    public string? TenNxb { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
