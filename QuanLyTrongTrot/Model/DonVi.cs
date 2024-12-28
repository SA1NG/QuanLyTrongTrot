using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace QuanLyTrongTrot.Model
{
    partial class ViewDonVi : DonViHanhChinh
    {
        public string TenDayDu => $"{TenDonVi}";
    }
    partial class DonVi
    {
        static List<ViewDonVi> _all;
        static public List<ViewDonVi> All
        {
            get
            {
                if (_all == null || _all.Count == 0)
                    _all = Provider.Current.Select<ViewDonVi>();
                return _all;
            }
        }
        static public int? CapHanhChinhDangXuLy { get; set; }
        static public List<ViewDonVi> DanhSach(int? cap)
        {
            if (cap == null)
                return All;

            var lst = new List<ViewDonVi>();
            lst.AddRange(All.Where(x => x.CapDoID == cap));
            return lst;
        }

        static DonViHanhChinh[] _hanhChinh;
        static public DonViHanhChinh[] HanhChinh
        {
            get
            {
                if (_hanhChinh == null)
                {
                    _hanhChinh = Provider.Current.Select<DonViHanhChinh>().ToArray();
                }
                return _hanhChinh;
            }
        }
    }
}
