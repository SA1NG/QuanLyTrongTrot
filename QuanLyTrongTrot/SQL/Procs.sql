USE QuanLyTrongTrot
GO
-- Xóa procedure nếu đã tồn tại
DROP PROC updateDonViHanhChinh
GO

-- Tạo procedure updateDonViHanhChinh
CREATE PROC updateDonViHanhChinh
(
    @action INT,
    @MaDonVi NVARCHAR(50) OUTPUT,
    @TenDonVi NVARCHAR(255) = NULL,
    @CapDoID INT = NULL,
    @CapTrenID NVARCHAR(50) = NULL
)
AS
BEGIN
    IF @action = -1 -- Xóa đơn vị hành chính
    BEGIN
        IF (SELECT TOP(1) MaDonVi FROM DonViHanhChinh WHERE CapTrenID = @MaDonVi) IS NULL
            DELETE FROM DonViHanhChinh WHERE MaDonVi = @MaDonVi
        RETURN
    END

    IF @action = 0 -- Cập nhật thông tin đơn vị hành chính
    BEGIN
        UPDATE DonViHanhChinh
        SET TenDonVi = @TenDonVi,
            CapDoID = @CapDoID,
            CapTrenID = @CapTrenID
        WHERE MaDonVi = @MaDonVi
        RETURN
    END
    -- Thêm mới đơn vị hành chính
    INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID)
    VALUES (@MaDonVi, @TenDonVi, @CapDoID, @CapTrenID)
    SET @MaDonVi = @@IDENTITY
END
GO

-- Xóa procedure nếu đã tồn tại
DROP PROC updateNguoiDung
GO

-- Tạo procedure updateNguoiDung
CREATE PROC updateNguoiDung
(
    @action INT,
    @ID INT OUTPUT,
    @TenNguoiDung NVARCHAR(255),
    @Email NVARCHAR(255) = NULL,
    @MatKhau NVARCHAR(255) = NULL,
    @VaiTroID INT = NULL,
    @TrangThai TINYINT = NULL,
    @DonViHanhChinhID NVARCHAR(50) = NULL
)
AS
BEGIN
    IF @action = -1 -- Xóa người dùng
    BEGIN
        DELETE FROM NguoiDung WHERE ID = @ID
        RETURN
    END

    IF @action = 0 -- Cập nhật thông tin người dùng
    BEGIN
        UPDATE NguoiDung
        SET TenNguoiDung = @TenNguoiDung,
            Email = @Email,
            MatKhau = @MatKhau,
            VaiTroID = @VaiTroID,
            TrangThai = @TrangThai,
            DonViHanhChinhID = @DonViHanhChinhID
        WHERE ID = @ID
        RETURN
    END

    -- Thêm mới người dùng
    INSERT INTO NguoiDung (TenNguoiDung, Email, MatKhau, VaiTroID, TrangThai, DonViHanhChinhID)
    VALUES (@TenNguoiDung, @Email, @MatKhau, @VaiTroID, @TrangThai, @DonViHanhChinhID)
    SET @ID = @@IDENTITY
END
GO

DROP PROC updateGiongCayTrong
GO

CREATE PROC updateGiongCayTrong
(
    @action INT,
    @MaCayTrong INT OUTPUT,
    @TenGiongCay NVARCHAR(255),
    @LoaiCayTrongID INT = NULL,
    @MoTa TEXT = NULL,
    @VungTrongID INT = NULL
)
AS
BEGIN
    IF @action = -1 -- Xóa giống cây trồng
    BEGIN
        DELETE FROM GiongCayTrong WHERE MaCayTrong = @MaCayTrong
        RETURN
    END

    IF @action = 0 -- Cập nhật thông tin giống cây trồng
    BEGIN
        UPDATE GiongCayTrong
        SET TenGiongCay = @TenGiongCay,
            LoaiCayTrongID = @LoaiCayTrongID,
            MoTa = @MoTa,
            VungTrongID = @VungTrongID
        WHERE MaCayTrong = @MaCayTrong
        RETURN
    END

    -- Thêm mới giống cây trồng
    INSERT INTO GiongCayTrong (TenGiongCay, LoaiCayTrongID, MoTa, VungTrongID)
    VALUES (@TenGiongCay, @LoaiCayTrongID, @MoTa, @VungTrongID)
    SET @MaCayTrong = @@IDENTITY
END
GO

DROP PROC updateThuocBVTV
GO

