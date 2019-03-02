using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class SuKienIndexViewModel
    {
        public int Id { get; set; }

        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public int? Xem { get; set; }

        public string Link { get; set; } // ???
        public int? NgonNguId { get; set; }
        public string NgonNgu { get; set; }

        public DateTime? NgayTao { get; set; }
       
        public string NguoiTaoId { get; set; }

        public string NguoiTao { get; set; }

    }

    public class SuKienEditViewModel
    {
        public int Id { get; set; }

        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public int? Xem { get; set; }

        public string Link { get; set; } // ???
        public int? NgonNguId { get; set; }

        public DateTime? NgayTao { get; set; }

        public string NguoiTao { get; set; }

    }

    public class SuKienCreateViewModel
    {

        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public int? Xem { get; set; }
        public string Link { get; set; } // ???
        public int? NgonNguId { get; set; }
     
        public string NguoiTao { get; set; }

    }
}
