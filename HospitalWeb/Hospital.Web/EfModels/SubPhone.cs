using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class SubPhone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SoDienThoai { get; set; }
        public int? FkNgonNgu { get; set; }
        public bool? Master { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
    }
}
