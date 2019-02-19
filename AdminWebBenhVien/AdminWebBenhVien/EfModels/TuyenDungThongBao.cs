using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class TuyenDungThongBao
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string NoiDung { get; set; }
        public int? FkNgonNgu { get; set; }
        public DateTime? NgayTao { get; set; }
        public string FkNguoiTao { get; set; }
        public int? FkLoaiTuyenDung { get; set; }
        public string HinhMinhHoa { get; set; }
        public int? Stt { get; set; }
        public int? LuotXem { get; set; }

        public virtual LoaiTuyenDung FkLoaiTuyenDungNavigation { get; set; }
        public virtual NgonNgu FkNgonNguNavigation { get; set; }
        public virtual User FkNguoiTaoNavigation { get; set; }
    }
}
