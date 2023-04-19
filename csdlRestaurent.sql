create database RestaurentManager
use [RestaurentManager]
create table LoaiMonAn(
	MaLoaiMA nvarchar(10) primary key not null,
	TenLoaiMA nvarchar(100) not null,
)
create table MonAn(
	MaMA nvarchar(10) primary key not null,
	TenMA nvarchar(100) not null,
	ChiTietMA nvarchar(500) not null,
	Gia money not null,
	AnhMA nvarchar(1024) not null,
	SoLuongMA int not null,
	Active int default 0,
	isDeleted int default 0,
	MaLoaiMA nvarchar(10),
	constraint fk_MonAn_LoaiMonAn foreign key (MaLoaiMA) references LoaiMonAn(MaLoaiMA) 
)




create table KhachHang(
	MaKH nvarchar(10) primary key not null,
	AnhKH nvarchar(1024) not null,
	TenKH nvarchar(100) not null,
	SDTKH nvarchar(20) not null,
	EmailKH nvarchar(30) not null,
	DiaChi nvarchar(100) not null
)
create table NhanVien(
	MaNV nvarchar(10) primary key not null,
	TenNV nvarchar(100) not null,
	ChucVu nvarchar(30) not null,
	AnhNV nvarchar(1024) not null,
	SDTNV nvarchar(20) not null,
	EmailNV nvarchar(30) not null,
)
create table HoaDon(
	MaHD nvarchar(10) primary key not null,
	MaMA nvarchar(10),
	MaKH nvarchar(10),
	NgayTao date DEFAULT GETDATE(),
	SoLuong int,
	TongTien money ,
	isDeleted int default 0,
	constraint fk_HoaDon_MonAn foreign key (MaMA) references MonAn(MaMA), 
	constraint fk_HoaDon_KhachHang foreign key (MaKH) references KhachHang(MaKH) 
)



create table TaiKhoan(
	id nvarchar(10) primary key not null,
	username nvarchar(30) not null,
	pass nvarchar(30) not null,
	email nvarchar(30) not null,
	loai nvarchar(20),
	MaNV nvarchar(10) ,
	MaKH nvarchar(10) ,
	isDeleted int default 0,
	constraint fk_TaiKhoan_NhanVien foreign key (MaNV) references NhanVien(MaNV), 
	constraint fk_TaiKhoan_KhachHang foreign key (MaKH) references KhachHang(MaKH), 
)