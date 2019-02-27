using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class LienHeKhachHangViewModel
    {

        public int Id { get; set; }
        public string HoVaTen { get; set; }
        public string DienThoaiNha { get; set; }
        public string DienThoaiDiDong { get; set; }
        public string Email { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayGui { get; set; }
        public bool? DaXem { get; set; }
        public string FkNguoiXem { get; set; }
        public DateTime? NgayXem { get; set; }
        public string TieuDe { get; set; }

    }
}
