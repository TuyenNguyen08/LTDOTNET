using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class NoiDungTinh
    {
        public NoiDungTinh()
        {
            NoiDungTinhChiTiet = new HashSet<NoiDungTinhChiTiet>();
        }

        public int Id { get; set; }
        public string TenNoiDung { get; set; }

        public virtual ICollection<NoiDungTinhChiTiet> NoiDungTinhChiTiet { get; set; }
    }
}
