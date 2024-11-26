using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string? Hoten { get; set; }

    public string? Taikhoan { get; set; }

    public string? Matkhau { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public string? Gioitinh { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
