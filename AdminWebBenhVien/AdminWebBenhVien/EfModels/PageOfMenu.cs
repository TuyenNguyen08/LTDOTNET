using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class PageOfMenu
    {
        public int Id { get; set; }
        public string NamePage { get; set; }
        public bool? Active { get; set; }
        public int? FkMenu { get; set; }

        public virtual MenuLeft FkMenuNavigation { get; set; }
    }
}
