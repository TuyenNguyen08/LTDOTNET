using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class LoaiTuyenDung
    {
        public LoaiTuyenDung()
        {
            TuyenDungThongBao = new HashSet<TuyenDungThongBao>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }
        public int? FkNgonNgu { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual ICollection<TuyenDungThongBao> TuyenDungThongBao { get; set; }
    }
}
