using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Globalization;

namespace QuanLyTrongTrot.View
{
	/// <summary>
	/// Interaction logic for HanhChinhPage.xaml
	/// </summary>
	public partial class HanhChinhPage : UserControl
	{
        // Chuỗi kết nối tới cơ sở dữ liệu
        string connectstring = @"LAPTOP - ULJ4Q7AM\KTPMUD20241; Initial Catalog = QuanLyTrongTrot; Persist Security Info = True; User ID = mailinh; Trust Server Certificate = True";

        public HanhChinhPage()
        {
            InitializeComponent();
           LoadData();  // Gọi hàm LoadData để tải dữ liệu
        }

        // Hàm tải dữ liệu từ SQL Server vào DataGrid
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();  // Mở kết nối

                    string query = "SELECT d.MaDonVi, d.TenDonVi, c.TenCapDo, d.CapTrenID FROM DonViHanhChinh d " +
                                   "JOIN CapDoHanhChinh c ON d.CapDoID = c.ID";  // Câu lệnh SQL truy vấn dữ liệu

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);  // Lưu dữ liệu vào DataTable

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        } 

        // Hàm thêm đơn vị hành chính mới
        private void AddDonVi(string maDonVi, string tenDonVi, int capDoID, string capTrenID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();  // Mở kết nối

                    string query = "INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID) " +
                                   "VALUES (@MaDonVi, @TenDonVi, @CapDoID, @CapTrenID)";  // Câu lệnh SQL thêm dữ liệu

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaDonVi", maDonVi);
                    command.Parameters.AddWithValue("@TenDonVi", tenDonVi);
                    command.Parameters.AddWithValue("@CapDoID", capDoID);
                    command.Parameters.AddWithValue("@CapTrenID", capTrenID);

                    command.ExecuteNonQuery();  // Thực thi câu lệnh SQL
                    MessageBox.Show("Thêm đơn vị hành chính thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm đơn vị: " + ex.Message);
            }
        }

        // Hàm xóa đơn vị hành chính
        private void DeleteDonVi(string maDonVi)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();  // Mở kết nối

                    string query = "DELETE FROM DonViHanhChinh WHERE MaDonVi = @MaDonVi";  // Câu lệnh SQL xóa dữ liệu
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaDonVi", maDonVi);

                    command.ExecuteNonQuery();  // Thực thi câu lệnh SQL
                    MessageBox.Show("Đơn vị hành chính đã được xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa đơn vị: " + ex.Message);
            }
        }

        // Sự kiện khi nhấn nút "Thêm Đơn Vị"
        private void addbutton_click(object sender)
        {
            // lấy thông tin đơn vị từ textbox hoặc các nguồn khác
            string madonvi = "dv001";
            string tendonvi = "đơn vị 1";
            int capdoid = 1;  // chọn cấp độ từ một combobox chẳng hạn
            string captrenid = null;  // cấp trên có thể là null nếu không có cấp trên

            AddDonVi(madonvi, tendonvi, capdoid, captrenid);  // gọi hàm thêm đơn vị
            LoadData();  // tải lại dữ liệu sau khi thêm
        }

        // sự kiện khi nhấn nút "xóa đơn vị"
         private void deletebutton_click(object sender)
        {
           string madonvi = "dv001";  // mã đơn vị cần xóa, lấy từ textbox hoặc các nguồn khác

           DeleteDonVi(madonvi);  // gọi hàm xóa đơn vị
          LoadData();  // tải lại dữ liệu sau khi xóa
         }
}
}
