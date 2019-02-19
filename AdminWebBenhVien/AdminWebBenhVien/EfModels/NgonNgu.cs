using System;
using System.Collections.Generic;

namespace AdminWebBenhVien.EfModels
{
    public partial class NgonNgu
    {
        public NgonNgu()
        {
            DichVuChiTiet = new HashSet<DichVuChiTiet>();
            Event = new HashSet<Event>();
            GioiThieuChiTiet = new HashSet<GioiThieuChiTiet>();
            HoatDong = new HashSet<HoatDong>();
            ImagePage = new HashSet<ImagePage>();
            KhoaPhong = new HashSet<KhoaPhong>();
            LichNgay = new HashSet<LichNgay>();
            LienKetWebSite = new HashSet<LienKetWebSite>();
            LoaiCauHoi = new HashSet<LoaiCauHoi>();
            LoaiTuyenDung = new HashSet<LoaiTuyenDung>();
            Menu = new HashSet<Menu>();
            Message = new HashSet<Message>();
            NoiDungTinhChiTiet = new HashSet<NoiDungTinhChiTiet>();
            PhanHoi = new HashSet<PhanHoi>();
            PhongKham = new HashSet<PhongKham>();
            SlideShow = new HashSet<SlideShow>();
            SubMenu = new HashSet<SubMenu>();
            SubNote = new HashSet<SubNote>();
            SubPhone = new HashSet<SubPhone>();
            TinTuc = new HashSet<TinTuc>();
            TinhTrangHonNhan = new HashSet<TinhTrangHonNhan>();
            TuyenDungThongBao = new HashSet<TuyenDungThongBao>();
            Video = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string TenNgonNgu { get; set; }
        public string Yahoo { get; set; }
        public string Skype { get; set; }
        public string TenBenhVien { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string HeadDangKy { get; set; }
        public string HeadDangNhap { get; set; }
        public string HeadSoDo { get; set; }
        public string Flat { get; set; }
        public string HeadButton { get; set; }
        public string PageTitle { get; set; }
        public string SXemTiep { get; set; }
        public string STinMoiNhat { get; set; }
        public string SLienHeKhachHang { get; set; }
        public string STen { get; set; }
        public string SDienThoai { get; set; }
        public string SDiaChi { get; set; }
        public string SNoiDung { get; set; }
        public string SMaBaoVe { get; set; }
        public string SGui { get; set; }
        public string SHuy { get; set; }
        public string SHoTro { get; set; }
        public string SSuKien { get; set; }
        public string SGuiCauHoi { get; set; }
        public string SCauHoi { get; set; }
        public string SCauTraLoi { get; set; }
        public string SCauHoiKhac { get; set; }
        public string STinTucKhac { get; set; }
        public string SDatLichKham { get; set; }
        public string KeyWord { get; set; }
        public string Description { get; set; }
        public string TieuDeLichKham { get; set; }
        public string Sang { get; set; }
        public string Chieu { get; set; }
        public string TieuDeNgay { get; set; }

        public virtual ICollection<DichVuChiTiet> DichVuChiTiet { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<GioiThieuChiTiet> GioiThieuChiTiet { get; set; }
        public virtual ICollection<HoatDong> HoatDong { get; set; }
        public virtual ICollection<ImagePage> ImagePage { get; set; }
        public virtual ICollection<KhoaPhong> KhoaPhong { get; set; }
        public virtual ICollection<LichNgay> LichNgay { get; set; }
        public virtual ICollection<LienKetWebSite> LienKetWebSite { get; set; }
        public virtual ICollection<LoaiCauHoi> LoaiCauHoi { get; set; }
        public virtual ICollection<LoaiTuyenDung> LoaiTuyenDung { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<NoiDungTinhChiTiet> NoiDungTinhChiTiet { get; set; }
        public virtual ICollection<PhanHoi> PhanHoi { get; set; }
        public virtual ICollection<PhongKham> PhongKham { get; set; }
        public virtual ICollection<SlideShow> SlideShow { get; set; }
        public virtual ICollection<SubMenu> SubMenu { get; set; }
        public virtual ICollection<SubNote> SubNote { get; set; }
        public virtual ICollection<SubPhone> SubPhone { get; set; }
        public virtual ICollection<TinTuc> TinTuc { get; set; }
        public virtual ICollection<TinhTrangHonNhan> TinhTrangHonNhan { get; set; }
        public virtual ICollection<TuyenDungThongBao> TuyenDungThongBao { get; set; }
        public virtual ICollection<Video> Video { get; set; }
    }
}
