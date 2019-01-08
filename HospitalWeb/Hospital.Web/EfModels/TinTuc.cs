using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class TinTuc
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string HinhAnhMinhHoa { get; set; }
        public int? FkNgonNgu { get; set; }
        public int? FkLoaiTin { get; set; }
        public DateTime? NgayTao { get; set; }
        public string FkUserNguoiTao { get; set; }
        public string NoiDung { get; set; }
        public int? Stt { get; set; }
        public int? LuotXem { get; set; }
        public string Author { get; set; }
        public string FkUserNguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }

        public virtual LoaiTin FkLoaiTinNavigation { get; set; }
        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual User FkUserNguoiSuaNavigation { get; set; }
        public virtual User FkUserNguoiTaoNavigation { get; set; }
    }
}
