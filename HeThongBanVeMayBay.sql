CREATE DATABASE QLBANVEMAYBAY
GO
USE QLBANVEMAYBAY
GO
CREATE TABLE CHUCVU
(
	ID			INT IDENTITY (1,1)	NOT NULL,
	IDChucVu	NCHAR(10) NOT NULL,
	TenChucVu	NVARCHAR(MAX)		NOT NULL,
	IsAdmin		VARCHAR(5) NOT NULL,
	PRIMARY KEY CLUSTERED			(IDChucVu ASC)
)
CREATE TABLE SANBAY
(
	ID			INT IDENTITY (1,1) NOT NULL,
	IATA		VARCHAR(10) NOT NULL,
	TenSB		NVARCHAR(50) NOT NULL,
	Status		NVARCHAR(MAX),
	PRIMARY KEY CLUSTERED (IATA ASC)
)
CREATE TABLE HANGBAY
(
	ID				INT IDENTITY (1,1)	NOT NULL,
	IDHangBay		NVARCHAR(10)		NOT NULL,
	TenHangbay		VARCHAR(20)			NOT NULL,
	ImageAviation	NVARCHAR (MAX)		NULL,
	Status			NVARCHAR(MAX),
	PRIMARY KEY		CLUSTERED (IDHangBay ASC)
)
CREATE TABLE MAYBAY
(
	ID						INT IDENTITY (1,1)	NOT NULL,
	IDMayBay				VARCHAR(10)			NOT NULL,
	SoDangBa				VARCHAR(10)			NOT NULL,
	LoaiMayBay				VARCHAR(MAX)		NOT NULL,	
	HangBay					NVARCHAR(10)		NOT NULL,
	SucChuaToiDa			INT					NOT NULL,
	Status					NVARCHAR(MAX),
	PRIMARY KEY				CLUSTERED			(IDMayBay ASC),
	FOREIGN KEY (HangBay)	REFERENCES			HANGBAY(IDHangBay)
)
CREATE TABLE CHUYENBAY
(
	ID								INT IDENTITY (1,1) NOT NULL,
	IDChuyenBay						VARCHAR(10) NOT NULL,
	HangBay							NVARCHAR(10) NOT NULL,
	IDSanBayDi						VARCHAR(10) NOT NULL,
	IDSanBayDen						VARCHAR(10) NOT NULL,
	GiaTien							INT NOT NULL,
	NgayBay							DATETIME NOT NULL,
	GioBay							Time NOT NULL,
	ThoiGianToiDuKien				TIME NOT NULL,
	SoGheHang1						INT NOT NULL,
	SoGheHang2						INT NOT NULL,
	Status							NVARCHAR(MAX),
	PRIMARY KEY						CLUSTERED (ID ASC),
	FOREIGN KEY (IDSanBayDi)		REFERENCES SANBAY(IATA),
	FOREIGN KEY (IDSanBayDen)		REFERENCES SANBAY(IATA),
	FOREIGN KEY (IDChuyenBay)		REFERENCES MAYBAY(IDMayBay),
	FOREIGN KEY (HangBay)			REFERENCES HANGBAY(IDHangBay)
)
--CREATE TABLE CHI_TIET_LICH_CHUYEN_BAY
--(
--	ID								INT IDENTITY (1,1) NOT NULL,
--	IDChiTietChuyenBay				VARCHAR(10) NOT NULL,
--	IDChuyenBay						VARCHAR(10) NOT NULL,
--	IDSanBayTrungGian				VARCHAR(10) NULL,
--	ThoiGianDung					TIME NULL,
--	GhiChu							NTEXT,
--	PRIMARY KEY						CLUSTERED (IDChiTietChuyenBay ASC),
--	FOREIGN KEY (IDSanBayTrungGian) REFERENCES SANBAY(IATA),
--	FOREIGN KEY (IDChuyenBay)		REFERENCES CHUYENBAY(IDChuyenBay)
--)
CREATE TABLE HANHKHACH
(
	ID						INT IDENTITY (1,1)	NOT NULL,
	UserName				VARCHAR(MAX)		NULL,
	Pass					VARCHAR(MAX)		NULL,
	Email					VARCHAR(MAX)		NOT NULL,
	CMND					VARCHAR(12)			NOT NULL,
	TenHanhKhach			NVARCHAR(50)		NOT NULL,
	NgaySinh				DATE				NOT NULL,
	GioiTinh				NVARCHAR(10)		NOT NULL,
	DienThoai				VARCHAR(10)			NOT NULL,
	ChucVu					NCHAR(10)			NOT NULL,
	Status					NVARCHAR(MAX),
	PRIMARY KEY				CLUSTERED			(CMND ASC),
	FOREIGN KEY (ChucVu)	REFERENCES			CHUCVU(IDChucVu)
)
CREATE TABLE VECHUYENBAY
(
	ID							INT IDENTITY (1,1)		NOT NULL,
	IDVeChuyenBay				VARCHAR(20)				NOT NULL,
	IDChuyenBay					INT						NOT NULL,
	CMND						VARCHAR(12)				NOT NULL,
	GiaTien						INT						NOT NULL,
	LoaiVe						NVARCHAR(10)			NOT NULL,
	Status						NVARCHAR(MAX),
	PRIMARY KEY					CLUSTERED				(ID),
	FOREIGN KEY (IDChuyenBay)	REFERENCES				CHUYENBAY(ID),
	FOREIGN KEY (CMND)			REFERENCES				HANHKHACH(CMND)
)
CREATE TABLE PHIEUDATCHO
(
	ID								INT IDENTITY (1,1)		NOT NULL,
	IDDatCho						VARCHAR(10)				NOT NULL,
	IDChuyenBay						INT						NOT NULL,
	CMND							VARCHAR(12)				NOT NULL,
	GiaTien							INT						NOT NULL,
	LoaiVe							NVARCHAR(10)			NOT NULL,
	Status							NVARCHAR(MAX),
	PRIMARY KEY						CLUSTERED				(ID ASC),
	FOREIGN KEY (IDChuyenBay)		REFERENCES				CHUYENBAY(ID),
	FOREIGN KEY (CMND)				REFERENCES				HANHKHACH(CMND)
)
CREATE TABLE PHONGBAN
(
	ID			INT IDENTITY (1,1)	NOT NULL,
	IDPhongBan	NCHAR(10)			NOT NULL,
	TenPhongBan	NVARCHAR(MAX)		NOT NULL,
	Status		NVARCHAR(MAX),
	PRIMARY KEY CLUSTERED			(IDPhongBan ASC)
)
CREATE TABLE NHANVIEN
(
	ID						INT IDENTITY (1,1)	NOT NULL,
	UserName				VARCHAR(50)			NOT NULL,
	Pass					VARCHAR(MAX)		NOT NULL,
	Email					VARCHAR(MAX)		NOT NULL,
	TenNV					NVARCHAR(30)		NOT NULL,
	GioiTinh				NVARCHAR(10)		NOT NULL,
	NgaySinh				DATETIME			NOT NULL,
	NgayVaoLam				DATETIME			NOT NULL,
	NgayNghiLam				DATETIME,
	ChucVu					NCHAR(10)			NOT NULL,
	BoPhan					NCHAR(10)			NOT NULL,
	ImageEmp				NVARCHAR (MAX)		NULL,
	Status					NVARCHAR(MAX),
	PRIMARY KEY				CLUSTERED			(UserName ASC),
	FOREIGN KEY (BoPhan)	REFERENCES			PHONGBAN(IDPhongBan),
	FOREIGN KEY (ChucVu)	REFERENCES			CHUCVU(IDChucVu)
)