using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class LoaiCauHoi
    {
        public LoaiCauHoi()
        {
            HoiDapYkhoa = new HashSet<HoiDapYkhoa>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }
        public int? FkNgonNgu { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual ICollection<HoiDapYkhoa> HoiDapYkhoa { get; set; }
    }
}
