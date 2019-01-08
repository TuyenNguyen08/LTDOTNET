using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class MenuLeft
    {
        public MenuLeft()
        {
            PageOfMenu = new HashSet<PageOfMenu>();
        }

        public int Idmenu { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int? Idparrent { get; set; }
        public int? Prioty { get; set; }

        public virtual ICollection<PageOfMenu> PageOfMenu { get; set; }
    }
}
