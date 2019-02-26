using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Web.Controllers
{
    public class ControllerBase : Controller
    {

        public ControllerBase(InitParam initParam)
        {
            InitParam = initParam;
        }

        protected InitParam InitParam { get; set; }

        protected int? NgonNgu = 1;
    }
}
