using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class SlideShowViewModel
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string HinhAnh { get; set; }
        public int? FkNgonNgu { get; set; }
        public int? Stt { get; set; }
        public bool? IsNewtab { get; set; }
        public string LinkEvent { get; set; }
        public bool? IsLink { get; set; }

    }
}
