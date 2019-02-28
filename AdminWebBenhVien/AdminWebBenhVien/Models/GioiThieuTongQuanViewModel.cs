using System;

namespace AdminWebBenhVien.Models
{
    public class GioiThieuTongQuanIndexViewModel
    {
        public int Id { get; set; }

        public int TenLoaiId { get; set; }
        public string TenLoai { get; set; }

        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public int? Xem { get; set; }

        public int NgonNguId { get; set; }
        public string NgonNgu { get; set; }

        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string NguoiSuaId { get; set; }
        public string NguoiSua { get; set; }
    }

    public class GioiThieuTongQuanEditViewModel
    {
        public int Id { get; set; }

        public string TenLoai { get; set; }

        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string NoiDung { get; set; }
        public int? Xem { get; set; }

        public string NgonNgu { get; set; }

        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
    }
}
