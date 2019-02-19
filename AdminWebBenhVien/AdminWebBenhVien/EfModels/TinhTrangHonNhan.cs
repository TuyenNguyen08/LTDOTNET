using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class TinhTrangHonNhan
    {
        public TinhTrangHonNhan()
        {
            HenKhamBenh = new HashSet<HenKhamBenh>();
        }

        public int Id { get; set; }
        public string TinhTrang { get; set; }
        public int? FkNgonNgu { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual ICollection<HenKhamBenh> HenKhamBenh { get; set; }
    }
}
