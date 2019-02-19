using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class DanhMucBacSi
    {
        public DanhMucBacSi()
        {
            HenKhamBenh = new HashSet<HenKhamBenh>();
        }

        public int Id { get; set; }
        public string TenBacSi { get; set; }
        public int? FkKhoaPhong { get; set; }

        public virtual KhoaPhong FkKhoaPhongNavigation { get; set; }
        public virtual ICollection<HenKhamBenh> HenKhamBenh { get; set; }
    }
}
