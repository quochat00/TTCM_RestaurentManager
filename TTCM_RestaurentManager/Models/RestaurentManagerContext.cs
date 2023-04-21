using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TTCM_RestaurentManager.Models;

public partial class RestaurentManagerContext : DbContext
{
    public RestaurentManagerContext()
    {
    }

    public RestaurentManagerContext(DbContextOptions<RestaurentManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ban> Bans { get; set; }

    public virtual DbSet<DatBan> DatBans { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiMonAn> LoaiMonAns { get; set; }

    public virtual DbSet<MonAn> MonAns { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-MIODTN1D\\SQLEXPRESS;Initial Catalog=RestaurentManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ban>(entity =>
        {
            entity.HasKey(e => e.MaBan).HasName("PK__Ban__3520ED6CA6C87C63");

            entity.ToTable("Ban");

            entity.Property(e => e.MaBan).HasMaxLength(10);
        });

        modelBuilder.Entity<DatBan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DatBan__3214EC279E8D0FA8");

            entity.ToTable("DatBan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.NgayDatBan).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6E06621B2A3");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd)
                .HasMaxLength(10)
                .HasColumnName("MaHD");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaMa)
                .HasMaxLength(10)
                .HasColumnName("MaMA");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.TongTien).HasColumnType("money");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("fk_HoaDon_KhachHang");

            entity.HasOne(d => d.MaMaNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaMa)
                .HasConstraintName("fk_HoaDon_MonAn");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E492CDB77");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.AnhKh)
                .HasMaxLength(1024)
                .HasColumnName("AnhKH");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.EmailKh)
                .HasMaxLength(30)
                .HasColumnName("EmailKH");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.Sdtkh)
                .HasMaxLength(20)
                .HasColumnName("SDTKH");
            entity.Property(e => e.TenKh)
                .HasMaxLength(100)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<LoaiMonAn>(entity =>
        {
            entity.HasKey(e => e.MaLoaiMa).HasName("PK__LoaiMonA__12253B45CC774324");

            entity.ToTable("LoaiMonAn");

            entity.Property(e => e.MaLoaiMa)
                .HasMaxLength(10)
                .HasColumnName("MaLoaiMA");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.TenLoaiMa)
                .HasMaxLength(100)
                .HasColumnName("TenLoaiMA");
        });

        modelBuilder.Entity<MonAn>(entity =>
        {
            entity.HasKey(e => e.MaMa).HasName("PK__MonAn__2725DFC056547045");

            entity.ToTable("MonAn");

            entity.Property(e => e.MaMa)
                .HasMaxLength(10)
                .HasColumnName("MaMA");
            entity.Property(e => e.Active).HasDefaultValueSql("((0))");
            entity.Property(e => e.AnhMa)
                .HasMaxLength(1024)
                .HasColumnName("AnhMA");
            entity.Property(e => e.ChiTietMa)
                .HasMaxLength(500)
                .HasColumnName("ChiTietMA");
            entity.Property(e => e.Gia).HasColumnType("money");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaLoaiMa)
                .HasMaxLength(10)
                .HasColumnName("MaLoaiMA");
            entity.Property(e => e.SoLuongMa).HasColumnName("SoLuongMA");
            entity.Property(e => e.TenMa)
                .HasMaxLength(100)
                .HasColumnName("TenMA");

            entity.HasOne(d => d.MaLoaiMaNavigation).WithMany(p => p.MonAns)
                .HasForeignKey(d => d.MaLoaiMa)
                .HasConstraintName("fk_MonAn_LoaiMonAn");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A765FCC63");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.AnhNv)
                .HasMaxLength(1024)
                .HasColumnName("AnhNV");
            entity.Property(e => e.ChucVu).HasMaxLength(30);
            entity.Property(e => e.EmailNv)
                .HasMaxLength(30)
                .HasColumnName("EmailNV");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.Sdtnv)
                .HasMaxLength(20)
                .HasColumnName("SDTNV");
            entity.Property(e => e.TenNv)
                .HasMaxLength(100)
                .HasColumnName("TenNV");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaiKhoan__3213E83F06746306");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.Loai)
                .HasMaxLength(20)
                .HasColumnName("loai");
            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.Pass)
                .HasMaxLength(30)
                .HasColumnName("pass");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("username");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("fk_TaiKhoan_KhachHang");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("fk_TaiKhoan_NhanVien");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
