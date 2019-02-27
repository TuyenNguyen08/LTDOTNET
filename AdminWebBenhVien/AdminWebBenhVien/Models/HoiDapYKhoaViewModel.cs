using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class HoiDapYKhoaViewModel
    {

        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungCauHoi { get; set; }
        public string HoTenNguoiHoi { get; set; }
        public bool? GioiTinh { get; set; }
        public int? FkNamSinh { get; set; }
        public string NoiDungTraLoi { get; set; }
        public DateTime? NgayGuiCauHoi { get; set; }
        public DateTime? NgayTraLoi { get; set; }
        public string NguoiTraLoi { get; set; }
        public bool? IsShow { get; set; }
        public int? FkChuyenKhoa { get; set; }
        public string FkUserTraLoi { get; set; }
        public bool? DaTraLoi { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }

    }
}
