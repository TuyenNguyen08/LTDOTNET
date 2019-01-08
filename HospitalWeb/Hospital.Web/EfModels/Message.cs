using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class Message
    {
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public string Keyword { get; set; }
        public int? FkNgonNgu { get; set; }
        public string Description { get; set; }

        public virtual NgonNgu FkNgonNguNavigation { get; set; }
    }
}
