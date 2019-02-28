using Hospital.Web.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Web.Models
{
    public class HomeModel
    {
        public List<KhoaPhong> lsKhoaPhong { get; set; }

        public List<SlideShow> lsSLide { get; set; }
        public List<GioiThieuChiTiet> lsGioiThieu { get; set; }
        public List<TinTuc> lsTinTuc { get; set; }
        public List<HoatDong> lsHoatDong { get; set; }
        public List<SubNote> lsSubNote { get; set; }
        public List<SubPhone> lsSubPhone { get; set; }
        //public List<GioKham> lsGioKham { get; set; }
        //public List<LichNgay> lsLichNgay { get; set; }
        public List<Video> lsVideo { get; set; }

    }
}
