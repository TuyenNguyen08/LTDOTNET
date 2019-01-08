using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class LienKetWebSite
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string Link { get; set; }
        public int? FkNgonNgu { get; set; }
        public int? Stt { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
    }
}
