CREATE DATABASE HeThong
GO
USE HeThong
GO

--Bảng quản lý cấp hành chính (cấp tỉnh, huyện, xã...)
CREATE TABLE CapDoHanhChinh 
(
    ID INT NOT NULL PRIMARY KEY,
    TenCapDo VARCHAR(50) NOT NULL
)
GO

--Bảng quản lý đơn vị hành chính
CREATE TABLE DonViHanhChinh 
(
	MaDonVi VARCHAR(50) NOT NULL PRIMARY KEY,
	TenDonVi VARCHAR(255) NOT NULL,
    CapDoID INT NOT NULL, -- Liên kết cấp độ hành chính
    CapTrenID VARCHAR(50) DEFAULT NULL, -- ID của cấp trên (NULL nếu là cấp cao nhất)
    FOREIGN KEY (CapDoID) REFERENCES CapDoHanhChinh(ID),
    FOREIGN KEY (CapTrenID) REFERENCES DonViHanhChinh(MaDonVi)
)
GO

-- Bảng vai trò
CREATE TABLE VaiTro 
(
    ID INT NOT NULL PRIMARY KEY,
    TenVaiTro VARCHAR(100) NOT NULL UNIQUE, -- Tên vai trò 
)
GO

-- Bảng người dùng
CREATE TABLE NguoiDung (
	ID INT NOT NULL PRIMARY KEY,		-- Mã người dùng
	TenNguoiDung VARCHAR(255) NOT NULL, -- Tên đầy đủ của người dùng
    Email VARCHAR(255) UNIQUE NOT NULL, -- Email người dùng
    MatKhau VARCHAR(255) NOT NULL, -- Mật khẩu đã mã hóa
    VaiTroID INT NOT NULL, -- Liên kết đến bảng vai trò
    TrangThai TINYINT NOT NULL, -- Trạng thái của người dùng
	DonViHanhChinhID VARCHAR(50), --Liên kết đến bảng đơn vị hành chính
    FOREIGN KEY (VaiTroID) REFERENCES VaiTro(ID),
	FOREIGN KEY (DonViHanhChinhID) REFERENCES DonViHanhChinh(MaDonVi)
)
GO

-- Bảng lịch sử truy cập
CREATE TABLE LichSuTruyCap (
    ID INT NOT NULL	PRIMARY KEY ,				 -- Mã lịch sử truy cập
    UserID INT,                                  -- Khóa ngoại tham chiếu tới người dùng
    ThoiGianTruyCap DATETIME DEFAULT GETDATE(),  -- Thời gian truy cập
    MoTaHanhDong VARCHAR(255),                   -- Mô tả hành động của người dùng 
    FOREIGN KEY (UserID) REFERENCES NguoiDung(ID)   -- Liên kết với bảng NguoiDung
)
GO

-- Bảng loại cây trồng
CREATE TABLE LoaiCayTrong (
    ID INT NOT NULL PRIMARY KEY,
    TenLoaiCayTrong VARCHAR(255) NOT NULL
)
GO
-- Bảng cây trồng
CREATE TABLE GiongCayTrong (
	MaCayTrong INT NOT NULL PRIMARY KEY,
    TenGiongCay VARCHAR(255) NOT NULL,
    LoaiCayTrongID INT,
    TinhTrangLuuHanh VARCHAR(255),  -- Ví dụ: "Lưu hành", "Chưa lưu hành"
    MoTa TEXT,
    VuonCayDauDong TINYINT,  -- Cây/vườn đầu dòng 
	VungTrongID INT,         -- Khóa ngoại liên kết với bảng VungTrong
    FOREIGN KEY (LoaiCayTrongID) REFERENCES LoaiCayTrong(ID),
	FOREIGN KEY (VungTrongID) REFERENCES VungTrong(ID)      -- Liên kết với bảng VungTrong
)
GO
-- Bảng danh mục sinh vật gây hại và tuổi sâu
CREATE TABLE SinhVatGayHaiVaTuoiSau (
    ID INT NOT NULL PRIMARY KEY ,						-- Mã sinh vật gây hại và tuổi sâu
    TenSinhVat VARCHAR(255) NOT NULL,                   -- Tên sinh vật gây hại
    LoaiSinhVat VARCHAR(255),                           -- Loại sinh vật (sâu, nấm, côn trùng...)
    TuoiSau VARCHAR(255),                               -- Tuổi sâu (ví dụ: "1 tuổi", "2 tuổi", ...)
    CapDoPhoBien VARCHAR(255),                          -- Cấp độ phổ biến của sinh vật (Cao, Trung bình, Thấp)
    MoTa TEXT,											-- Mô tả sinh vật gây hại
	VungTrongID INT,									-- Khóa ngoại tham chiếu tới bảng VungTrong
	FOREIGN KEY (VungTrongID) REFERENCES VungTrong(ID)	-- Liên kết với bảng VungTrong
)
GO
-- Bảng danh mục vùng trồng
CREATE TABLE VungTrong (
    ID INT NOT NULL PRIMARY KEY,                -- Mã vùng trồng
    TenVungTrong VARCHAR(255) NOT NULL,         -- Tên vùng trồng
    MoTa TEXT,                                  -- Mô tả vùng trồng (có thể là thông tin vùng khí hậu, đặc điểm vùng trồng)
	MaVungTrongID VARCHAR(50),
	FOREIGN KEY (MaVungTrongID) REFERENCES DonViHanhChinh(MaDonVi) --Liên kết với bảng DonViHanhChinh
)
GO

