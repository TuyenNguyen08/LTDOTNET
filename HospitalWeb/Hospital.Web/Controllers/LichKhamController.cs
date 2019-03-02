using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

            var lsLichNgay = await InitParam.Db.LichNgay.AsNoTracking().Take(6)
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

            var litsLichKham = new List<LichKhamVm>();
            foreach (var itemPhongKham in lsPhongKham)
            {
                litsLichKham.Add(new LichKhamVm { Loai = "TieuDe"});
                var flag = true;

                foreach (var itemNgay in lsLichNgay)
                {
                    var ngay = lslichKham.FirstOrDefault( c => c.FkPhongKham == itemPhongKham.Id && c.FkLichNgay == itemNgay.Id);
                    var lichKham = new LichKhamVm {
                        Loai = flag ? "PhongKham" : "",
                        PhongKham = itemPhongKham.TenPhongKham,
                        Ngay = itemNgay.TenThu,
                        Sang = ngay.Sang,
                        Chieu = ngay.Chieu
                    };
                    litsLichKham.Add(lichKham);
                    flag = false;
                }
            }

            return View(litsLichKham);
        }
        
    }
    public class LichKhamVm {

        public string Loai { get; set; }

        public string PhongKham { get; set; }
        public string Ngay { get; set; }

        public string Sang { get; set; }

        public string Chieu { get; set; }

    }

}