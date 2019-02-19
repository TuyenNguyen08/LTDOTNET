using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        public string ReturnUrl { get; set; }

        public string LocalIp { get; set; }
    }
}
