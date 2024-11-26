using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class SachDto
    {
        public int MaSach { get; set; }

        [Required(ErrorMessage = "Tên sách không để trống")]
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

        public virtual NhaXuatBanDto? MaNxbNavigation { get; set; }

        public virtual TacGiaDto? MaTacGiaNavigation { get; set; }

        public virtual TheLoaiDto? MaTlNavigation { get; set; }
    }
}
