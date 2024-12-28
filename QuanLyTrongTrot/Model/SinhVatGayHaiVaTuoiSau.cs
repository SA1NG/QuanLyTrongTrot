using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
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
}
