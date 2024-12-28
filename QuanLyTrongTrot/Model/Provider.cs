using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace QuanLyTrongTrot.Model
{
        public class DataSchema
        {
            public Dictionary<string, Table> Tables { get; set; }
            public DataSchema(Provider engine) { provider = engine; }

            #region Migrate
            Provider provider;
             // Lớp Paragraph dùng để xây dựng các đoạn mã với định dạng phù hợp
            class Paragraph : List<string>
            {
                int tab;
                // Phương thức chỉnh mức thụt lề
                public Paragraph Tab(int n)
                {
                    tab += n;
                    return this;
                }
                // Bắt đầu một đoạn mới
                public Paragraph Start(string s)
                {
                    Clear();

                    tab = 0;
                    Add(s);

                    return this;
            }
                // Mở một khối mã
                public Paragraph OpenBlock(string s)
                {
                    Append(s);
                    ++tab;
                    return this;
                }
                // Đóng một khối mã
                public Paragraph CloseBlock(string s)
                {
                    --tab;
                    return Append(s);
                }
                public Paragraph Append(string s)
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        return this;
                    }

                    Add(new string(' ', tab << 2) + s);
                    return this;
                }

                public override string ToString()
                {
                    return string.Join("\r\n", this);
                }
            }
            public class Column
            {
                public bool IsIdentity { get; set; }
                public bool IsNullable { get; set; }
                public string Name { get; set; }
                public string Type { get; set; }
                public string Size { get; set; }
                public string Model
                {
                    get
                    {
                        var t = _dataTypes[Type];
                        if (t != "string")
                            t += '?';
                        return $"public {t} {Name} {{ get; set; }}";
                    }
                }
                public string ProcName => $"@{Name}";
                public string ProcParam
                {
                    get
                    {
                        var s = $"@{Name} {Type}";
                        if (!string.IsNullOrEmpty(Size))
                            s += $"({Size})";

                        if (IsIdentity) s += " output";
                        if (IsNullable) s += " = NULL";
                        return s;
                    }
                }
                public string EQ => $"{Name} = @{Name}";
                public override string ToString()
                {
                    return Model;
                }
            }
            public class Table
            {
                public string Name { get; set; }
                public string Type { get; set; }
                public Column PK { get; set; }
                public List<Column> Columns { get; set; } = new List<Column>();

                public string GetModel()
                {
                    var p = new Paragraph()
                        .Start("public partial class " + Name)
                        .OpenBlock("{");

                    foreach (var c in Columns)
                        p.Append(c.Model);

                    return p.CloseBlock("}").ToString();
                }
                public string GetProc()
                {
                    var pk = PK ?? new Column { Name = "Id", Type = "int" };
                    var p = new Paragraph()
                        .Start($"drop proc update{Name}")
                        .Append("go")
                        .Append($"create proc update{Name}")
                        .Append("( @action int");

                    foreach (var c in Columns)
                    {
                        p.Append($", {c.ProcParam}");
                    }
                    p.Append(") as").OpenBlock("BEGIN");

                    p.Append("if @action = -1")
                        .OpenBlock("begin")
                        .Append($"delete from {Name} where " + pk.EQ)
                        .Append("return")
                        .CloseBlock("end");

                    p.Append("if @action = 0")
                        .OpenBlock("begin")
                        .Append($"update {Name} set").Tab(1);

                    int n = Columns.Count - 1;
                    foreach (var c in Columns)
                    {
                        if (c != pk)
                        {
                            p.Append(c.EQ + (--n > 0 ? "," : ""));
                        }
                    }

                    p.Append($"where {pk.EQ}")
                        .Tab(-1).Append("return")
                        .CloseBlock("end");

                    p.Append($"insert into {Name} values (");
                    var s = "";
                    Column iden = null;
                    foreach (var c in Columns)
                    {
                        if (c.IsIdentity) { iden = c; continue; }
                        if (s != string.Empty)
                            s += ',';
                        s += c.ProcName;
                    }
                    p.Tab(1).Append(s).CloseBlock(")");
                    if (iden != null)
                    {
                        p.Append($"set {iden.ProcName} = @@IDENTITY");
                    }
                    return p.CloseBlock("END").Append("go").ToString();
                }
                public string GetController()
                {
                    return $"partial class {Name}Controller : DataController<Models.{Name}> {{ }}";
                }
            }

            static Dictionary<string, string> _dataTypes = new Dictionary<string, string> {
            { "varbinary", "byte[]" },
            { "binary", "byte[]" },
            { "image", "string" },
            { "varchar", "string" },
            { "char", "string" },
            { "nvarchar", "string" },
            { "nchar", "string" },
            { "text", "string" },
            { "ntext", "string" },
            { "uniqueidentifier", "Guid" },
            { "rowversion", "byte[]" },
            { "bit", "bool" },
            { "tinyint", "byte" },
            { "smallint", "short" },
            { "int", "int" },
            { "bigint", "long" },
            { "smallmoney", "decimal" },
            { "money", "decimal" },
            { "numeric", "decimal" },
            { "decimal", "decimal" },
            { "real", "double" },
            { "float", "double" },
            { "smalldatetime", "DateTime" },
            { "datetime", "DateTime" },
            { "date", "DateTime" },
            { "sql_variant", "object" },
            { "table", "string" },
            { "cursor", "string" },
            { "timestamp", "string" },
            { "xml", "string" },
        };

            public void Run(string sql, Action<DataRow> callback)
            {
                var dt = provider.Load(sql);
                foreach (DataRow r in dt.Rows)
                    callback(r);
            }
            #endregion

            public DataSchema Start()
            {
                var sql = "SELECT TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES";
                Tables = new Dictionary<string, Table>();
                Run(sql, r => {
                    var name = r[0].ToString();
                    Tables.Add(name, new Table
                    {
                        Name = name,
                        Type = r[1].ToString(),
                    });
                });

                TableLoaded?.Invoke();
                return this;
            }
            public DataSchema GetColumns()
            {
                var sql = "SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH "
                    + "FROM INFORMATION_SCHEMA.COLUMNS";

                Run(sql, r => {
                    var name = r[0].ToString();
                    var tabl = Tables[name];

                    var col = new Column
                    {
                        Name = r[1].ToString(),
                        Type = r[2].ToString(),
                        IsNullable = r[3].Equals("YES"),
                    };

                    if (col.Type.Contains("char"))
                    {
                        col.Size = r[4].ToString();
                    }

                    tabl.Columns.Add(col);
                });

                sql = "SELECT TABLE_NAME, COLUMN_NAME, CONSTRAINT_NAME from "
                    + "INFORMATION_SCHEMA.KEY_COLUMN_USAGE";
                Run(sql, r => {
                    var name = r[0].ToString();
                    var cs = r[2].ToString();
                    if (cs[0] == 'P')
                    {
                        var t = Tables[name];
                        name = r[1].ToString();
                        t.PK = t.Columns.Find(x => x.Name == name);
                    }
                });

                sql = "select o.name, c.name from sys.objects o "
                    + "inner join sys.columns c "
                    + "on o.object_id = c.object_id and c.is_identity = 1";

                Run(sql, r => {
                    Table t;
                    if (Tables.TryGetValue(r[0].ToString(), out t))
                    {
                        var c = t.Columns.Find(x => r[1].Equals(x.Name));
                        c.IsIdentity = true;
                    }
                });
                return this;
            }

            public event Action GenerateCompleted;
            public event Action TableLoaded;
            public string GenerateMode { get; set; }
            string _result;
            public string Result
            {
                get => _result;
                set
                {
                    _result = value;
                    GenerateCompleted?.Invoke();
                }
            }
            public DataSchema Generate(Table table)
            {
                var lst = new List<object>();
                var fn = typeof(Table).GetMethod("Get" + char.ToUpper(GenerateMode[0]) + GenerateMode.Substring(1));

                IEnumerable<Table> src = Tables.Values;
                if (table != null)
                    src = new Table[] { table };

                foreach (Table e in src.OrderBy(x => x.Name))
                {
                    lst.Add(fn.Invoke(e, new object[] { }));
                }

                Result = string.Join("\r\n\r\n", lst);
                return this;
            }
            public DataSchema Migrate()
            {
                Start();
                GetColumns();
                return this;
            }
        }
        public class Provider
        {
            static public Provider Current { get; private set; }
            public Provider()
            {
                Current = this;
            }

            DataSchema _schema;
            public DataSchema Schema
            {
                get
                {
                    if (_schema == null)
                        _schema = new DataSchema(this);
                    return _schema;
                }
            }

            string _hostName = @"LAPTOP-ULJ4Q7AM\KTPMUD20241";
            string _dataName = @"QuanLyTrongTrot";
            SqlConnection _conn;

            public string HostName
            {
                get => _hostName;
                set
                {
                    _hostName = value;
                    _conn = null;
                }
            }
            public string DataName
            {
                get => _dataName;
                set
                {
                    _dataName = value;
                    _conn = null;
                }
            }
            public string ConnectionString => $"Data Source={_hostName};Initial Catalog={_dataName};Integrated Security=True";
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
            public void CreateCommand(Action<SqlCommand> callback)
            {
                var cmd = new SqlCommand
                {
                    Connection = Open()
                };
                callback(cmd);
                Close();
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

            /// <summary>
            /// Đọc dữ liệu từ table
            /// </summary>
            /// <param name="sql">Câu lệnh SQL (select)</param>
            /// <returns>Đối tượng kiểu DataTable</returns>
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
            /// <summary>
            /// Chạy câu lệnh SQL
            /// </summary>
            /// <param name="sql">Câu lệnh SQL</param>
            /// <returns>Ô đầu tiên của bảng dữ liệu, trả về null nếu không có hàng nào trong bảng</returns>
            public Provider Exec(string sql)
            {
                Result.Scalar = null;
                CreateCommand(cmd => {
                    cmd.CommandText = sql;
                    Result.Scalar = cmd.ExecuteScalar();
                });
                return this;
            }
            /// <summary>
            /// Chạy chương trình con hoặc câu SQL (insert, update, delete)
            /// </summary>
            /// <param name="sql">Câu lệnh SQL</param>
            /// <returns>Số bản ghi được xử lý</returns>
            public Provider RunProc(string sql)
            {
                Result.NonQuery = 0;
                CreateCommand(cmd => {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.StoredProcedure;
                    Result.NonQuery = cmd.ExecuteNonQuery();
                });
                return this;
            }

            public CommandResult Result { get; private set; } = new CommandResult();
            public CommandResult Select(string tableName)
            {
                return Select(tableName, null, null);
            }
            public CommandResult Select(string tableName, string filter)
            {
                return Select(tableName, filter, null);
            }
            public CommandResult Select(string tableName, string filter, string sort)
            {
                var sql = "SELECT * FROM " + tableName;
                if (!string.IsNullOrWhiteSpace(filter))
                    sql += " WHERE " + filter;
                if (!string.IsNullOrWhiteSpace(sort))
                    sql += " ORDER BY " + sort;

                Result.Select = Load(sql);
                return Result;
            }

            public List<T> Select<T>() => Select(typeof(T).Name).GetEntity<T>();
            public List<T> GetOptions<T>(string valueName, string displayName)
            {
                Result.Select = Load($"SELECT {valueName}, {displayName} FROM {typeof(T).Name} ORDER BY {displayName}");
                return Result.GetEntity<T>();
            }
            public List<object> GetOptions(string tableName, string valueName)
            {
                var lst = new List<object>();
                Schema.Run($"SELECT {valueName} FROM {tableName} ORDER BY {valueName}", r => lst.Add(r[0]));

                return lst;
            }
            public T Find<T>(object key)
            {
                if (Schema.Tables == null)
                {
                    Schema.Start().GetColumns();
                }

                var name = typeof(T).Name;
                var table = Schema.Tables[name];
                Select(name, $"{table.PK.Name}='{key}'");

                var lst = Result.GetEntity<T>();
                return lst.Count == 0 ? default(T) : lst[0];
            }
        }
    }
