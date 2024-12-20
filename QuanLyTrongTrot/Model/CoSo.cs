using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
    internal class CoSo
    {
        public int MaCoSo { get; set; } 
        public string TenCoSo { get; set; } 
        public string DiaChi { get; set; } 
        public DateTime NgayCapGiayPhep { get; set; } 
        public int LoaiCoSoID { get; set; } 
        public byte TinhTrang { get; set; } 
        public string MaHanhChinh { get; set; } 
    }
}
