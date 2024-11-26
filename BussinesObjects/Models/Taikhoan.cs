using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public partial class Taikhoan
{
    [Required(ErrorMessage = "Tên tài khoản không để trống")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không để trống")]
    public string? Password { get; set; }

    public string? Quyen { get; set; }

    public DateTime? Dob { get; set; }

    public string? Mobile { get; set; }

    public string? Address { get; set; }
}
