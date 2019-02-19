using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class PhanHoi
    {
        public int Id { get; set; }
        public DateTime? NgayKham { get; set; }
        public string MaSoBenhNhan { get; set; }
        public string HoVaTen { get; set; }
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }
        public string DienThoai { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayGui { get; set; }
        public bool? DaXem { get; set; }
        public int? FkLoaiPhanHoi { get; set; }
        public int? FkNgonNgu { get; set; }
        public string NoiDungTraLoi { get; set; }
        public DateTime? NgayTraLoi { get; set; }
        public string FkUserTraLoi { get; set; }
        public string DiaChi { get; set; }
        public bool? IsShow { get; set; }
        public string TieuDe { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual User FkUserTraLoiNavigation { get; set; }
    }
}
