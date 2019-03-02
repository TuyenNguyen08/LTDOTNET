using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers
{
    public class LienHeController : ControllerBase
    {

        public LienHeController(InitParam initParam) : base(initParam)
        {

        }
      
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LienHeKhachHang lienHeKhachHang)
        {
           if (ModelState.IsValid)
            {
                LienHeKhachHang model = new LienHeKhachHang();
                model.HoVaTen = lienHeKhachHang.HoVaTen;
                model.NoiDung = lienHeKhachHang.NoiDung;
                model.TieuDe = lienHeKhachHang.TieuDe;
                model.DienThoaiDiDong = lienHeKhachHang.DienThoaiDiDong;
                model.Email = lienHeKhachHang.Email;
                await InitParam.Db.AddAsync(model);
                await InitParam.Db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));


        }
    }
}