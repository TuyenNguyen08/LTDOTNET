using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class Event
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string HinhAnh { get; set; }
        public string Link { get; set; }
        public int? FkNgonNgu { get; set; }
        public DateTime? NgayTao { get; set; }
        public string FkUserTao { get; set; }
        public int? Stt { get; set; }
        public bool? NewTab { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual User FkUserTaoNavigation { get; set; }
    }
}
