
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Controller
{
    internal class BaseController : System.Mvc.Controller
    {
        public virtual object Index() => View();
    }
}
