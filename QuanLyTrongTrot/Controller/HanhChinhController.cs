using QuanLyTrongTrot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Controller
{
    partial class HanhChinhController : DataController<ViewDonVi> 
    {
        protected override ViewDonVi CreateEntity() => new ViewDonVi { CapDoID = (int)DonVi.CapHanhChinhDangXuLy };
        public object Add(ViewDonVi one) => View(new EditContext { Model = one, Action = EditActions.Insert });
        public override object Index()
        {
            return View(Select(null));
        }
        protected object Select(int? cap)
        {
            return DonVi.DanhSach(DonVi.CapHanhChinhDangXuLy = cap ?? 0);
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
        // Chức năng tìm kiếm đơn vị hành chính
        public object Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return View(DonVi.All);

            var result = DonVi.All.Where(dv => dv.TenDonVi.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                dv.MaDonVi.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (!result.Any())
            {
                UpdateContext.Message = "Không tìm thấy đơn vị hành chính nào phù hợp.";
                return View(new List<ViewDonVi>());
            }

            return View(result);
        }
    }
}
