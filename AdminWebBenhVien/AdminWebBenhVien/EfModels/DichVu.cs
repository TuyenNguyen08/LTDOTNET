using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class DichVu
    {
        public DichVu()
        {
            DichVuChiTiet = new HashSet<DichVuChiTiet>();
        }

        public int Id { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<DichVuChiTiet> DichVuChiTiet { get; set; }
    }
}
