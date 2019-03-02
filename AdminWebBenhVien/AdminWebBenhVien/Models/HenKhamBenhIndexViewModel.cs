using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class HenKhamBenhIndexViewModel
    {
        public int Id { get; set; }
        public int? ChuyenKhoaId { get; set; }
        public string TenChuyenKhoa { get; set; }
        public int? BacSiId { get; set; }
        public string TenBacSi { get; set; }
        public DateTime? NgayHen { get; set; }
        public int? GioHenId { get; set; }
        public string GioHen { get; set; }
        public string MoTaTrieuChung { get; set; }
        public DateTime? NgayGui { get; set; }
        public string DiaChiEmail { get; set; }
        public string HoVaTen { get; set; }
        public int? NamSinhId { get; set; }
        public int? NamSinh { get; set; }
        public bool? GioiTinh { get; set; }
        public int? TinhTrangHonNhanId { get; set; }
        public string TinhTrangHonNhan { get; set; }
        public int? QuocTichId { get; set; }
        public string QuocTich { get; set; }
        public string SoDienThoaiNha { get; set; }
        public string SoDienThoaiDiDong { get; set; }
        public string DiaChi { get; set; }
        public string BacSi { get; set; }
        public int? TrangThaiId { get; set; }
        public string TenTrangThai { get; set; }
    }

    public class HenKhamBenhEditViewModel
    {
        public int Id { get; set; }
        public int? ChuyenKhoaId { get; set; }
        public int? BacSiId { get; set; }
        public DateTime? NgayHen { get; set; }
        public int? GioHenId { get; set; }
        public string MoTaTrieuChung { get; set; }
        public DateTime? NgayGui { get; set; }
        public string DiaChiEmail { get; set; }
        public string HoVaTen { get; set; }
        public int? NamSinhId { get; set; }
        public bool? GioiTinh { get; set; }
        public int? TinhTrangHonNhanId { get; set; }
        public int? QuocTichId { get; set; }
        public string SoDienThoaiNha { get; set; }
        public string SoDienThoaiDiDong { get; set; }
        public string DiaChi { get; set; }
        public string BacSi { get; set; }
        public int? TrangThaiId { get; set; }
    }

    public class HenKhamBenhCreateViewModel
    {
        public int Id { get; set; }
        public int? ChuyenKhoaId { get; set; }
        public int? BacSiId { get; set; }
        public DateTime? NgayHen { get; set; }
        public int? GioHenId { get; set; }
        public string MoTaTrieuChung { get; set; }
        public DateTime? NgayGui { get; set; }
        public string DiaChiEmail { get; set; }
        public string HoVaTen { get; set; }
        public int? NamSinhId { get; set; }
        public bool? GioiTinh { get; set; }
        public int? TinhTrangHonNhanId { get; set; }
        public int? QuocTichId { get; set; }
        public string SoDienThoaiNha { get; set; }
        public string SoDienThoaiDiDong { get; set; }
        public string DiaChi { get; set; }
        public string BacSi { get; set; }
        public int? TrangThaiId { get; set; }
    }
}
