-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th6 25, 2020 lúc 05:08 AM
-- Phiên bản máy phục vụ: 10.4.11-MariaDB
-- Phiên bản PHP: 7.4.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `dienluc`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `bactiendien`
--

CREATE TABLE `bactiendien` (
  `id` int(11) NOT NULL,
  `bac` int(11) DEFAULT NULL,
  `gia` float DEFAULT NULL,
  `min` float DEFAULT NULL,
  `max` float DEFAULT NULL,
  `ngaySua` date DEFAULT NULL,
  `ngayTao` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `bactiendien`
--

INSERT INTO `bactiendien` (`id`, `bac`, `gia`, `min`, `max`, `ngaySua`, `ngayTao`) VALUES
(1, 1, 1510, 0, 50, NULL, '2020-06-11'),
(2, 2, 1561, 50, 100, '2020-06-14', '2020-06-11'),
(3, 3, 1813, 100, 200, '2020-06-14', '2020-06-11'),
(4, 4, 2282, 200, 300, '2020-06-14', '2020-06-11'),
(7, 5, 2834, 300, 400, NULL, '2020-06-14'),
(8, 6, 2927, 400, 1000000000, '2020-06-24', '2020-06-14');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `chisodien`
--

CREATE TABLE `chisodien` (
  `id` int(11) NOT NULL,
  `idKH` int(11) DEFAULT NULL,
  `chiSoMoi` float DEFAULT NULL,
  `chiSoCu` float DEFAULT NULL,
  `tinhTrang` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `chisodien`
--

INSERT INTO `chisodien` (`id`, `idKH`, `chiSoMoi`, `chiSoCu`, `tinhTrang`) VALUES
(11, 29, 0, 0, NULL),
(12, 30, 300, 200, NULL),
(13, 31, 250, 123, NULL),
(14, 32, 220, 120, NULL),
(15, 33, 0, 0, NULL),
(18, 36, 0, 0, NULL),
(19, 37, 0, 0, NULL),
(20, 38, 0, 0, NULL),
(21, 39, 0, 0, NULL),
(22, 40, 0, 0, NULL),
(23, 41, 0, 0, NULL),
(24, 42, 300, 0, NULL),
(25, 43, 0, 2000, NULL),
(26, 44, 0, 0, NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `datenhapchiso`
--

CREATE TABLE `datenhapchiso` (
  `id` int(11) NOT NULL,
  `idKH` int(11) DEFAULT NULL,
  `chiSoMoi` varchar(10) DEFAULT NULL,
  `chiSoCu` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `datenhapchiso`
--

INSERT INTO `datenhapchiso` (`id`, `idKH`, `chiSoMoi`, `chiSoCu`) VALUES
(14, 29, '0000-00-00', '2020-06-24'),
(15, 30, '2020-06-24', '2020-06-24'),
(16, 31, '2020-06-24', '2020-06-24'),
(17, 32, '2020-06-25', '2020-06-24'),
(18, 33, '0000-00-00', '2020-06-24'),
(21, 36, '0000-00-00', '2020-06-24'),
(22, 37, '0000-00-00', '2020-06-24'),
(23, 38, '0000-00-00', '2020-06-24'),
(24, 39, '0000-00-00', '2020-06-24'),
(25, 40, '0000-00-00', '2020-06-24'),
(26, 41, '0000-00-00', '2020-06-24'),
(27, 42, '2020-06-24', '2020-06-24'),
(28, 43, '0000-00-00', '2020-06-24'),
(29, 44, '0000-00-00', '2020-06-25');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `khachhang`
--

CREATE TABLE `khachhang` (
  `idKH` int(11) NOT NULL,
  `hoTen` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `gioiTinh` varchar(10) DEFAULT NULL,
  `ngaySinh` date DEFAULT NULL,
  `soCMT` varchar(50) DEFAULT NULL,
  `soDienThoai` varchar(12) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `diaChi` text CHARACTER SET utf8 DEFAULT NULL,
  `tenPhuong` varchar(50) CHARACTER SET utf8 DEFAULT NULL,
  `soCongTo` varchar(30) NOT NULL,
  `donGia` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `khachhang`
--

INSERT INTO `khachhang` (`idKH`, `hoTen`, `gioiTinh`, `ngaySinh`, `soCMT`, `soDienThoai`, `email`, `diaChi`, `tenPhuong`, `soCongTo`, `donGia`) VALUES
(29, 'Trần Văn A', 'Nam', '1969-01-01', '19690101', '1234567810', 'tranvana@gmail.com', '69 Cầu Diễn', 'Cầu Diễn', '1111', 0),
(30, 'Trần Thị B', 'Nữ', '1969-02-02', '19690202', '1234567811', 'tranthib@gmail.com', '69 Đại Mỗ', 'Đại Mỗ', '1112', 2000),
(31, 'Trần Thị C', 'Nữ', '1969-03-03', '19690303', '1234567812', 'tranthic@gmail.com', '69 Mễ Trì', 'Mễ Trì', '1113', 0),
(32, 'Trần Văn D', 'Nam', '1969-04-04', '19690404', '1234567813', 'tranthid@gmail.com', '69 Mỹ Đình 1', 'Mỹ Đình 1', '1114', 0),
(33, 'Hoàng Văn E', 'Nam', '1969-05-05', '19690505', '1234567814', 'hoangvane@gmail.com', '69 Mỹ Đình 2', 'Mỹ Đình 2', '1115', 0),
(36, 'Vũ Thị H', 'Nữ', '1969-08-08', '19690808', '1234567817', 'vuthih@gmail.com', '69 Trung Văn', 'Trung Văn', '1117', 0),
(37, 'Lê Văn I', 'Nam', '1969-09-09', '19690909', '1234567818', 'lethii@gmail.com', '69 Tây Mỗ', 'Tây Mỗ', '1118', 0),
(38, 'Tô Văn J', 'Nam', '1969-10-10', '19691010', '1234567819', 'tovanj@gmail.com', '69 Xuân Phương', 'Xuân Phương', '1119', 2500),
(39, 'Trần Duy Bá', 'Nam', '1999-05-02', '19691111', '1234567820', 'tranduyba2599@gmail.com', '96 Cầu Diễn', 'Cầu Diễn', '1120', 0),
(40, 'Nguyễn Thị Linh', 'Nữ', '1999-05-05', '19691212', '1234567821', 'nguyenlinh@gmail.com', '96 Cầu Diễn', 'Cầu Diễn', '1121', 0),
(41, 'Phân Văn K', 'Nam', '1970-01-01', '19700101', '1234567823', 'phanvank@gmail.com', '70 Đại Mỗ', 'Đại Mỗ', '1124', 0),
(42, 'Hoàng Tuấn Anh', 'Nam', '1999-01-01', '19990101', '1234567825', 'hoangtuananh@gmail.com', '70 Mỹ Đình', 'Mỹ Đình 1', '1125', 0),
(43, 'Phạm Văn X', 'Nam', '1970-02-02', '19700202', '1234567829', 'phamvanx@gmail.com', '99 Đại Mỗ', 'Đại Mỗ', '1126', 0),
(44, 'Đặng Hải N', 'Nam', '1996-01-01', '19960101', '1234567878', 'danghaing@mail.com', '96 Đại Mỗ', 'Đại Mỗ', '1234', 2000);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `lichsugiaodich`
--

CREATE TABLE `lichsugiaodich` (
  `id` int(11) NOT NULL,
  `idKH` int(11) DEFAULT NULL,
  `thangNam` date DEFAULT NULL,
  `chiSoMoi` float DEFAULT NULL,
  `chiSoCu` float DEFAULT NULL,
  `tienThanhToan` float DEFAULT NULL,
  `dateThanhToan` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Đang đổ dữ liệu cho bảng `lichsugiaodich`
--

INSERT INTO `lichsugiaodich` (`id`, `idKH`, `thangNam`, `chiSoMoi`, `chiSoCu`, `tienThanhToan`, `dateThanhToan`) VALUES
(14, 30, '2020-06-24', 200, 0, 440000, '2020-06-24'),
(15, 43, '2020-06-24', 2000, 0, 6082620, '2020-06-24');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `bactiendien`
--
ALTER TABLE `bactiendien`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `chisodien`
--
ALTER TABLE `chisodien`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idKH` (`idKH`);

--
-- Chỉ mục cho bảng `datenhapchiso`
--
ALTER TABLE `datenhapchiso`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idKH` (`idKH`);

--
-- Chỉ mục cho bảng `khachhang`
--
ALTER TABLE `khachhang`
  ADD PRIMARY KEY (`idKH`);

--
-- Chỉ mục cho bảng `lichsugiaodich`
--
ALTER TABLE `lichsugiaodich`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idKH` (`idKH`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `bactiendien`
--
ALTER TABLE `bactiendien`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT cho bảng `chisodien`
--
ALTER TABLE `chisodien`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT cho bảng `datenhapchiso`
--
ALTER TABLE `datenhapchiso`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT cho bảng `khachhang`
--
ALTER TABLE `khachhang`
  MODIFY `idKH` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- AUTO_INCREMENT cho bảng `lichsugiaodich`
--
ALTER TABLE `lichsugiaodich`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `chisodien`
--
ALTER TABLE `chisodien`
  ADD CONSTRAINT `chisodien_ibfk_1` FOREIGN KEY (`idKH`) REFERENCES `khachhang` (`idKH`);

--
-- Các ràng buộc cho bảng `datenhapchiso`
--
ALTER TABLE `datenhapchiso`
  ADD CONSTRAINT `datenhapchiso_ibfk_1` FOREIGN KEY (`idKH`) REFERENCES `khachhang` (`idKH`);

--
-- Các ràng buộc cho bảng `lichsugiaodich`
--
ALTER TABLE `lichsugiaodich`
  ADD CONSTRAINT `lichsugiaodich_ibfk_1` FOREIGN KEY (`idKH`) REFERENCES `khachhang` (`idKH`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
