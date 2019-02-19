using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class HenKhamBenh
    {
        public int Id { get; set; }
        public int? FkChuyenKhoa { get; set; }
        public int? FkBacSi { get; set; }
        public DateTime? NgayHen { get; set; }
        public int? FkGioHen { get; set; }
        public string MoTaTrieuChung { get; set; }
        public DateTime? NgayGui { get; set; }
        public string DiaChiEmail { get; set; }
        public string HoVaTen { get; set; }
        public int? FkNamSinh { get; set; }
        public bool? GioiTinh { get; set; }
        public int? FkTinhTrangHonNhan { get; set; }
        public int? FkQuocTich { get; set; }
        public string SoDienThoaiNha { get; set; }
        public string SoDienThoaiDiDong { get; set; }
        public string DiaChi { get; set; }
        public string BacSi { get; set; }
        public int? FkTrangThai { get; set; }

        public virtual DanhMucBacSi FkBacSiNavigation { get; set; }
        public virtual PhongKham FkChuyenKhoaNavigation { get; set; }
        public virtual GioKham FkGioHenNavigation { get; set; }
        public virtual NamSinh FkNamSinhNavigation { get; set; }
        public virtual QuocTich FkQuocTichNavigation { get; set; }
        public virtual TinhTrangHonNhan FkTinhTrangHonNhanNavigation { get; set; }
        public virtual TrangThai FkTrangThaiNavigation { get; set; }
    }
}
