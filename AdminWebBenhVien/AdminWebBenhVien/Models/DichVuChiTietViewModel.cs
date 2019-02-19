using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class DichVuChiTietViewModel
    {

        public int FkNgonNgu { get; set; }
        public int FkDichVu { get; set; }
        public string TenDichVu { get; set; }
        public string GioiThieu { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public string FkUserModify { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayChinhSua { get; set; }
        public int Id { get; set; }
        public int? LuotXem { get; set; }

    }
}
