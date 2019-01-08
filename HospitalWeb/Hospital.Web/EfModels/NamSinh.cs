using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class NamSinh
    {
        public NamSinh()
        {
            HenKhamBenh = new HashSet<HenKhamBenh>();
            HoiDapYkhoa = new HashSet<HoiDapYkhoa>();
        }

        public int Id { get; set; }
        public int? Nam { get; set; }

        public virtual ICollection<HenKhamBenh> HenKhamBenh { get; set; }
        public virtual ICollection<HoiDapYkhoa> HoiDapYkhoa { get; set; }
    }
}
