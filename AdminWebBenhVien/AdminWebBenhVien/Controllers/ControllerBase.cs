using AdminWebBenhVien.EfModels;
using AdminWebBenhVien.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var listNgonNgu = InitParam.Db.NgonNgu.AsNoTracking()
                .OrderBy(h => h.TenNgonNgu)
                .Select(h => new NgonNgu
                {
                    Id = h.Id,
                    TenNgonNgu = h.TenNgonNgu
                }).ToList();

            ListNgonNgu = listNgonNgu;
        }

        protected InitParam InitParam { get; set; }
        protected List<NgonNgu> ListNgonNgu { get; set; }
    }
}
