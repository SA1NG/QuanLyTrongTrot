using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
    public static class CapDoHanhChinhProvider
    {
        // Chuỗi kết nối đến SQL Server
        private static string connectionString = "LAPTOP-ULJ4Q7AM\\KTPMUD20241;Initial Catalog=QuanLyTrongTrot;Persist Security Info=True;User ID=mailinh;Trust Server Certificate=True";

        // Lấy danh sách cấp độ hành chính
        public static List<CapDoHanhChinh> GetCapDoHanhChinh()
        {
            List<CapDoHanhChinh> capDoList = new List<CapDoHanhChinh>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ID, TenCapDo FROM CapDoHanhChinh";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CapDoHanhChinh capDo = new CapDoHanhChinh
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                TenCapDo = reader["TenCapDo"].ToString()
                            };

                            capDoList.Add(capDo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return capDoList;
        }

        // Thêm cấp độ hành chính mới
        public static bool AddCapDoHanhChinh(CapDoHanhChinh capDo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO CapDoHanhChinh (TenCapDo) VALUES (@TenCapDo)"; // Không cần ID vì sẽ tự động tăng

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenCapDo", capDo.TenCapDo);

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        // Xóa cấp độ hành chính theo ID
        public static bool DeleteCapDoHanhChinh(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM CapDoHanhChinh WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        // Cập nhật cấp độ hành chính
        public static bool UpdateCapDoHanhChinh(CapDoHanhChinh capDo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE CapDoHanhChinh SET TenCapDo = @TenCapDo WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@TenCapDo", capDo.TenCapDo);
                        cmd.Parameters.AddWithValue("@ID", capDo.ID);

                        // Thực thi câu lệnh SQL
                        cmd.ExecuteNonQuery();
                    }
                }

                return true; // Trả về true nếu cập nhật thành công
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, in thông báo lỗi và trả về false
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}