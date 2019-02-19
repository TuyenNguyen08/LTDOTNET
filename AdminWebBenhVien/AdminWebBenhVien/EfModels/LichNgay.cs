using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class LichNgay
    {
        public LichNgay()
        {
            LichKham = new HashSet<LichKham>();
        }

        public int Id { get; set; }
        public string TenThu { get; set; }
        public int? FkNgonNgu { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual ICollection<LichKham> LichKham { get; set; }
    }
}
