using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyTrongTrot.View
{
	/// <summary>
	/// Interaction logic for HanhChinhPage.xaml
	/// </summary>
	public partial class HanhChinhPage : UserControl
	{
		string connectstring = @"LAPTOP - ULJ4Q7AM\KTPMUD20241; Initial Catalog = QuanLyTrongTrot; Persist Security Info = True; User ID = mailinh; Trust Server Certificate = True";
        public HanhChinhPage()
        {
            InitializeComponent();
            LoadData();
        }

        // Hàm tải dữ liệu từ SQL Server vào DataGrid
        private void LoadData()
        {
            try
            {
                // Tạo kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    // Mở kết nối đến cơ sở dữ liệu
                    connection.Open();

                    // Câu lệnh SQL để truy vấn dữ liệu từ các bảng
                    string query = "SELECT d.MaDonVi, d.TenDonVi, c.TenCapDo, d.CapTrenID FROM DonViHanhChinh d " +
                                   "JOIN CapDoHanhChinh c ON d.CapDoID = c.ID";

                    // Tạo đối tượng SqlDataAdapter để thực thi câu lệnh SQL
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Tạo đối tượng DataTable để lưu trữ kết quả truy vấn
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Gán dữ liệu vào DataGrid
                    dataGrid.DataContext = dataTable;  // dataGrid là tên DataGrid trong XAML
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có sự cố
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Hàm để thêm đơn vị hành chính mới
        private void AddDonVi(string maDonVi, string tenDonVi, int capDoID, string capTrenID)
        {
            try
            {
                // Mở kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();

                    // Câu lệnh SQL để thêm dữ liệu vào bảng DonViHanhChinh
                    string query = "INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID) " +
                                   "VALUES (@MaDonVi, @TenDonVi, @CapDoID, @CapTrenID)";

                    // Tạo đối tượng SqlCommand để thực thi câu lệnh SQL
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaDonVi", maDonVi);
                    command.Parameters.AddWithValue("@TenDonVi", tenDonVi);
                    command.Parameters.AddWithValue("@CapDoID", capDoID);
                    command.Parameters.AddWithValue("@CapTrenID", capTrenID);

                    // Thực thi câu lệnh SQL để thêm dữ liệu vào cơ sở dữ liệu
                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm đơn vị hành chính thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm đơn vị: " + ex.Message);
            }
        }

        // Hàm để xóa đơn vị hành chính
        private void DeleteDonVi(string maDonVi)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();

                    string query = "DELETE FROM DonViHanhChinh WHERE MaDonVi = @MaDonVi";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaDonVi", maDonVi);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Đơn vị hành chính đã được xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa đơn vị: " + ex.Message);
            }
        }
    }
}
