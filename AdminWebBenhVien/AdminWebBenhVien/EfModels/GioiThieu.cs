using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class GioiThieu
    {
        public GioiThieu()
        {
            GioiThieuChiTiet = new HashSet<GioiThieuChiTiet>();
        }

        public int Id { get; set; }
        public string TenGioiThieu { get; set; }

        public virtual ICollection<GioiThieuChiTiet> GioiThieuChiTiet { get; set; }
    }
}
