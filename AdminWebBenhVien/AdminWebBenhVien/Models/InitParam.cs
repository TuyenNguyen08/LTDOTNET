using AdminWebBenhVien.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
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
