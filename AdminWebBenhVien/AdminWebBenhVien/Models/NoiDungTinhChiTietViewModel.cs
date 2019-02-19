using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class NoiDungTinhChiTietViewModel
    {
        public int FkNgonNgu { get; set; }
        public int FkNoiDungTinh { get; set; }
        public string TieuDe { get; set; }
        public string GioiThieu { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
        public DateTime? NgayChinhSua { get; set; }
        public string FkUserChinhsua { get; set; }
        public int Id { get; set; }
        public int? LuotXem { get; set; }


    }
}
