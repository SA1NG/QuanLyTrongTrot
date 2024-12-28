using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
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
