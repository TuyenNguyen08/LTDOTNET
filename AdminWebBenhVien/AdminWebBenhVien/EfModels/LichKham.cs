using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class LichKham
    {
        public int FkLichNgay { get; set; }
        public int FkPhongKham { get; set; }
        public string Sang { get; set; }
        public string Chieu { get; set; }

        public virtual LichNgay FkLichNgayNavigation { get; set; }
        public virtual PhongKham FkPhongKhamNavigation { get; set; }
    }
}
