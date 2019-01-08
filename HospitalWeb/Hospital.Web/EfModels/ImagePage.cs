using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class ImagePage
    {
        public int FkNgonNgu { get; set; }
        public string PageName { get; set; }
        public string ImagePath { get; set; }
        public string TieuDe { get; set; }
        public int Id { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
    }
}
