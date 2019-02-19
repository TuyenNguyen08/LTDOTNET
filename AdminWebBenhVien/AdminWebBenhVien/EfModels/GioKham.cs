using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class GioKham
    {
        public GioKham()
        {
            HenKhamBenh = new HashSet<HenKhamBenh>();
        }

        public int Id { get; set; }
        public string Gio { get; set; }

        public virtual ICollection<HenKhamBenh> HenKhamBenh { get; set; }
    }
}
