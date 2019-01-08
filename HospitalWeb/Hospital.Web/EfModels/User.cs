using System;
using System.Collections.Generic;

namespace Hospital.Web.EfModels
{
    public partial class User
    {
        public User()
        {
            DichVuChiTiet = new HashSet<DichVuChiTiet>();
            Event = new HashSet<Event>();
            GioiThieuChiTiet = new HashSet<GioiThieuChiTiet>();
            HoatDongFkNguoiSuaNavigation = new HashSet<HoatDong>();
            HoatDongFkNguoiTaoNavigation = new HashSet<HoatDong>();
            HoiDapYkhoa = new HashSet<HoiDapYkhoa>();
            LienHeKhachHang = new HashSet<LienHeKhachHang>();
            NoiDungTinhChiTiet = new HashSet<NoiDungTinhChiTiet>();
            PhanHoi = new HashSet<PhanHoi>();
            TinTucFkUserNguoiSuaNavigation = new HashSet<TinTuc>();
            TinTucFkUserNguoiTaoNavigation = new HashSet<TinTuc>();
            TuyenDungThongBao = new HashSet<TuyenDungThongBao>();
            UserPermission = new HashSet<UserPermission>();
        }

        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string HoVaTen { get; set; }
        public int? FkKhoaPhong { get; set; }
        public int? FkGroup { get; set; }
        public DateTime? NearLastLogin { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? Active { get; set; }
        public string Email { get; set; }

        public virtual Group FkGroupNavigation { get; set; }
        public virtual KhoaPhong FkKhoaPhongNavigation { get; set; }
        public virtual ICollection<DichVuChiTiet> DichVuChiTiet { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<GioiThieuChiTiet> GioiThieuChiTiet { get; set; }
        public virtual ICollection<HoatDong> HoatDongFkNguoiSuaNavigation { get; set; }
        public virtual ICollection<HoatDong> HoatDongFkNguoiTaoNavigation { get; set; }
        public virtual ICollection<HoiDapYkhoa> HoiDapYkhoa { get; set; }
        public virtual ICollection<LienHeKhachHang> LienHeKhachHang { get; set; }
        public virtual ICollection<NoiDungTinhChiTiet> NoiDungTinhChiTiet { get; set; }
        public virtual ICollection<PhanHoi> PhanHoi { get; set; }
        public virtual ICollection<TinTuc> TinTucFkUserNguoiSuaNavigation { get; set; }
        public virtual ICollection<TinTuc> TinTucFkUserNguoiTaoNavigation { get; set; }
        public virtual ICollection<TuyenDungThongBao> TuyenDungThongBao { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}
