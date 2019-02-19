using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class KhoaPhong
    {
        public KhoaPhong()
        {
            DanhMucBacSi = new HashSet<DanhMucBacSi>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string TieuDeKhoa { get; set; }
        public string TenKhoaPhong { get; set; }
        public string GioiThieu { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string UserModify { get; set; }
        public int? FkLoaiKhoaPhong { get; set; }
        public int? FkNgonNgu { get; set; }
        public string HinhAnhDaiDien { get; set; }
        public bool? HenKhamBenh { get; set; }
        public int? Stt { get; set; }
        public int? LuotXem { get; set; }

        public virtual LoaiKhoaPhong FkLoaiKhoaPhongNavigation { get; set; }
        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual ICollection<DanhMucBacSi> DanhMucBacSi { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
