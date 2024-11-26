using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Sach
{
    public int MaSach { get; set; }

    public string? TenSach { get; set; }

    public string? MoTa { get; set; }

    public int? Soluongton { get; set; }

    public int? MaNxb { get; set; }

    public string? AnhBia { get; set; }

    public int? MaTl { get; set; }

    public int? NamXuatBan { get; set; }

    public int? SoTrang { get; set; }

    public int? MaTacGia { get; set; }

    public double? Gia { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual NhaXuatBan? MaNxbNavigation { get; set; }

    public virtual TacGia? MaTacGiaNavigation { get; set; }

    public virtual TheLoai? MaTlNavigation { get; set; }
}