-- Bảng loại cơ sở
CREATE TABLE LoaiCoSo (
    ID INT NOT NULL PRIMARY KEY,       -- Mã loại cơ sở
    TenLoaiCoSo VARCHAR(255) NOT NULL  -- Tên loại cơ sở (ví dụ: Buôn bán, Sản xuất, Ðủ điều kiện ATTP VietGap)
)
GO

-- Bảng cơ sở
CREATE TABLE CoSo (
    MaCoSo INT NOT NULL PRIMARY KEY,			-- Mã cơ sở
    TenCoSo VARCHAR(255) NOT NULL,              -- Tên cơ sở
    DiaChi VARCHAR(255),                        -- Ðịa chỉ cơ sở
    NgayCapGiayPhep DATE,                       -- Ngày cấp giấy phép
    LoaiCoSoID INT,                             -- Khóa ngoại tham chiếu tới bảng LoaiCoSo
    TinhTrang TINYINT,							-- Tình trạng cơ sở
	MaHanhChinh VARCHAR(50),                    -- Mã hành chính của cơ sở
    FOREIGN KEY (LoaiCoSoID) REFERENCES LoaiCoSo(ID),  -- Liên kết với bảng LoaiCoSo
    FOREIGN KEY (MaHanhChinh) REFERENCES DonViHanhChinh(MaDonVi)  -- Liên kết với bảng DonViHanhChinh
)
GO

-- Bảng tọa độ
CREATE TABLE BanDoPhanBo (
    ID INT NOT NULL PRIMARY KEY,			 -- Mã bản đồ phân bố
    CoSoID INT,                              -- Khóa ngoại tham chiếu đến cơ sở buôn bán, tổ chức/cá nhân sản xuất, khu vực có sinh vật gây hại
    KinhDo DECIMAL(9, 6),                    -- Kinh độ (vị trí trên bản đồ)
    ViDo DECIMAL(9, 6),                      -- Vĩ dộ (vị trí trên bản đồ)
	KhuSVID INT,
    FOREIGN KEY (CoSoID) REFERENCES CoSo(MaCoSo), -- Liên kết với cơ sở buôn bán
	FOREIGN KEY (KhuSVID) REFERENCES VungTrong(ID) -- Liên kết với vùng trồng
)
GO

-- Bảng danh mục thuốc BVTV
CREATE TABLE ThuocBVTV (
    ID INT NOT NULL  PRIMARY KEY,					 -- Mã thuốc BVTV
    TenThuoc VARCHAR(255) NOT NULL,                  -- Tên thuốc BVTV
    LoaiThuoc VARCHAR(255),                          -- Loại thuốc
    MoTa VARCHAR(500),                               -- Mô tả thuốc
    NgaySanXuat DATE,                                -- Ngày sản xuất
    NgayHetHan DATE,                                 -- Ngày hết hạn
	CoSoID INT,
	FOREIGN KEY (CoSoID) REFERENCES CoSo(MaCoSo)    -- Liên kết với cơ sở buôn bán/sản xuất
)
GO

