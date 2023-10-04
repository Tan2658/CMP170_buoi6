CREATE DATABASE QLNhanSu
GO

USE QLNhanSu
GO

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE NAME='Phongban')
	DROP TABLE Phongban
GO
CREATE TABLE Phongban
(
	MaPB char(2) NOT NULL,
	TenPB nvarchar(30) NOT NULL,
	PRIMARY KEY (MaPB)
)

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE NAME='Nhanvien')
	DROP TABLE Nhanvien
GO
CREATE TABLE Nhanvien
(
	MaNV char(6) NOT NULL,
	TenNV nvarchar(20) NOT NULL,
	Ngaysinh Datetime,
	MaPB char(2) NOT NULL,
	PRIMARY KEY (MaNV),
	CONSTRAINT CHK_MaPB FOREIGN KEY (MaPB) REFERENCES Phongban(MaPB)
)

INSERT INTO Phongban VALUES ('IT', 'Cong nghe thong tin')
INSERT INTO Phongban VALUES ('TA', 'Ngon ngu Anh')
SELECT * FROM Phongban

SET DATEFORMAT DMY
INSERT INTO Nhanvien VALUES ('NV0000', 'Nguyen Van A', '1/1/1980', 'IT')
INSERT INTO Nhanvien VALUES ('NV0001', 'Nguyen Thi B', '2/2/1985', 'TA')
INSERT INTO Nhanvien VALUES ('NV0002', 'Nguyen Van C', '3/4/1990', 'IT')
SELECT * FROM Nhanvien