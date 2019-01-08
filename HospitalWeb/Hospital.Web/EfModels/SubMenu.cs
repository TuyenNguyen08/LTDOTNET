using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class SubMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int? Stt { get; set; }
        public int? FkNgonNgu { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
    }
}
