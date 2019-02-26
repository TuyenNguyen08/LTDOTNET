using Hospital.Web.EfModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Web.Models
{
    public class InitParam
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InitParam(NBenhVien7CContext context, IHttpContextAccessor httpContextAccessor)
        {
            Db = context;
            HttpContextAccessor = httpContextAccessor;
        }

        public NBenhVien7CContext Db { get; private set; }
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
    }
}
