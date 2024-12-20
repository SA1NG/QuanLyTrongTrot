using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace QuanLyTrongTrot.Model
{
    public class ToaDo
    {
        public int ID { get; set; } 
        public int CoSoID { get; set; } 
        public decimal KinhDo { get; set; } 
        public decimal ViDo { get; set; } 
        public int KhuSVID { get; set; }
    }
}
