using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class UserPermission
    {
        public string FkUser { get; set; }
        public int FkPermission { get; set; }
        public bool? Allow { get; set; }

        public virtual Permission FkPermissionNavigation { get; set; }
        public virtual User FkUserNavigation { get; set; }
    }
}
