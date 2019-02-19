using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class GroupPermission
    {
        public int FkGroup { get; set; }
        public int FkPermission { get; set; }
        public bool? Allow { get; set; }

        public virtual Group FkGroupNavigation { get; set; }
        public virtual Permission FkPermissionNavigation { get; set; }
    }
}
