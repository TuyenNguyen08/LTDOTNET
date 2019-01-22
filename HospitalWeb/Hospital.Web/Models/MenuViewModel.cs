using Hospital.Web.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Web.Models
{
    public class MenuViewModel
    {
        public Menu Menu { get; set; }

        public List<MenuViewModel> ListMenu { get; set; } = new List<MenuViewModel>();
    }
}
