using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Controller
{
    class MeController : BaseController
    {
        // Hiển thị trang thay đổi mật khẩu
        public override object Index() => View();
        // Hiển thị trang nhập mật khẩu mới

        public object ChangePass() => View();
        public object ChangePass(string value)
        {
            Provider.Exec($"exec changePass '{App.User.UserName}', '{value}'");
            return Redirect("home/logout");
        }
    }
}
