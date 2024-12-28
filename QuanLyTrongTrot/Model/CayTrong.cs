using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
    public class CayTrong
    {
        public int ID { get; set; } 
        public string TenGiongCay { get; set; } 
        public int LoaiCayTrongID { get; set; } 
        public string MoTa { get; set; } 
        public int VungTrongID { get; set; }
    }
}
