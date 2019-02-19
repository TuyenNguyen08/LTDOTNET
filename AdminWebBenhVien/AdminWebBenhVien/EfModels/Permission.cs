using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class Permission
    {
        public Permission()
        {
            GroupPermission = new HashSet<GroupPermission>();
            UserPermission = new HashSet<UserPermission>();
        }

        public int Idpermission { get; set; }
        public string PermissionName { get; set; }
        public string PageName { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<GroupPermission> GroupPermission { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}
