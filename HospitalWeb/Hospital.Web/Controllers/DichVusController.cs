using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.EfModels;
using Hospital.Web.Models;

namespace Hospital.Web.Controllers
{
    public class DichVusController : ControllerBase
    {

        public DichVusController(InitParam initParam) : base(initParam)
        {
        }

        // GET: DichVus
        public async Task<IActionResult> Index()
        {
            var lstDichVuChiTiet = await InitParam.Db.DichVuChiTiet.AsNoTracking()
                .Select(t => new DichVuChiTiet
                {
                   Id = t.Id,
                   TenDichVu=t.TenDichVu,
                }).ToListAsync();

            return View(lstDichVuChiTiet);
        }
        // GET: DichVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lstdichVuChiTiet = await InitParam.Db.DichVuChiTiet.AsNoTracking()
                .Include(t => t.FkDichVu)
                .Include(t => t.FkDichVuNavigation)
                .Include(t => t.FkNgonNgu)
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserModify)
                .Include(t => t.FkUserModifyNavigation)
                .Where(t => t.Id == id)
                .Select(t => new DichVuChiTiet
                {
                    Id = t.Id,
                    TenDichVu = t.TenDichVu,
                    GioiThieu = t.GioiThieu,
                    NoiDung = t.NoiDung,
                    HinhAnh = t.HinhAnh,
                    NgayTao = t.NgayTao,
                    LuotXem = t.LuotXem,
                }).FirstOrDefaultAsync();

            if (lstdichVuChiTiet == null)
            {
                return NotFound();
            }

            return View(lstdichVuChiTiet);
        }

        // GET: DichVus/Create
  
    }
}
