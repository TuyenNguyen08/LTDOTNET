using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class PhongKham
    {
        public PhongKham()
        {
            HenKhamBenh = new HashSet<HenKhamBenh>();
            LichKham = new HashSet<LichKham>();
        }

        public int Id { get; set; }
        public string TenPhongKham { get; set; }
        public int? FkNgonNgu { get; set; }
        public bool? HenKham { get; set; }
        public string GhiChuThem { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual ICollection<HenKhamBenh> HenKhamBenh { get; set; }
        public virtual ICollection<LichKham> LichKham { get; set; }
    }
}
