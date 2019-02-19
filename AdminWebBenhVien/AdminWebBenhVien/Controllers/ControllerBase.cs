using AdminWebBenhVien.EfModels;
using AdminWebBenhVien.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Controllers
{
    public class ControllerBase : Controller
    {

        public ControllerBase(InitParam initParam)
        {
            InitParam = initParam;
        }

        protected InitParam InitParam { get; set; }
    }
}
