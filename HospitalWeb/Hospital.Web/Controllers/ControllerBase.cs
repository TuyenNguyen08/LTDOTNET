using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected List<NgonNgu> _listNgonNgu;

        public ControllerBase(InitParam initParam)
        {
            InitParam = initParam;

            _listNgonNgu = InitParam.Db.NgonNgu.AsNoTracking().ToList();

            var ngonNgu = InitParam.HttpContextAccessor.HttpContext.Session.GetInt32("NgonNgu");

            NgonNgu = -1;
            if (ngonNgu.HasValue)
            {
                if (_listNgonNgu.Any(h => h.Id == ngonNgu))
                {
                    NgonNgu = ngonNgu.Value;
                }
                else if (_listNgonNgu.Count > 0)
                {
                    NgonNgu = _listNgonNgu[0].Id;
                }
            }
            else if (_listNgonNgu.Count > 0)
            {
                NgonNgu = _listNgonNgu[0].Id;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.ListNgonNgu = _listNgonNgu;
            ViewBag.NgonNgu = NgonNgu;
            base.OnActionExecuting(context);

        }

        protected InitParam InitParam { get; set; }
        protected int NgonNgu { get; set; }
    }
}
