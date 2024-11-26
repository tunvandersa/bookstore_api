using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public int? DaThanhToan { get; set; }

    public int? TinhTrangGiaoHang { get; set; }

    public DateTime? NgayDat { get; set; }

    public DateOnly? NgayGiao { get; set; }

    public int? MaKh { get; set; }
    public string? Xa { get; set; }
    public string? Huyen { get; set; }
    public string? ThanhPho {  get; set; }
    public string? Nuoc { get; set; }
    public string? SDT { get; set; }
    public string? HoVaTen { get; set; }
    public decimal? ThanhTien { get; set; }
    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual KhachHang? MaKhNavigation { get; set; }
}
