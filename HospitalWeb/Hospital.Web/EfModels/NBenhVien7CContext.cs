using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hospital.Web.EfModels
{
    public partial class NBenhVien7CContext : DbContext
    {
        public NBenhVien7CContext()
        {
        }

        public NBenhVien7CContext(DbContextOptions<NBenhVien7CContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DanhMucBacSi> DanhMucBacSi { get; set; }
        public virtual DbSet<DichVu> DichVu { get; set; }
        public virtual DbSet<DichVuChiTiet> DichVuChiTiet { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<GioKham> GioKham { get; set; }
        public virtual DbSet<GioiThieu> GioiThieu { get; set; }
        public virtual DbSet<GioiThieuChiTiet> GioiThieuChiTiet { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupPermission> GroupPermission { get; set; }
        public virtual DbSet<HenKhamBenh> HenKhamBenh { get; set; }
        public virtual DbSet<HoatDong> HoatDong { get; set; }
        public virtual DbSet<HoiDapYkhoa> HoiDapYkhoa { get; set; }
        public virtual DbSet<ImagePage> ImagePage { get; set; }
        public virtual DbSet<KhoaPhong> KhoaPhong { get; set; }
        public virtual DbSet<LichKham> LichKham { get; set; }
        public virtual DbSet<LichNgay> LichNgay { get; set; }
        public virtual DbSet<LienHeKhachHang> LienHeKhachHang { get; set; }
        public virtual DbSet<LienKetWebSite> LienKetWebSite { get; set; }
        public virtual DbSet<LoaiCauHoi> LoaiCauHoi { get; set; }
        public virtual DbSet<LoaiHoatDong> LoaiHoatDong { get; set; }
        public virtual DbSet<LoaiKhoaPhong> LoaiKhoaPhong { get; set; }
        public virtual DbSet<LoaiTin> LoaiTin { get; set; }
        public virtual DbSet<LoaiTuyenDung> LoaiTuyenDung { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuLeft> MenuLeft { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<NamSinh> NamSinh { get; set; }
        public virtual DbSet<NgonNgu> NgonNgu { get; set; }
        public virtual DbSet<NoiDungTinh> NoiDungTinh { get; set; }
        public virtual DbSet<NoiDungTinhChiTiet> NoiDungTinhChiTiet { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageOfMenu> PageOfMenu { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PhanHoi> PhanHoi { get; set; }
        public virtual DbSet<PhongKham> PhongKham { get; set; }
        public virtual DbSet<QuocTich> QuocTich { get; set; }
        public virtual DbSet<SlideShow> SlideShow { get; set; }
        public virtual DbSet<SubMenu> SubMenu { get; set; }
        public virtual DbSet<SubNote> SubNote { get; set; }
        public virtual DbSet<SubPhone> SubPhone { get; set; }
        public virtual DbSet<ThamSo> ThamSo { get; set; }
        public virtual DbSet<TinTuc> TinTuc { get; set; }
        public virtual DbSet<TinhTrangHonNhan> TinhTrangHonNhan { get; set; }
        public virtual DbSet<TrangThai> TrangThai { get; set; }
        public virtual DbSet<TuyenDungThongBao> TuyenDungThongBao { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPermission> UserPermission { get; set; }
        public virtual DbSet<Video> Video { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SERVER27\\SQLEXPRESS;Database=NBenhVien7C;User Id=sa;Password=1@qweQAZ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<DanhMucBacSi>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkKhoaPhong).HasColumnName("FK_KhoaPhong");

                entity.Property(e => e.TenBacSi).HasMaxLength(100);

                entity.HasOne(d => d.FkKhoaPhongNavigation)
                    .WithMany(p => p.DanhMucBacSi)
                    .HasForeignKey(d => d.FkKhoaPhong)
                    .HasConstraintName("FK_DanhMucBacSi_KhoaPhong");
            });

            modelBuilder.Entity<DichVu>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MoTa).HasMaxLength(100);
            });

            modelBuilder.Entity<DichVuChiTiet>(entity =>
            {
                entity.HasKey(e => new { e.FkNgonNgu, e.FkDichVu });

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.FkDichVu).HasColumnName("FK_DichVu");

                entity.Property(e => e.FkUserModify)
                    .HasColumnName("FK_UserModify")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhAnh).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LuotXem).HasDefaultValueSql("((1))");

                entity.Property(e => e.NgayChinhSua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.TenDichVu).HasMaxLength(100);

                entity.HasOne(d => d.FkDichVuNavigation)
                    .WithMany(p => p.DichVuChiTiet)
                    .HasForeignKey(d => d.FkDichVu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DichVuChiTiet_DichVu");

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.DichVuChiTiet)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DichVuChiTiet_NgonNgu");

                entity.HasOne(d => d.FkUserModifyNavigation)
                    .WithMany(p => p.DichVuChiTiet)
                    .HasForeignKey(d => d.FkUserModify)
                    .HasConstraintName("FK_DichVuChiTiet_User");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.FkUserTao)
                    .HasColumnName("FK_UserTao")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhAnh).HasMaxLength(100);

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TieuDe).HasMaxLength(50);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_Event_NgonNgu");

                entity.HasOne(d => d.FkUserTaoNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.FkUserTao)
                    .HasConstraintName("FK_Event_User");
            });

            modelBuilder.Entity<GioKham>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Gio)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GioiThieu>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TenGioiThieu).HasMaxLength(50);
            });

            modelBuilder.Entity<GioiThieuChiTiet>(entity =>
            {
                entity.HasKey(e => new { e.FkNgonNgu, e.FkGioiThieu });

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.FkGioiThieu).HasColumnName("FK_GioiThieu");

                entity.Property(e => e.FkUserModify)
                    .HasColumnName("FK_UserModify")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhAnh).HasMaxLength(250);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LuotXem).HasDefaultValueSql("((1))");

                entity.Property(e => e.NgayChinhSua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.Ten).HasMaxLength(100);

                entity.HasOne(d => d.FkGioiThieuNavigation)
                    .WithMany(p => p.GioiThieuChiTiet)
                    .HasForeignKey(d => d.FkGioiThieu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GioiThieuChiTiet_GioiThieu");

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.GioiThieuChiTiet)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GioiThieuChiTiet_NgonNgu");

                entity.HasOne(d => d.FkUserModifyNavigation)
                    .WithMany(p => p.GioiThieuChiTiet)
                    .HasForeignKey(d => d.FkUserModify)
                    .HasConstraintName("FK_GioiThieuChiTiet_User");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupName).HasMaxLength(100);
            });

            modelBuilder.Entity<GroupPermission>(entity =>
            {
                entity.HasKey(e => new { e.FkGroup, e.FkPermission });

                entity.Property(e => e.FkGroup).HasColumnName("FK_Group");

                entity.Property(e => e.FkPermission).HasColumnName("FK_Permission");

                entity.HasOne(d => d.FkGroupNavigation)
                    .WithMany(p => p.GroupPermission)
                    .HasForeignKey(d => d.FkGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPermission_Group");

                entity.HasOne(d => d.FkPermissionNavigation)
                    .WithMany(p => p.GroupPermission)
                    .HasForeignKey(d => d.FkPermission)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupPermission_Permission");
            });

            modelBuilder.Entity<HenKhamBenh>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BacSi).HasMaxLength(200);

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.DiaChiEmail).HasMaxLength(50);

                entity.Property(e => e.FkBacSi).HasColumnName("FK_BacSi");

                entity.Property(e => e.FkChuyenKhoa).HasColumnName("FK_ChuyenKhoa");

                entity.Property(e => e.FkGioHen).HasColumnName("FK_GioHen");

                entity.Property(e => e.FkNamSinh).HasColumnName("FK_NamSinh");

                entity.Property(e => e.FkQuocTich).HasColumnName("FK_QuocTich");

                entity.Property(e => e.FkTinhTrangHonNhan).HasColumnName("FK_TinhTrangHonNhan");

                entity.Property(e => e.FkTrangThai).HasColumnName("FK_TrangThai");

                entity.Property(e => e.HoVaTen).HasMaxLength(100);

                entity.Property(e => e.NgayGui).HasColumnType("datetime");

                entity.Property(e => e.NgayHen).HasColumnType("datetime");

                entity.Property(e => e.SoDienThoaiDiDong)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoaiNha)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkBacSiNavigation)
                    .WithMany(p => p.HenKhamBenh)
                    .HasForeignKey(d => d.FkBacSi)
                    .HasConstraintName("FK_HenKhamBenh_DanhMucBacSi");

                entity.HasOne(d => d.FkChuyenKhoaNavigation)
                    .WithMany(p => p.HenKhamBenh)
                    .HasForeignKey(d => d.FkChuyenKhoa)
                    .HasConstraintName("FK_HenKhamBenh_PhongKham");

                entity.HasOne(d => d.FkGioHenNavigation)
                    .WithMany(p => p.HenKhamBenh)
                    .HasForeignKey(d => d.FkGioHen)
                    .HasConstraintName("FK_HenKhamBenh_GioKham");

                entity.HasOne(d => d.FkNamSinhNavigation)
                    .WithMany(p => p.HenKhamBenh)
                    .HasForeignKey(d => d.FkNamSinh)
                    .HasConstraintName("FK_HenKhamBenh_NamSinh");

                entity.HasOne(d => d.FkQuocTichNavigation)
                    .WithMany(p => p.HenKhamBenh)
                    .HasForeignKey(d => d.FkQuocTich)
                    .HasConstraintName("FK_HenKhamBenh_QuocTich");

                entity.HasOne(d => d.FkTinhTrangHonNhanNavigation)
                    .WithMany(p => p.HenKhamBenh)
                    .HasForeignKey(d => d.FkTinhTrangHonNhan)
                    .HasConstraintName("FK_HenKhamBenh_TinhTrangHonNhan");

                entity.HasOne(d => d.FkTrangThaiNavigation)
                    .WithMany(p => p.HenKhamBenh)
                    .HasForeignKey(d => d.FkTrangThai)
                    .HasConstraintName("FK_HenKhamBenh_TrangThai");
            });

            modelBuilder.Entity<HoatDong>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Author).HasMaxLength(100);

                entity.Property(e => e.FkLoaiHoatDong).HasColumnName("FK_LoaiHoatDong");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.FkNguoiSua)
                    .HasColumnName("FK_NguoiSua")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FkNguoiTao)
                    .HasColumnName("FK_NguoiTao")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhAnhMinhHoa).HasMaxLength(200);

                entity.Property(e => e.LuotXem).HasDefaultValueSql("((1))");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkLoaiHoatDongNavigation)
                    .WithMany(p => p.HoatDong)
                    .HasForeignKey(d => d.FkLoaiHoatDong)
                    .HasConstraintName("FK_HoatDong_LoaiHoatDong");

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.HoatDong)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_HoatDong_NgonNgu");

                entity.HasOne(d => d.FkNguoiSuaNavigation)
                    .WithMany(p => p.HoatDongFkNguoiSuaNavigation)
                    .HasForeignKey(d => d.FkNguoiSua)
                    .HasConstraintName("FK_HoatDong_UserNguoiSua");

                entity.HasOne(d => d.FkNguoiTaoNavigation)
                    .WithMany(p => p.HoatDongFkNguoiTaoNavigation)
                    .HasForeignKey(d => d.FkNguoiTao)
                    .HasConstraintName("FK_HoatDong_User");
            });

            modelBuilder.Entity<HoiDapYkhoa>(entity =>
            {
                entity.ToTable("HoiDapYKhoa");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiaChi).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FkChuyenKhoa).HasColumnName("FK_ChuyenKhoa");

                entity.Property(e => e.FkNamSinh).HasColumnName("FK_NamSinh");

                entity.Property(e => e.FkUserTraLoi)
                    .HasColumnName("FK_UserTraLoi")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HoTenNguoiHoi).HasMaxLength(100);

                entity.Property(e => e.NgayGuiCauHoi).HasColumnType("datetime");

                entity.Property(e => e.NgayTraLoi).HasColumnType("datetime");

                entity.Property(e => e.NguoiTraLoi).HasMaxLength(100);

                entity.Property(e => e.SoDienThoai).HasMaxLength(50);

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkChuyenKhoaNavigation)
                    .WithMany(p => p.HoiDapYkhoa)
                    .HasForeignKey(d => d.FkChuyenKhoa)
                    .HasConstraintName("FK_HoiDapYKhoa_LoaiCauHoi");

                entity.HasOne(d => d.FkNamSinhNavigation)
                    .WithMany(p => p.HoiDapYkhoa)
                    .HasForeignKey(d => d.FkNamSinh)
                    .HasConstraintName("FK_HoiDapYKhoa_NamSinh");

                entity.HasOne(d => d.FkUserTraLoiNavigation)
                    .WithMany(p => p.HoiDapYkhoa)
                    .HasForeignKey(d => d.FkUserTraLoi)
                    .HasConstraintName("FK_HoiDapYKhoa_User");
            });

            modelBuilder.Entity<ImagePage>(entity =>
            {
                entity.HasKey(e => new { e.FkNgonNgu, e.PageName });

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.PageName).HasMaxLength(50);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ImagePath).HasMaxLength(255);

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.ImagePage)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImagePage_NgonNgu");
            });

            modelBuilder.Entity<KhoaPhong>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkLoaiKhoaPhong).HasColumnName("FK_LoaiKhoaPhong");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhAnhDaiDien).HasMaxLength(100);

                entity.Property(e => e.LuotXem).HasDefaultValueSql("((1))");

                entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");

                entity.Property(e => e.Stt)
                    .HasColumnName("STT")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TenKhoaPhong).HasMaxLength(100);

                entity.Property(e => e.TieuDeKhoa).HasMaxLength(50);

                entity.Property(e => e.UserModify)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkLoaiKhoaPhongNavigation)
                    .WithMany(p => p.KhoaPhong)
                    .HasForeignKey(d => d.FkLoaiKhoaPhong)
                    .HasConstraintName("FK_KhoaPhong_LoaiKhoaPhong");

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.KhoaPhong)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_KhoaPhong_NgonNgu");
            });

            modelBuilder.Entity<LichKham>(entity =>
            {
                entity.HasKey(e => new { e.FkLichNgay, e.FkPhongKham });

                entity.Property(e => e.FkLichNgay).HasColumnName("FK_LichNgay");

                entity.Property(e => e.FkPhongKham).HasColumnName("FK_PhongKham");

                entity.Property(e => e.Chieu).HasMaxLength(100);

                entity.Property(e => e.Sang).HasMaxLength(100);

                entity.HasOne(d => d.FkLichNgayNavigation)
                    .WithMany(p => p.LichKham)
                    .HasForeignKey(d => d.FkLichNgay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichKham_LichNgay");

                entity.HasOne(d => d.FkPhongKhamNavigation)
                    .WithMany(p => p.LichKham)
                    .HasForeignKey(d => d.FkPhongKham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichKham_PhongKham");
            });

            modelBuilder.Entity<LichNgay>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.TenThu).HasMaxLength(50);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.LichNgay)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_LichNgay_NgonNgu");
            });

            modelBuilder.Entity<LienHeKhachHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DienThoaiDiDong)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DienThoaiNha)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkNguoiXem)
                    .HasColumnName("FK_NguoiXem")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HoVaTen).HasMaxLength(100);

                entity.Property(e => e.NgayGui).HasColumnType("datetime");

                entity.Property(e => e.NgayXem).HasColumnType("datetime");

                entity.Property(e => e.TieuDe).HasMaxLength(500);

                entity.HasOne(d => d.FkNguoiXemNavigation)
                    .WithMany(p => p.LienHeKhachHang)
                    .HasForeignKey(d => d.FkNguoiXem)
                    .HasConstraintName("FK_LienHeKhachHang_User");
            });

            modelBuilder.Entity<LienKetWebSite>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.Link).HasMaxLength(200);

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.LienKetWebSite)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_LienKetWebSite_NgonNgu");
            });

            modelBuilder.Entity<LoaiCauHoi>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.TenLoai).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.LoaiCauHoi)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_LoaiCauHoi_NgonNgu");
            });

            modelBuilder.Entity<LoaiHoatDong>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiKhoaPhong>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TenLoai).HasMaxLength(100);
            });

            modelBuilder.Entity<LoaiTin>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TenLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiTuyenDung>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.TenLoai).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.LoaiTuyenDung)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_LoaiTuyenDung_NgonNgu");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.Idparrent).HasColumnName("IDParrent");

                entity.Property(e => e.Link).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PageName).HasMaxLength(50);

                entity.Property(e => e.Root).HasColumnName("root");

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_NgonNgu");
            });

            modelBuilder.Entity<MenuLeft>(entity =>
            {
                entity.HasKey(e => e.Idmenu);

                entity.Property(e => e.Idmenu).HasColumnName("IDMenu");

                entity.Property(e => e.Idparrent).HasColumnName("IDParrent");

                entity.Property(e => e.Link).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.Keyword)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_Message_NgonNgu");
            });

            modelBuilder.Entity<NamSinh>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<NgonNgu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Chieu).HasMaxLength(10);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.DienThoai).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Flat).HasMaxLength(50);

                entity.Property(e => e.HeadButton)
                    .HasColumnName("Head_Button")
                    .HasMaxLength(50);

                entity.Property(e => e.HeadDangKy)
                    .HasColumnName("Head_DangKy")
                    .HasMaxLength(50);

                entity.Property(e => e.HeadDangNhap)
                    .HasColumnName("Head_DangNhap")
                    .HasMaxLength(50);

                entity.Property(e => e.HeadSoDo)
                    .HasColumnName("Head_SoDo")
                    .HasMaxLength(50);

                entity.Property(e => e.KeyWord).HasMaxLength(500);

                entity.Property(e => e.PageTitle).HasMaxLength(50);

                entity.Property(e => e.SCauHoi)
                    .HasColumnName("sCauHoi")
                    .HasMaxLength(50);

                entity.Property(e => e.SCauHoiKhac)
                    .HasColumnName("sCauHoiKhac")
                    .HasMaxLength(50);

                entity.Property(e => e.SCauTraLoi)
                    .HasColumnName("sCauTraLoi")
                    .HasMaxLength(50);

                entity.Property(e => e.SDatLichKham)
                    .HasColumnName("sDatLichKham")
                    .HasMaxLength(50);

                entity.Property(e => e.SDiaChi)
                    .HasColumnName("sDiaChi")
                    .HasMaxLength(50);

                entity.Property(e => e.SDienThoai)
                    .HasColumnName("sDienThoai")
                    .HasMaxLength(50);

                entity.Property(e => e.SGui)
                    .HasColumnName("sGui")
                    .HasMaxLength(50);

                entity.Property(e => e.SGuiCauHoi)
                    .HasColumnName("sGuiCauHoi")
                    .HasMaxLength(50);

                entity.Property(e => e.SHoTro)
                    .HasColumnName("sHoTro")
                    .HasMaxLength(50);

                entity.Property(e => e.SHuy)
                    .HasColumnName("sHuy")
                    .HasMaxLength(50);

                entity.Property(e => e.SLienHeKhachHang)
                    .HasColumnName("sLienHeKhachHang")
                    .HasMaxLength(100);

                entity.Property(e => e.SMaBaoVe)
                    .HasColumnName("sMaBaoVe")
                    .HasMaxLength(50);

                entity.Property(e => e.SNoiDung)
                    .HasColumnName("sNoiDung")
                    .HasMaxLength(50);

                entity.Property(e => e.SSuKien)
                    .HasColumnName("sSuKien")
                    .HasMaxLength(50);

                entity.Property(e => e.STen)
                    .HasColumnName("sTen")
                    .HasMaxLength(50);

                entity.Property(e => e.STinMoiNhat)
                    .HasColumnName("sTinMoiNhat")
                    .HasMaxLength(50);

                entity.Property(e => e.STinTucKhac)
                    .HasColumnName("sTinTucKhac")
                    .HasMaxLength(50);

                entity.Property(e => e.SXemTiep)
                    .HasColumnName("sXemTiep")
                    .HasMaxLength(50);

                entity.Property(e => e.Sang).HasMaxLength(10);

                entity.Property(e => e.Skype)
                    .HasColumnName("skype")
                    .HasMaxLength(50);

                entity.Property(e => e.TenBenhVien).HasMaxLength(100);

                entity.Property(e => e.TenNgonNgu).HasMaxLength(50);

                entity.Property(e => e.TieuDeLichKham).HasMaxLength(100);

                entity.Property(e => e.TieuDeNgay).HasMaxLength(10);

                entity.Property(e => e.Yahoo)
                    .HasColumnName("yahoo")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NoiDungTinh>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TenNoiDung).HasMaxLength(100);
            });

            modelBuilder.Entity<NoiDungTinhChiTiet>(entity =>
            {
                entity.HasKey(e => new { e.FkNgonNgu, e.FkNoiDungTinh });

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.FkNoiDungTinh).HasColumnName("FK_NoiDungTinh");

                entity.Property(e => e.FkUserChinhsua)
                    .HasColumnName("FK_UserChinhsua")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhAnh).HasMaxLength(250);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LuotXem).HasDefaultValueSql("((1))");

                entity.Property(e => e.NgayChinhSua).HasColumnType("datetime");

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.NoiDungTinhChiTiet)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoiDungTinhChiTiet_NgonNgu");

                entity.HasOne(d => d.FkNoiDungTinhNavigation)
                    .WithMany(p => p.NoiDungTinhChiTiet)
                    .HasForeignKey(d => d.FkNoiDungTinh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoiDungTinhChiTiet_NoiDungTinh");

                entity.HasOne(d => d.FkUserChinhsuaNavigation)
                    .WithMany(p => p.NoiDungTinhChiTiet)
                    .HasForeignKey(d => d.FkUserChinhsua)
                    .HasConstraintName("FK_NoiDungTinhChiTiet_User");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(e => e.Idpage)
                    .HasName("PK_PageMenu");

                entity.Property(e => e.Idpage).HasColumnName("IDPage");

                entity.Property(e => e.FkMenu).HasColumnName("FK_Menu");

                entity.Property(e => e.NamePage).HasMaxLength(50);

                entity.HasOne(d => d.FkMenuNavigation)
                    .WithMany(p => p.Page)
                    .HasForeignKey(d => d.FkMenu)
                    .HasConstraintName("FK_Page_Menu");
            });

            modelBuilder.Entity<PageOfMenu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkMenu).HasColumnName("FK_Menu");

                entity.Property(e => e.NamePage).HasMaxLength(100);

                entity.HasOne(d => d.FkMenuNavigation)
                    .WithMany(p => p.PageOfMenu)
                    .HasForeignKey(d => d.FkMenu)
                    .HasConstraintName("FK_PageOfMenu_MenuLeft");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Idpermission);

                entity.Property(e => e.Idpermission).HasColumnName("IDPermission");

                entity.Property(e => e.PageName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PermissionName).HasMaxLength(100);
            });

            modelBuilder.Entity<PhanHoi>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiaChi).HasMaxLength(300);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FkLoaiPhanHoi).HasColumnName("FK_LoaiPhanHoi");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.FkUserTraLoi)
                    .HasColumnName("FK_UserTraLoi")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HoVaTen).HasMaxLength(100);

                entity.Property(e => e.MaSoBenhNhan)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.NgayGui).HasColumnType("datetime");

                entity.Property(e => e.NgayKham).HasColumnType("datetime");

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.NgayTraLoi).HasColumnType("datetime");

                entity.Property(e => e.TieuDe).HasMaxLength(500);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.PhanHoi)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_PhanHoi_NgonNgu");

                entity.HasOne(d => d.FkUserTraLoiNavigation)
                    .WithMany(p => p.PhanHoi)
                    .HasForeignKey(d => d.FkUserTraLoi)
                    .HasConstraintName("FK_PhanHoi_User");
            });

            modelBuilder.Entity<PhongKham>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.GhiChuThem).HasMaxLength(100);

                entity.Property(e => e.TenPhongKham).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.PhongKham)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_PhongKham_NgonNgu");
            });

            modelBuilder.Entity<QuocTich>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TenQuocGia).HasMaxLength(50);
            });

            modelBuilder.Entity<SlideShow>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.HinhAnh).HasMaxLength(100);

                entity.Property(e => e.LinkEvent).HasMaxLength(200);

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.SlideShow)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_SlideShow_NgonNgu");
            });

            modelBuilder.Entity<SubMenu>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.Link).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.SubMenu)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_SubMenu_NgonNgu");
            });

            modelBuilder.Entity<SubNote>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Link).HasMaxLength(200);

                entity.Property(e => e.NoiDung).HasMaxLength(500);

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.SubNote)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_SubNote_NgonNgu");
            });

            modelBuilder.Entity<SubPhone>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SoDienThoai).HasMaxLength(50);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.SubPhone)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_SubPhone_NgonNgu");
            });

            modelBuilder.Entity<ThamSo>(entity =>
            {
                entity.HasKey(e => e.Khoa);

                entity.Property(e => e.Khoa)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.MatKhau).HasMaxLength(100);
            });

            modelBuilder.Entity<TinTuc>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Author).HasMaxLength(100);

                entity.Property(e => e.FkLoaiTin).HasColumnName("FK_LoaiTin");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.FkUserNguoiSua)
                    .HasColumnName("FK_UserNguoiSua")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FkUserNguoiTao)
                    .HasColumnName("FK_UserNguoiTao")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhAnhMinhHoa).HasMaxLength(200);

                entity.Property(e => e.LuotXem).HasDefaultValueSql("((1))");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkLoaiTinNavigation)
                    .WithMany(p => p.TinTuc)
                    .HasForeignKey(d => d.FkLoaiTin)
                    .HasConstraintName("FK_TinTuc_LoaiTin");

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.TinTuc)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_TinTuc_NgonNgu");

                entity.HasOne(d => d.FkUserNguoiSuaNavigation)
                    .WithMany(p => p.TinTucFkUserNguoiSuaNavigation)
                    .HasForeignKey(d => d.FkUserNguoiSua)
                    .HasConstraintName("FK_TinTuc_UserNguoiSua");

                entity.HasOne(d => d.FkUserNguoiTaoNavigation)
                    .WithMany(p => p.TinTucFkUserNguoiTaoNavigation)
                    .HasForeignKey(d => d.FkUserNguoiTao)
                    .HasConstraintName("FK_TinTuc_User");
            });

            modelBuilder.Entity<TinhTrangHonNhan>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.TinhTrang).HasMaxLength(50);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.TinhTrangHonNhan)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_TinhTrangHonNhan_NgonNgu");
            });

            modelBuilder.Entity<TrangThai>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TenLoaiTrangThai).HasMaxLength(50);
            });

            modelBuilder.Entity<TuyenDungThongBao>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkLoaiTuyenDung).HasColumnName("FK_LoaiTuyenDung");

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNGu");

                entity.Property(e => e.FkNguoiTao)
                    .HasColumnName("FK_NguoiTao")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhMinhHoa).HasMaxLength(100);

                entity.Property(e => e.LuotXem).HasDefaultValueSql("((1))");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.FkLoaiTuyenDungNavigation)
                    .WithMany(p => p.TuyenDungThongBao)
                    .HasForeignKey(d => d.FkLoaiTuyenDung)
                    .HasConstraintName("FK_TuyenDungThongBao_LoaiTuyenDung");

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.TuyenDungThongBao)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .HasConstraintName("FK_TuyenDungThongBao_NgonNgu");

                entity.HasOne(d => d.FkNguoiTaoNavigation)
                    .WithMany(p => p.TuyenDungThongBao)
                    .HasForeignKey(d => d.FkNguoiTao)
                    .HasConstraintName("FK_TuyenDungThongBao_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FkGroup).HasColumnName("FK_Group");

                entity.Property(e => e.FkKhoaPhong).HasColumnName("FK_KhoaPhong");

                entity.Property(e => e.HoVaTen).HasMaxLength(100);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.NearLastLogin).HasColumnType("datetime");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkGroupNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.FkGroup)
                    .HasConstraintName("FK_User_Group");

                entity.HasOne(d => d.FkKhoaPhongNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.FkKhoaPhong)
                    .HasConstraintName("FK_User_KhoaPhong");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.FkUser, e.FkPermission });

                entity.Property(e => e.FkUser)
                    .HasColumnName("FK_User")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FkPermission).HasColumnName("FK_Permission");

                entity.HasOne(d => d.FkPermissionNavigation)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.FkPermission)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermission_Permission");

                entity.HasOne(d => d.FkUserNavigation)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.FkUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermission_User");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DuongDanFile)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FkNgonNgu).HasColumnName("FK_NgonNgu");

                entity.Property(e => e.GioiThieu).HasMaxLength(500);

                entity.Property(e => e.HinhAnh).HasMaxLength(200);

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiTao).HasMaxLength(100);

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TieuDe)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UserNguoiSua).HasMaxLength(100);

                entity.HasOne(d => d.FkNgonNguNavigation)
                    .WithMany(p => p.Video)
                    .HasForeignKey(d => d.FkNgonNgu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Video_NgonNgu");
            });
        }
    }
}