-- Bảng danh mục phân bón
CREATE TABLE PhanBon (
    ID INT NOT NULL PRIMARY KEY,					 -- Mã phân bón
    TenPhanBon VARCHAR(255) NOT NULL,                -- Tên phân bón
    LoaiPhanBon VARCHAR(255),                        -- Loại phân bón 
    MoTa VARCHAR(500),                               -- Mô tả phân bón
    NgaySanXuat DATE,                                -- Ngày sản xuấ
    NgayHetHan DATE,                                 -- Ngày hết hạn
	CoSoID INT,
	FOREIGN KEY (CoSoID) REFERENCES CoSo(MaCoSo)	 -- Liên kết với cơ cở buôn bán/sản xuất
)
GO

-- Bảng số liệu thống kê
CREATE TABLE SoLieuThongKe (
    ID INT NOT NULL PRIMARY KEY,                 -- Mã số liệu thống kê
    CoSoID INT,                                  -- Khóa ngoại tham chiếu tới bảng Cơ Sở
    PhanBonID INT,                               -- Khóa ngoại tham chiếu tới bảng Phân Bón
    GiongCayID INT,                              -- Khóa ngoại tham chiếu tới bảng Giống Cây Trồng
    VungTrongID INT,                             -- Khóa ngoại tham chiếu tới bảng Vùng Trồng
    ThuocBVTVID INT,                             -- Khóa ngoại tham chiếu tới bảng thuốc BVTV
    SinhVatGayHaiVaTuoiSauID INT,                -- Khóa ngoại tham chiếu tới bảng Sinh vật gây hại và tuổi sâu
    SoLuong INT,                                 -- Số lượng (số cơ sở, số sản phẩm,...)
    ThoiGianThongKe DATE,                        -- Thời gian thống kê
    FOREIGN KEY (CoSoID) REFERENCES CoSo(MaCoSo),-- Liên kết với bảng Cơ sở
    FOREIGN KEY (PhanBonID) REFERENCES PhanBon(ID),-- Liên kết với bảng Phân Bón
    FOREIGN KEY (GiongCayID) REFERENCES GiongCayTrong(MaCayTrong),-- Liên kết với bảng giống cây trồng
    FOREIGN KEY (VungTrongID) REFERENCES VungTrong(ID),-- Liên kết với bảng vùng trồng
    FOREIGN KEY (ThuocBVTVID) REFERENCES ThuocBVTV(ID),-- Liên kết với bảng thuốc BVTV
    FOREIGN KEY (SinhVatGayHaiVaTuoiSauID) REFERENCES SinhVatGayHaiVaTuoiSau(ID)-- Liên kết với bảng sinh vật gây hại và tuổi sâu
)
GO

-- Cấp độ hành chính: Tỉnh, Huyện, Xã	
INSERT INTO CapDoHanhChinh (TenCapDo) 
VALUES (1,'Tỉnh'), (2,'Huyện/Thành phố/Thị xã'), (3,'Xã/Phường/Thị trấn')
GO

-- Tỉnh Bắc Ninh (MaDonVi = '27')
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID) 
VALUES ('27', 'Bắc Ninh', 1, NULL)
GO

-- Các huyện/thành phố thuộc tỉnh Bắc Ninh
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('256', 'Thành phố Bắc Ninh', 2, '27'),
	('258', 'Huyện Yên Phong', 2, '27'),
	('259', 'Huyện Quế Võ', 2, '27'),
	('260', 'Huyện Tiên Du', 2, '27'),
	('261', 'Thành phố Từ Sơn', 2, '27'),
	('262', 'Huyện Thuận Thành', 2, '27'),
    ('263', 'Huyện Gia Bình', 2, '27'),
    ('264', 'Huyện Lương Tài', 2, '27')
GO

-- Các phường thuộc thành phố Bắc Ninh
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('09163', 'Phường Vũ Ninh', 3, '256'),
    ('09166', 'Phường Đáp Cầu', 3, '256'),
    ('09169', 'Phường Thị Cầu', 3, '256'),
    ('09172', 'Phường Kinh Bắc', 3, '256'),
    ('09175', 'Phường Vệ An', 3, '256'),
    ('09178', 'Phường Tiền An', 3, '256'),
    ('09181', 'Phường Đại Phúc', 3, '256'),
    ('09184', 'Phường Ninh Xá', 3, '256'),
    ('09187', 'Phường Suối Hoa', 3, '256'),
    ('09190', 'Phường Võ Cường', 3, '256'),
    ('09214', 'Phường Hòa Long', 3, '256'),
    ('09226', 'Phường Vạn An', 3, '256'),
    ('09235', 'Phường Khúc Xuyên', 3, '256'),
    ('09244', 'Phường Phong Khê', 3, '256'),
    ('09256', 'Phường Kim Chân', 3, '256'),
    ('09271', 'Phường Vân Dương', 3, '256'),
    ('09286', 'Phường Nam Sơn', 3, '256'),
    ('09325', 'Phường Khắc Niệm', 3, '256'),
    ('09331', 'Phường Hạp Lĩnh', 3, '256')
