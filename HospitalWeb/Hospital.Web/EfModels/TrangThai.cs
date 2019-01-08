using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class TrangThai
    {
        public TrangThai()
        {
            HenKhamBenh = new HashSet<HenKhamBenh>();
        }

        public int Id { get; set; }
        public string TenLoaiTrangThai { get; set; }

        public virtual ICollection<HenKhamBenh> HenKhamBenh { get; set; }
    }
}
