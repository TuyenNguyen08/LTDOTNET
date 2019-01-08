using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class LoaiHoatDong
    {
        public LoaiHoatDong()
        {
            HoatDong = new HashSet<HoatDong>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<HoatDong> HoatDong { get; set; }
    }
}
