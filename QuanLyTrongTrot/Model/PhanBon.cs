using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
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
}
