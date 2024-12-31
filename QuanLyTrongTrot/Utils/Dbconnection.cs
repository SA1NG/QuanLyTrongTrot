using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyTrongTrot.Model;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Reflection;
namespace QuanLyTrongTrot.Utils
{
    public class Dbconnection:DbContext
    {
        public string ConnectionString = "Server=LAPTOP-J7UA5U3P;Database=QuanLyTrongTrot;Trusted_Connection=True;";
        public DbSet<CapDoHanhChinh> CapDoHanhChinh { get; set; }

        public DbSet<DonViHanhChinh> DonViHanhChinh { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public void CreateCommand(Action<SqlCommand> callback)
        {
            var cmd = new SqlCommand
            {
                Connection = Open()
            };
            callback(cmd);
            Close();
        }
        SqlConnection _conn;
        public SqlConnection Open()
        {
            if (_conn == null)
                _conn = new SqlConnection(ConnectionString);
            if (_conn.State != ConnectionState.Open)
                _conn.Open();

            return _conn;
        }
        public void Close()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
                _conn.Close();
        }
        public DataTable Load(string sql)
        {
            DataTable table = new DataTable();
            table.BeginLoadData();

            CreateCommand(cmd => {
                cmd.CommandText = sql;

                var reader = cmd.ExecuteReader();
                table.Load(reader);
            });

            table.EndLoadData();
            return table;
        }
        public class CommandResult
        {
            public object Scalar { get; set; }
            public int NonQuery { get; set; }
            public DataTable Select { get; set; }
            public List<T> GetEntity<T>()
            {
                var lst = new List<T>();
                if (Select == null)
                    return lst;

                var typ = typeof(T);

                var cols = new Dictionary<PropertyInfo, DataColumn>();
                foreach (DataColumn c in Select.Columns)
                {
                    var prop = typ.GetProperty(c.ColumnName);
                    if (prop != null && prop.CanWrite)
                    {
                        cols.Add(prop, c);
                    }
                }

                foreach (DataRow r in Select.Rows)
                {
                    var e = (T)Activator.CreateInstance(typ);
                    foreach (var p in cols)
                    {
                        object v = r[p.Value];
                        if (v != DBNull.Value)
                        {
                            p.Key.SetValue(e, r[p.Value]);
                        }
                    }
                    lst.Add(e);
                }
                return lst;
            }
        }
        public CommandResult Result { get; private set; } = new CommandResult();
        public Dbconnection Exec(string sql)
        {
            Result.Scalar = null;
            CreateCommand(cmd => {
                cmd.CommandText = sql;
                Result.Scalar = cmd.ExecuteScalar();
            });
            return this;
        }
    }
}
