using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class QuocTich
    {
        public QuocTich()
        {
            HenKhamBenh = new HashSet<HenKhamBenh>();
        }

        public int Id { get; set; }
        public string TenQuocGia { get; set; }

        public virtual ICollection<HenKhamBenh> HenKhamBenh { get; set; }
    }
}