CREATE PROC updateThuocBVTV
(
    @action INT,
    @ID INT OUTPUT,
    @TenThuoc NVARCHAR(255),
    @LoaiThuoc NVARCHAR(255) = NULL,
    @MoTa NVARCHAR(500) = NULL,
    @NgaySanXuat DATE = NULL,
    @NgayHetHan DATE = NULL,
    @CoSoID INT = NULL
)
AS
BEGIN
    IF @action = -1 -- Xóa thuốc BVTV
    BEGIN
        DELETE FROM ThuocBVTV WHERE ID = @ID
        RETURN
    END

    IF @action = 0 -- Cập nhật thông tin thuốc BVTV
    BEGIN
        UPDATE ThuocBVTV
        SET TenThuoc = @TenThuoc,
            LoaiThuoc = @LoaiThuoc,
            MoTa = @MoTa,
            NgaySanXuat = @NgaySanXuat,
            NgayHetHan = @NgayHetHan,
            CoSoID = @CoSoID
        WHERE ID = @ID
        RETURN
    END

    -- Thêm mới thuốc BVTV
    INSERT INTO ThuocBVTV (TenThuoc, LoaiThuoc, MoTa, NgaySanXuat, NgayHetHan, CoSoID)
    VALUES (@TenThuoc, @LoaiThuoc, @MoTa, @NgaySanXuat, @NgayHetHan, @CoSoID)
    SET @ID = @@IDENTITY
END
GO

DROP PROC updatePhanBon
GO

CREATE PROC updatePhanBon
(
    @action INT,
    @ID INT OUTPUT,
    @TenPhanBon NVARCHAR(255),
    @LoaiPhanBon NVARCHAR(255) = NULL,
    @MoTa NVARCHAR(500) = NULL,
    @NgaySanXuat DATE = NULL,
    @NgayHetHan DATE = NULL,
    @CoSoID INT = NULL
)
AS
BEGIN
    IF @action = -1 -- Xóa phân bón
    BEGIN
        DELETE FROM PhanBon WHERE ID = @ID
        RETURN
    END

    IF @action = 0 -- Cập nhật thông tin phân bón
    BEGIN
        UPDATE PhanBon
        SET TenPhanBon = @TenPhanBon,
            LoaiPhanBon = @LoaiPhanBon,
            MoTa = @MoTa,
            NgaySanXuat = @NgaySanXuat,
            NgayHetHan = @NgayHetHan,
            CoSoID = @CoSoID
        WHERE ID = @ID
        RETURN
    END

    -- Thêm mới phân bón
    INSERT INTO PhanBon (TenPhanBon, LoaiPhanBon, MoTa, NgaySanXuat, NgayHetHan, CoSoID)
    VALUES (@TenPhanBon, @LoaiPhanBon, @MoTa, @NgaySanXuat, @NgayHetHan, @CoSoID)
    SET @ID = @@IDENTITY
END
GO

DROP PROC updateSinhVatGayHaiVaTuoiSau
GO

CREATE PROC updateSinhVatGayHaiVaTuoiSau
(
    @action INT,                           
    @ID INT OUTPUT,                        
    @TenSinhVat NVARCHAR(255),             
    @LoaiSinhVat NVARCHAR(255) = NULL,     
    @TuoiSau NVARCHAR(255) = NULL,         
    @CapDoPhoBien NVARCHAR(255) = NULL,    
    @MoTa TEXT = NULL,                     
    @VungTrongID INT = NULL                
)
AS
BEGIN
    -- Xóa sinh vật gây hại
    IF @action = -1 
    BEGIN
        DELETE FROM SinhVatGayHaiVaTuoiSau WHERE ID = @ID
        RETURN
    END

    -- Cập nhật thông tin sinh vật gây hại
    IF @action = 0 
    BEGIN
        UPDATE SinhVatGayHaiVaTuoiSau
        SET TenSinhVat = @TenSinhVat,
            LoaiSinhVat = @LoaiSinhVat,
            TuoiSau = @TuoiSau,
            CapDoPhoBien = @CapDoPhoBien,
            MoTa = @MoTa,
            VungTrongID = @VungTrongID
        WHERE ID = @ID
        RETURN
    END

    -- Thêm mới sinh vật gây hại
    IF @action = 1 
    BEGIN
        INSERT INTO SinhVatGayHaiVaTuoiSau 
        (TenSinhVat, LoaiSinhVat, TuoiSau, CapDoPhoBien, MoTa, VungTrongID)
        VALUES 
        (@TenSinhVat, @LoaiSinhVat, @TuoiSau, @CapDoPhoBien, @MoTa, @VungTrongID);

        -- Lấy ID mới tạo và trả về qua tham số OUTPUT
        SET @ID = @@IDENTITY
    END
END
GO

