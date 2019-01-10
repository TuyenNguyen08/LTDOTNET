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

        public List<GioiThieuChiTiet> lsGioiThieuChiTiet { get; set; }
    }
}