GO

-- Các đơn vị hành chính thuộc huyện Yên Phong
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('09193', 'Thị trấn Chờ', 3, '258'),
    ('09196', 'Xã Dũng Liệt', 3, '258'),
    ('09199', 'Xã Tam Đa', 3, '258'),
    ('09202', 'Xã Tam Giang', 3, '258'),
    ('09205', 'Xã Yên Trung', 3, '258'),
    ('09208', 'Xã Thụy Hòa', 3, '258'),
    ('09211', 'Xã Hòa Tiến', 3, '258'),
    ('09217', 'Xã Đông Tiến', 3, '258'),
    ('09220', 'Xã Yên Phụ', 3, '258'),
    ('09223', 'Xã Trung Nghĩa', 3, '258'),
    ('09229', 'Xã Đông Phong', 3, '258'),
    ('09232', 'Xã Long Châu', 3, '258'),
    ('09238', 'Xã Văn Môn', 3, '258'),
    ('09241', 'Xã Đông Thọ', 3, '258')
GO

-- Các đơn vị hành chính thuộc huyện Quế Võ
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('09247', 'Thị trấn Phố Mới', 3, '259'),
    ('09250', 'Xã Việt Thống', 3, '259'),
    ('09253', 'Xã Đại Xuân', 3, '259'),
    ('09259', 'Xã Nhân Hòa', 3, '259'),
    ('09262', 'Xã Bằng An', 3, '259'),
    ('09265', 'Xã Phương Liễu', 3, '259'),
    ('09268', 'Xã Quế Tân', 3, '259'),
    ('09274', 'Xã Phù Lương', 3, '259'),
    ('09277', 'Xã Phù Lãng', 3, '259'),
    ('09280', 'Xã Phượng Mao', 3, '259'),
    ('09283', 'Xã Việt Hùng', 3, '259'),
    ('09289', 'Xã Ngọc Xá', 3, '259'),
    ('09292', 'Xã Châu Phong', 3, '259'),
    ('09295', 'Xã Bồng Lai', 3, '259'),
    ('09298', 'Xã Cách Bi', 3, '259'),
    ('09301', 'Xã Đào Viên', 3, '259'),
    ('09304', 'Xã Yên Giả', 3, '259'),
    ('09307', 'Xã Mộ Đạo', 3, '259'),
    ('09310', 'Xã Đức Long', 3, '259'),
    ('09313', 'Xã Chi Lăng', 3, '259'),
    ('09316', 'Xã Hán Quảng', 3, '259')
GO

-- Các đơn vị hành chính thuộc huyện Tiên Du
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('09319', 'Thị trấn Lim', 3, '260'),
    ('09322', 'Xã Phú Lâm', 3, '260'),
    ('09328', 'Xã Nội Duệ', 3, '260'),
    ('09334', 'Xã Liên Bão', 3, '260'),
    ('09337', 'Xã Hiên Vân', 3, '260'),
    ('09340', 'Xã Hoàn Sơn', 3, '260'),
    ('09343', 'Xã Lạc Vệ', 3, '260'),
    ('09346', 'Xã Việt Đoàn', 3, '260'),
    ('09349', 'Xã Phật Tích', 3, '260'),
    ('09352', 'Xã Tân Chi', 3, '260'),
    ('09355', 'Xã Đại Đồng', 3, '260'),
    ('09358', 'Xã Tri Phương', 3, '260'),
    ('09361', 'Xã Minh Đạo', 3, '260'),
    ('09364', 'Xã Cảnh Hưng', 3, '260')
GO

