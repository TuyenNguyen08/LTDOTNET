using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class SlideShow
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string HinhAnh { get; set; }
        public int? FkNgonNgu { get; set; }
        public int? Stt { get; set; }
        public bool? IsNewtab { get; set; }
        public string LinkEvent { get; set; }
        public bool? IsLink { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
    }
}
