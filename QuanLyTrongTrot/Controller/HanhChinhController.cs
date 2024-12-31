
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using QuanLyTrongTrot.Model;
using System.Data;
using QuanLyTrongTrot.Utils;

namespace QuanLyTrongTrot.Controller
{
    public class HanhChinhController
    {
        private  Dbconnection _dataProvider = new Dbconnection();

        public HanhChinhController(Dbconnection dataProvider)
        {
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Lấy danh sách tất cả các đơn vị hành chính.
        /// </summary>
        /// <returns>Danh sách đơn vị hành chính</returns>
        public List<CapDoHanhChinh> GetCapDoHanhChinh()
        {
            var query = "SELECT ID, TenCapDo FROM CapDoHanhChinh";
            var dataTable = _dataProvider.Load(query);

            return dataTable.AsEnumerable().Select(row => new CapDoHanhChinh
            {
                ID = row.Field<int>("ID"),
                TenCapDo = row.Field<string>("TenCapDo"),
            }).ToList();
        }
        public List<DonViHanhChinh> GetDonViHanhChinh()
        {
            var query = "SELECT MaDonVi, TenDonVi, CapDoID, CapTrenID FROM DonViHanhChinh";
            var dataTable = _dataProvider.Load(query);
            return dataTable.AsEnumerable().Select(row => new DonViHanhChinh
            {
                MaDonVi = row.Field<string>("MaDonVi"),
                TenDonVi = row.Field<string>("TenDonVi"),
                CapDoID = row.Field<int>("CapDoID"),
                CapTrenID = row.Field<string>("CapTrenID"),
            }).ToList();
        }

        /// <summary>
        /// Tìm kiếm đơn vị hành chính theo từ khóa.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách đơn vị hành chính khớp với từ khóa</returns>
        public List<DonViHanhChinh> SearchDonViHanhChinh(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetDonViHanhChinh();

            keyword = Regex.Escape(keyword);

            var query = $"SELECT MaDonVi, TenDonVi, CapDoID, CapTrenID " +
                        $"FROM DonViHanhChinh " +
                        $"WHERE TenDonVi LIKE '%{keyword}%'";

            var dataTable = _dataProvider.Load(query);

            return dataTable.AsEnumerable().Select(row => new DonViHanhChinh
            {
                MaDonVi = row.Field<string>("MaDonVi"),
                TenDonVi = row.Field<string>("TenDonVi"),
                CapDoID = row.Field<int>("CapDoID"),
                CapTrenID = row.Field<string>("CapTrenID"),
            }).ToList();
        }
        public List<CapDoHanhChinh> SearchCapDoHanhChinh(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetCapDoHanhChinh();

            keyword = Regex.Escape(keyword);

            var query = "SELECT ID, TenCapDo " +
                        "FROM CapDoHanhChinh " +
                        "WHERE TenCapDo LIKE @keyword";

            var parameters = new Dictionary<string, object>
    {
        { "@keyword", $"%{keyword}%" }
    };

            var dataTable = _dataProvider.Load(query);

            var result = new List<CapDoHanhChinh>();

            foreach (DataRow row in dataTable.Rows)
            {
                result.Add(new CapDoHanhChinh
                {
                    ID = Convert.ToInt32(row["ID"]),
                    TenCapDo = row["TenCapDo"].ToString(),
                });
            }

            return result;
        }
        /// <summary>
        /// Thêm một đơn vị hành chính mới.
        /// </summary>
        /// <param name="model">Dữ liệu của đơn vị hành chính</param>
        /// <returns>Kết quả thêm (true/false)</returns>
        public bool AddCapDoHanhChinh(CapDoHanhChinh model)
        {
            var query = $"INSERT INTO CapDoHanhChinh (ID, TenCapDo) " +
                        $"VALUES ('{model.ID}', '{model.TenCapDo}')";

            return _dataProvider.Exec(query) != null;
        }
        public bool AddDonViHanhChinh(DonViHanhChinh model)
        {
            var query = $"INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID) " +
                        $"VALUES ('{model.MaDonVi}', '{model.TenDonVi}', '{model.CapDoID}', '{model.CapTrenID}')";
            return _dataProvider.Exec(query) != null;
        }

        /// <summary>
        /// Xóa một đơn vị hành chính.
        /// </summary>
        /// <param name="id">ID của đơn vị hành chính</param>
        /// <returns>Kết quả xóa (true/false)</returns>
        public bool DeleteCapDoHanhChinh(int id)
        {
            var query = $"DELETE FROM CapDoHanhChinh WHERE ID = {id}";

            return _dataProvider.Exec(query) != null;
        }
        public bool DeleteDonViHanhChinh(int ID)
        {
            var query = $"DELETE FROM DonViHanhChinh WHERE CapDoID = {ID}";
            return _dataProvider.Exec(query) == null;
        }

        /// <summary>
        /// Cập nhật thông tin một đơn vị hành chính.
        /// </summary>
        /// <param name="model">Dữ liệu của đơn vị hành chính</param>
        /// <returns>Kết quả cập nhật (true/false)</returns>
        public bool UpdateCapDoHanhChinh(CapDoHanhChinh model)
        {
            var query = $"UPDATE CapDoHanhChinh " +
                        $"SET TenCapDo = '{model.TenCapDo}'" +
                        $"WHERE ID = {model.ID}";

            return _dataProvider.Exec(query) != null;
        }
        public bool UpdateDonViHanhChinh(DonViHanhChinh model)
{
            var query = "UPDATE DonViHanhChinh " +
                        "SET MaDonVi = @MaDonVi, TenDonVi = @TenDonVi, CapTrenID = @CapTrenID " +
                        "WHERE CapDoID = @CapDoID";

            return _dataProvider.Exec(query) != null;
        }
    }

}
