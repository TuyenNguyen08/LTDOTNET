using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class LoaiKhoaPhong
    {
        public LoaiKhoaPhong()
        {
            KhoaPhong = new HashSet<KhoaPhong>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<KhoaPhong> KhoaPhong { get; set; }
    }
}