-- Các đơn vị hành chính thuộc thành phố Từ Sơn
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('09367', 'Phường Đông Ngàn', 3, '261'),
    ('09370', 'Xã Tam Sơn', 3, '261'),
    ('09373', 'Xã Hương Mạc', 3, '261'),
    ('09376', 'Xã Tương Giang', 3, '261'),
    ('09379', 'Xã Phù Khê', 3, '261'),
    ('09382', 'Phường Đồng Kỵ', 3, '261'),
    ('09383', 'Phường Trang Hạ', 3, '261'),
    ('09385', 'Phường Đồng Nguyên', 3, '261'),
    ('09388', 'Phường Châu Khê', 3, '261'),
    ('09391', 'Phường Tân Hồng', 3, '261'),
    ('09394', 'Phường Đình Bảng', 3, '261'),
    ('09397', 'Xã Phù Chẩn', 3, '261')
GO

-- Các đơn vị hành chính thuộc huyện Thuận Thành
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('09400', 'Thị trấn Hồ', 2, '262'),
    ('09403', 'Xã Hoài Thượng', 3, '262'),
    ('09406', 'Xã Đại Đồng Thành', 3, '262'),
    ('09409', 'Xã Mão Điền', 3, '262'),
    ('09412', 'Xã Song Hồ', 3, '262'),
    ('09415', 'Xã Đình Tổ', 3, '262'),
    ('09418', 'Xã An Bình', 3, '262'),
    ('09421', 'Xã Trí Quả', 3, '262'),
    ('09424', 'Xã Gia Đông', 3, '262'),
    ('09427', 'Xã Thanh Khương', 3, '262'),
    ('09430', 'Xã Trạm Lộ', 3, '262'),
    ('09433', 'Xã Xuân Lâm', 3, '262'),
    ('09436', 'Xã Hà Mãn', 3, '262'),
    ('09439', 'Xã Ngũ Thái', 3, '262'),
    ('09442', 'Xã Nguyệt Đức', 3, '262'),
    ('09445', 'Xã Ninh Xá', 3, '262'),
    ('09448', 'Xã Nghĩa Đạo', 3, '262'),
    ('09451', 'Xã Song Liễu', 3, '262')
GO

-- Các đơn vị hành chính thuộc huyện Gia Bình
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('09454', 'Thị trấn Gia Bình', 2, '263'),
    ('09457', 'Xã Vạn Ninh', 3, '263'),
    ('09460', 'Xã Thái Bảo', 3, '263'),
    ('09463', 'Xã Giang Sơn', 3, '263'),
    ('09466', 'Xã Cao Đức', 3, '263'),
    ('09469', 'Xã Đại Lai', 3, '263'),
    ('09472', 'Xã Song Giang', 3, '263'),
    ('09475', 'Xã Bình Dương', 3, '263'),
    ('09478', 'Xã Lãng Ngâm', 3, '263'),
    ('09481', 'Xã Nhân Thắng', 3, '263'),
    ('09484', 'Xã Xuân Lai', 3, '263'),
    ('09487', 'Xã Đông Cứu', 3, '263'),
    ('09490', 'Xã Đại Bái', 3, '263'),
    ('09493', 'Xã Quỳnh Phú', 3, '263');
GO

-- Các đơn vị hành chính thuộc huyện Lương Tài
INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
VALUES
    ('09496', 'Thị trấn Thứa', 2, '264'),
    ('09499', 'Xã An Thịnh', 3, '264'),
    ('09502', 'Xã Trung Kênh', 3, '264'),
    ('09505', 'Xã Phú Hòa', 3, '264'),
    ('09508', 'Xã Mỹ Hương', 3, '264'),
    ('09511', 'Xã Tân Lãng', 3, '264'),
    ('09514', 'Xã Quảng Phú', 3, '264'),
    ('09517', 'Xã Trừng Xá', 3, '264'),
    ('09520', 'Xã Lai Hạ', 3, '264'),
    ('09523', 'Xã Trung Chính', 3, '264'),
    ('09526', 'Xã Minh Tân', 3, '264'),
    ('09529', 'Xã Bình Định', 3, '264'),
    ('09532', 'Xã Phú Lương', 3, '264'),
    ('09535', 'Xã Lâm Thao', 3, '264');
GO

-- Chèn dữ liệu vào bảng VaiTro
INSERT INTO VaiTro (TenVaiTro)
VALUES 
    (1,'Admin'), -- Vai trò Admin
    (2,'User');  -- Vai trò User
GO
SELECT *FROM VaiTro
GO

