using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
    internal class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public bool IsOnline { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public bool IsGuest { get; set; }
        public bool IsUser { get; set; } = false;


    }
}
