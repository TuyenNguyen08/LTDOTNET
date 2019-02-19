using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class TinTucViewModel
    {

        public int Id { get; set; }
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }
        [Display(Name = "Giới thiệu")]
        public string GioiThieu { get; set; }
        [Display(Name = "Hình ảnh")]
        public string HinhAnhMinhHoa { get; set; }
        public int? FkNgonNgu { get; set; }
        public int? FkLoaiTin { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? NgayTao { get; set; }
        public string FkUserNguoiTao { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }
        public int? Stt { get; set; }
        [Display(Name = "Lượt xem")]
        public int? LuotXem { get; set; }
        public string Author { get; set; }
        public string FkUserNguoiSua { get; set; }
        [Display(Name = "Ngày sửa")]
        public DateTime? NgaySua { get; set; }

    }
}