-- Chèn dữ liệu vào bảng NguoiDung
INSERT INTO NguoiDung (ID, TenNguoiDung, Email, MatKhau, VaiTroID, TrangThai, DonViHanhChinhID)
VALUES
    (1, 'Trịnh Thị Mai Linh', 'mailinh1532004&gmail.com', 'mailinh', 1, 1, '09163'), -- Admin
    (2, 'Nguyễn Thanh Mai', 'mai2452004@gmail.com', 'mai', 1, 1, '09166'), -- Admin
    (3, 'Trần Lân Sang', 'sang0108@gmail.com', 'sang', 1, 1, '09235'), -- Admin
    (4, 'Đỗ Chí Thanh', 'chithanh&gmail.com', 'chi thanh', 1, 1, '09169'), -- Admin
    (5, 'Phạm Quốc E', 'phamquoce@gmail.com', 'quoc', 2, 1, '09172'), -- User
    (6, 'Hoàng Minh F', 'hoangminhf@gmail.com', 'minh', 2, 1, '09244'), -- User
    (7, 'Lê Hoài G', 'lehoaig@gmail.com', 'hoai', 2, 1, '09256'), -- User
    (8, 'Trần Thị H', 'tranthih@gmail.com', 'hai', 2, 1, '09271'), -- User
    (9, 'Nguyễn Hữu I', 'nguyenhuui@gmail.com', 'huu', 2, 1, '09394'), -- User
    (10, 'Lê Thuỳ J', 'lethuylj@gmail.com', 'thuy', 2, 1, '09379'); -- User
GO

-- Chèn dữ liệu vào bảng LoaiCayTrong
INSERT INTO LoaiCayTrong (ID, TenLoaiCayTrong)
VALUES 
    (1, 'Giống Cây Chính'),      -- Loại cây chính
    (2, 'Giống Lưu Hành'),       -- Loại giống lưu hành
    (3, 'Cây/Vườn Đầu Dòng');    -- Loại cây/vườn đầu dòng
GO
SELECT *FROM LoaiCayTrong

-- Chèn dữ liệu vào bảng VungTrong
INSERT INTO VungTrong (ID, TenVungTrong, MoTa, MaVungTrongID)
VALUES
    (1, 'Vùng trồng Thành phố Bắc Ninh', 'Khu vực chuyên canh rau màu và cây thực phẩm ngắn ngày', '256'),
    (2, 'Vùng trồng Huyện Tiên Du', 'Đất phù sa thích hợp cho cây ăn quả như vải, nhãn', '03'),
    (3, 'Vùng trồng Thị xã Từ Sơn', 'Vùng trồng rau công nghệ cao và các loại hoa màu', '04'),
    (4, 'Vùng trồng Huyện Yên Phong', 'Vùng trồng cây công nghiệp ngắn ngày', '05'),
    (5, 'Vùng trồng Huyện Thuận Thành', 'Khu vực chuyên canh lúa và các loại rau đặc sản', '06'),
    (6, 'Vùng trồng Huyện Quế Võ', 'Đất phù sa bồi đắp thích hợp cho ngô, đậu, và cây thực phẩm', '07'),
    (7, 'Vùng trồng Huyện Lương Tài', 'Khu vực trồng cây ăn quả lâu năm như cam, bưởi', '08'),
    (8, 'Vùng trồng Huyện Gia Bình', 'Phát triển trồng cây dược liệu và lúa chất lượng cao', '09');

-- Chèn dữ liệu giống lưu hành vào bảng GiongCayTrong
INSERT INTO GiongCayTrong (MaCayTrong, TenGiongCay, LoaiCayTrongID, TinhTrangLuuHanh, MoTa, VuonCayDauDong, VungTrongID)
VALUES 
    (1, 'Ăn non NV 016', 1, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Cải bẹ xanh
    (2, 'BÓNG THÁI XANH 01', 2, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Dưa leo
    (3, 'Cải Củ 45 ngày BM 07', 1, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Cải củ
    (4, 'Coarse NV 05', 3, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Bí đỏ
    (5, 'CRIMSON NV 01', 4, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Dưa hấu
    (6, 'CRIMSON NV 08', 4, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Dưa hấu
    (7, 'D25', 5, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Dong riềng
    (8, 'Dưa Gang Jakkapat', 6, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Dưa gang
    (9, 'Dưa Gang ML 6564', 6, 'Tự công bố lưu hành', 'Hàng năm', 1, 1),  -- Dưa gang
    (10, 'Dưa Gang NV 09', 6, 'Tự công bố lưu hành', 'Hàng năm', 1, 1);  -- Dưa gang
GO


