using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class TacGia
{
    public int MaTacGia { get; set; }

    public string? TenTacGia { get; set; }

    public string? TieuSu { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
