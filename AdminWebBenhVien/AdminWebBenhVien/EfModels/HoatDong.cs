using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class HoatDong
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string HinhAnhMinhHoa { get; set; }
        public int? FkNgonNgu { get; set; }
        public int? FkLoaiHoatDong { get; set; }
        public string NoiDung { get; set; }
        public int? Stt { get; set; }
        public int? LuotXem { get; set; }
        public string FkNguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public string Author { get; set; }
        public string FkNguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }

        public virtual LoaiHoatDong FkLoaiHoatDongNavigation { get; set; }
        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual User FkNguoiSuaNavigation { get; set; }
        public virtual User FkNguoiTaoNavigation { get; set; }
    }
}
