using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class HoatDongViewModel
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string HinhAnhMinhHoa { get; set; }
        public int? FkNgonNgu { get; set; }
        public int? FkLoaiHoatDong { get; set; }
        public string NoiDung { get; set; }
        public int? Stt { get; set; }
        public int? LuotXem { get; set; }
        public string FkNguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public string Author { get; set; }
        public string FkNguoiSua { get; set; }
        public DateTime? NgaySua { get; set; }
    }
   
}
