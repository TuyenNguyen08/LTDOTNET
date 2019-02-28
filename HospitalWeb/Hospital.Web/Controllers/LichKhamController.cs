using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Hospital.Web.Controllers
{
    public class LichKhamController : ControllerBase
    {
        public LichKhamController(InitParam initParam) : base(initParam)
        {
        }

        public async Task<IActionResult> Index()
        {
            var fkNgonNgu = base.NgonNgu;
            
            return View();
        }
    }
}