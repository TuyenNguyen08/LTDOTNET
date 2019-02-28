using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class VideoIndexViewModel
    {
        public int Id { get; set; }

        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string DuongDanFile { get; set; }
        public int? Xem { get; set; }

        public int NgonNguId { get; set; }
        public string NgonNgu { get; set; }

        public DateTime? NgayTao { get; set; }

        public string NguoiTao { get; set; }

        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
    }

    public class VideoEditViewModel
    {
        public int Id { get; set; }

        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string DuongDanFile { get; set; }
        public int? Xem { get; set; }
        public string NgonNgu { get; set; }

        public DateTime? NgayTao { get; set; }

        public string NguoiTao { get; set; }

        public DateTime? NgaySua { get; set; }
        public string NguoiSua { get; set; }
    }


}
