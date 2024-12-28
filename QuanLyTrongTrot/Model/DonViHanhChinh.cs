using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
    public class DonViHanhChinh
    {
        public string MaDonVi { get; set; } // Khóa chính
        public string TenDonVi { get; set; } // Tên đơn vị hành chính
        public int CapDoID { get; set; } // Liên kết đến bảng CapDoHanhChinh
        public string CapTrenID { get; set; } // Liên kết đến đơn vị hành chính cấp trên
    }
}
