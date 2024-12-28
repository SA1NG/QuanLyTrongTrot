using QuanLyTrongTrot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Controller
{
    partial class HanhChinhController: BaseController
    {
        protected override ViewDonVi CreateEntity() => new ViewDonVi { CapDoID = DonVi.CapHanhChinhDangXuLy };
        public object Add(ViewDonVi one) => View(new EditContext { Model = one, Action = EditActions.Insert });
        public override object Index()
        {
            return View(Select(null));
        }
        protected object Select(int? cap)
        {
            return DonVi.DanhSach(DonVi.CapHanhChinhDangXuLy = cap);
        }
        protected override void TryDelete(ViewDonVi e)
        {
            Provider.Exec($"select top(1) id from DonVi where CapTrenID={e.CapDoID}");
            if (Provider.Result.Scalar != null)
            {
                UpdateContext.Message = $"Cần xóa tất cả đơn vị con của {e.TenDonVi}";
                return;
            }

            Exec(e.MaDonVi);
            DonVi.All.Remove(e);
        }

        protected override void TryInsert(ViewDonVi e)
        {
            Exec(null, e.MaDonVi, e.TenDonVi, e.CapDoID, e.CapTrenID);
            DonVi.All.Clear();
        }

        protected override void TryUpdate(ViewDonVi e)
        {
            Exec(e.MaDonVi, e.TenDonVi, e.CapDoID, e.CapTrenID);
        }
    }
}
