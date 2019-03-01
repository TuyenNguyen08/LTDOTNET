using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Web.Controllers
{
    public class PhanHoiController : ControllerBase
    {
        public PhanHoiController(InitParam initParam) : base(initParam)
        {
        }

        public async Task<IActionResult> Feedback()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Feedback(PhanHoi ph)
        {
            if (ModelState.IsValid)
            {
                PhanHoi model = new PhanHoi();
                model.NgayKham = DateTime.Now;
                model.HoVaTen = ph.HoVaTen;
                model.DienThoai = ph.DienThoai;
                model.NoiDung = ph.NoiDung;
                await InitParam.Db.PhanHoi.AddAsync(model);
                await InitParam.Db.SaveChangesAsync();
            }
            return RedirectToAction("Feedback");
        }
    }
}