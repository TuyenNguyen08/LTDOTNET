using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class Page
    {
        public int Idpage { get; set; }
        public string NamePage { get; set; }
        public bool? Active { get; set; }
        public int? FkMenu { get; set; }

        public virtual Menu FkMenuNavigation { get; set; }
    }
}
