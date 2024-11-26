using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class TheLoai
{
    public int MaTl { get; set; }

    public string? TenTl { get; set; }

    public string? MotaTheLoai { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
