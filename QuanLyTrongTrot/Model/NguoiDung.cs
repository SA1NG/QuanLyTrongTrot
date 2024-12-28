using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
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
}
