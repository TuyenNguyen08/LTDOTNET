using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class NoiDungTinhChiTiet
    {
        public int FkNgonNgu { get; set; }
        public int FkNoiDungTinh { get; set; }
        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public DateTime? NgayChinhSua { get; set; }
        public string FkUserChinhsua { get; set; }
        public int Id { get; set; }
        public int? LuotXem { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual NoiDungTinh FkNoiDungTinhNavigation { get; set; }
        public virtual User FkUserChinhsuaNavigation { get; set; }
    }
}
