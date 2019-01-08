using Hospital.Web.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Web.Models
{
    public class InitParam
    {
        public InitParam(NBenhVien7CContext context)
        {
            Db = context;
        }

        public NBenhVien7CContext Db { get; private set; }
    }
}
