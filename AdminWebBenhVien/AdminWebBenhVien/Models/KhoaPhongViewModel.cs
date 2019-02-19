using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class KhoaPhongViewModel
    {

        public int Id { get; set; }
        [Display(Name = "Tiêu đề khoa")]
        public string TieuDeKhoa { get; set; }
        [Display(Name = "Tên khoa phòng")]
        public string TenKhoaPhong { get; set; }
        [Display(Name = "Giới thiệu")]
        public string GioiThieu { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime? NgayCapNhat { get; set; }
        public string UserModify { get; set; }
        public int? FkLoaiKhoaPhong { get; set; }
        public int? FkNgonNgu { get; set; }
        [Display(Name = "Hình ảnh")]
        public string HinhAnhDaiDien { get; set; }
        [Display(Name = "Hẹn khám bệnh")]
        public bool? HenKhamBenh { get; set; }
        public int? Stt { get; set; }
        [Display(Name = "Lượt xem")]
        public int? LuotXem { get; set; }
    }
}
