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

insert into NhanVien(MaNV,TenNV,ChucVu,AnhNV,SDTNV,EmailNV) values
('NV01',N'Trần Bảo Quốc',N'Quản lý','quoc','0829307866',N'quoc@gmail.com'),
('NV02',N'Trần Tuấn Huy ',N'Nhân viên phục vụ','huy','0829307866',N'huy@gmail.com'),
('NV03',N'Trần Văn Hát',N'Bếp trưởng','hat','0829307866',N'hat@gmail.com'),
('NV04',N'Nguyễn Thị Loan',N'Bếp phó','loan','0829307866',N'loan@gmail.com'),
('NV05',N'Trần Mai An',N'Nhân viên thu ngân','an','0829307866',N'an@gmail.com')


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
create table Ban(
	MaBan nvarchar(10) primary key not null,
	LoaiBan int not null 
)
insert into Ban values
('B01',2),
('B02',3),
('B03',4),
('B04',5),
('B05',6)


CREATE TABLE DatBan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(50) NOT NULL,
    SoDienThoai NVARCHAR(20) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    NgayDatBan DATETIME NOT NULL,
    SoLuongNguoi INT NOT NULL,
    GhiChu NVARCHAR(100)
);

insert into KhachHang(MaKH,TenKH,SDTKH,EmailKH,DiaChi,AnhKH) values 
(N'KH01',N'Trần Mai Anh','0971833578','anh@gmail.com',N'Hà Nội','NV01'),
(N'KH02',N'Nguyễn Quốc Hưng','0829328732','hung@gmail.com',N'Hà Nội','NV02'),
(N'KH03',N'Ông Cao Thắng','0985789466','thang@gmail.com',N'Nam Định','NV03'),
(N'KH04',N'Nguyễn Quỳnh Ngọc','0356478450','ngoc@gmail.com',N'Thái Bình','NV04'),
(N'KH05',N'Trần Văn Vinh','0362025641','vinh@gmail.com',N'Bắc Giang','NV05'),
(N'KH06',N'Trần Văn Hát','0985678888','hat@gmail.com',N'Hà Nội','NV06'),
(N'KH07',N'Nguyễn Thị Loan','0914645555','loan@gmail.com',N'Sài Gòn','NV07')

insert into TaiKhoan(id,username,pass,email,loai) values 
('TK01','nbtuan','123456',N'tuan@gmail.com','admin'),
('TK02','ndthuan','123456',N'thuan@gmail.com','agency'),
('TK03','tbquoc','123456',N'quoc@gmail.com','agency'),
('TK04','ntphuong','123456',N'phuong@gmail.com','customer')
UPDATE TaiKhoan
SET MaNV = 'NV01'
WHERE id = 'TK03' AND MaNV IS NULL;