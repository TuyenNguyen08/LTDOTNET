using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class GioiThieuChiTietViewModel
    {

        public string Mode { get; set; }
        public int FkNgonNgu { get; set; }
        public int FkGioiThieu { get; set; }
        [Display(Name = "Tên")]
        public string Ten { get; set; }
        [Display(Name = "Giới thiệu")]
        public string GioiThieu { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
        public string FkUserModify { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayChinhSua { get; set; }
        public int Id { get; set; }
        public int? LuotXem { get; set; }
    }
}
