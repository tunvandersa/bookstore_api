using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class ChiTietDonHang
{
    public int MaDonHang { get; set; }

    public int MaSach { get; set; }

    public int? SoLuong { get; set; }

    public double? DonGia { get; set; }

    public virtual DonHang MaDonHangNavigation { get; set; } = null!;

    public virtual Sach MaSachNavigation { get; set; } = null!;
}
