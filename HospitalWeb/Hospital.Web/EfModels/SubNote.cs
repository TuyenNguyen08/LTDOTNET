using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class SubNote
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string Link { get; set; }
        public int? FkNgonNgu { get; set; }
        public string Image { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
    }
}
