using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class SlideShowIndexViewModel
    {
        public int Id { get; set; }

        public string TieuDe { get; set; }

        public int? NgonNguId { get; set; }

        public string NgonNgu { get; set; }

        public string LinkSuKien { get; set; }
    }

    public class SlideShowEditViewModel
    {
        public int Id { get; set; }

        public string TieuDe { get; set; }

        public string HinhAnh { get; set; }

        public int? NgonNguId { get; set; }

        public string LinkSuKien { get; set; }
    }

    public class SlideShowCreateViewModel
    { 
        public string TieuDe { get; set; }

        public string HinhAnh { get; set; }

        public int? NgonNguId { get; set; }

        public string LinkSuKien { get; set; }
    }
}
