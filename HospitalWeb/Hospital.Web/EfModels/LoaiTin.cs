using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class LoaiTin
    {
        public LoaiTin()
        {
            TinTuc = new HashSet<TinTuc>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<TinTuc> TinTuc { get; set; }
    }
}
