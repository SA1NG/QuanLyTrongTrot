using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QuanLyTrongTrot.Model
{
    public class CapDoHanhChinh
    {
   
        public int ID { get; set; } // Khóa chính
        public string TenCapDo { get; set; } // Tên cấp độ hành chính

    }
    public class DonViHanhChinh
    {
        public string MaDonVi { get; set; } // Khóa chính
        public string TenDonVi { get; set; } // Tên đơn vị hành chính
        public int CapDoID { get; set; } // Liên kết đến bảng CapDoHanhChinh
        public string CapTrenID { get; set; } // Liên kết đến đơn vị hành chính cấp trên
    }
    public class VaiTro
    {
        public int ID { get; set; }
        public string TenVaiTro { get; set; }

    }
    public class NguoiDung
    {
        public int ID { get; set; }
        public string TenNguoiDung { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public int VaiTroID { get; set; }
        public byte TrangThai { get; set; }
        public string DonViHanhChinhID { get; set; }
    }
    public class LichSuTruyCap
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime ThoiGianTruyCap { get; set; }
        public string MoTaHanhDong { get; set; }

    }
    public class LoaiCayTrong
    {
        public int ID { get; set; }
        public string TenLoaiCayTrong { get; set; }
    }
    public class CayTrong
    {
        public int ID { get; set; }
        public string TenGiongCay { get; set; }
        public int LoaiCayTrongID { get; set; }
        public string MoTa { get; set; }
        public int VungTrongID { get; set; }
    }
    public class SinhVatGayHaiVaTuoiSau
    {
        public int ID { get; set; }
        public string TenSinhVat { get; set; }
        public string LoaiSinhVat { get; set; }
        public string TuoiSau { get; set; }
        public string CapDoPhoBien { get; set; }
        public string MoTa { get; set; }
        public int VungTrongID { get; set; }
    }
    public class VungTrong
    {
        public int ID { get; set; }
        public string TenVungTrong { get; set; }
        public string MoTa { get; set; }
        public string MaVungTrongID { get; set; }
    }
    public class LoaiCoSo
    {
        public int ID { get; set; }
        public string TenLoaiCoSo { get; set; }
    }
    public class CoSo
    {
        public int MaCoSo { get; set; }
        public string TenCoSo { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayCapGiayPhep { get; set; }
        public int LoaiCoSoID { get; set; }
        public byte TinhTrang { get; set; }
        public string MaHanhChinh { get; set; }
    }
    public class ToaDo
    {
        public int ID { get; set; }
        public int CoSoID { get; set; }
        public decimal KinhDo { get; set; }
        public decimal ViDo { get; set; }
        public int KhuSVID { get; set; }
    }
    public class ThuocBVTV
    {
        public int ID { get; set; }
        public string TenThuoc { get; set; }
        public string LoaiThuoc { get; set; }
        public string MoTa { get; set; }
        public DateTime NgaySanXuat { get; set; }
        public DateTime NgayHetHan { get; set; }
        public int CoSoID { get; set; }

    }
    public class PhanBon
    {
        public int ID { get; set; }
        public string TenPhanBon { get; set; }
        public string LoaiPhanBon { get; set; }
        public string MoTa { get; set; }
        public DateTime NgaySanXuat { get; set; }
        public DateTime NgayHetHan { get; set; }
        public int CoSoID { get; set; }
    }
    public class SoLieuThongKe
    {
        public int ID { get; set; }
        public int CoSoID { get; set; }
        public int PhanBonID { get; set; }
        public int GiongCayID { get; set; }
        public int VungTrongID { get; set; }
        public int ThuocBVTVID { get; set; }
        public int SinhVatGayHaiVaTuoiSauID { get; set; }
        public int SoLuong { get; set; }
        public DateTime ThoiGianThongKe { get; set; }
    }
}
