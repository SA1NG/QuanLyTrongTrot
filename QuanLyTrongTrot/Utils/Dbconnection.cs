using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyTrongTrot.Model;

namespace QuanLyTrongTrot.Utils
{
    internal class Dbconnection:DbContext
    {
        public DbSet<CapDoHanhChinh> CapDoHanhChinh { get; set; }

        public DbSet<DonViHanhChinh> DonViHanhChinh { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-J7UA5U3P;Database=QuanLyTrongTrot;Trusted_Connection=True;");
        }
    }
}
