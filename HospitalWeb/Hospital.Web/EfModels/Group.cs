using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class Group
    {
        public Group()
        {
            GroupPermission = new HashSet<GroupPermission>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<GroupPermission> GroupPermission { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
