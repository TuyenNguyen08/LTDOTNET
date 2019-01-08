using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class GioiThieuChiTiet
    {
        public int FkNgonNgu { get; set; }
        public int FkGioiThieu { get; set; }
        public string Ten { get; set; }
        public string GioiThieu { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public string FkUserModify { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayChinhSua { get; set; }
        public int Id { get; set; }
        public int? LuotXem { get; set; }

        public virtual GioiThieu FkGioiThieuNavigation { get; set; }
        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual User FkUserModifyNavigation { get; set; }
    }
}
