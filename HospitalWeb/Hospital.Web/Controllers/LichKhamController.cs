using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

            #region lsPhongKham 

            var lsPhongKham = await InitParam.Db.PhongKham.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
               .Select(t => new PhongKham
               {
                   Id = t.Id,
                   TenPhongKham = t.TenPhongKham
               }).ToListAsync();

            ViewBag.listPhongKham = lsPhongKham;

            #endregion

            #region lsLichNgay

            var lsLichNgay = await InitParam.Db.LichNgay.AsNoTracking()
              .Where(h => h.FkNgonNgu == fkNgonNgu)
               .Select(t => new LichNgay
               {
                   Id = t.Id,
                   TenThu = t.TenThu
               }).ToListAsync();

            ViewBag.listLichNgay = lsLichNgay;


            #endregion

            #region lslichKham
            var lslichKham = await InitParam.Db.LichKham.AsNoTracking()
             .Select(t => new LichKham
             {
                 Sang = t.Sang,
                 Chieu = t.Chieu,
                 FkLichNgay = t.FkLichNgay,
                 FkPhongKham = t.FkPhongKham
             })
             .ToListAsync();

            #endregion

            return View(lslichKham);
        }
    }
}