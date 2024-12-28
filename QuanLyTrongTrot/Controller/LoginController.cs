using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Controller
{
    using QuanLyTrongTrot.Model;

    class LoginController : BaseController
    {
        static int remain = 3;
        public override object Error(int code, string message)
        {
            if (remain == 0)
            {
                App.Current.Shutdown();
                return null;
            }
            message += $".\nĐược phép sai thêm {--remain} lần.";
            return base.Error(code, message);
        }

        public override object Index()
        {
            return View(new EditContext(new NguoiDung { TenNguoiDung = "admin", MatKhau = "1234" }));
        }
        public object Update(EditContext context)
        {
            var acc = (NguoiDung)context.Model;
            var pass = acc.MatKhau;
            acc = Provider.Find<NguoiDung>(acc.TenNguoiDung);
            if (acc == null)
            {
                return Error(404, "Người dùng không tồn tại");
            }
            if (acc.MatKhau != pass)
            {
                return Error(404, "Sai mật khẩu");
            }
        return null;

        }
    }
    
}
