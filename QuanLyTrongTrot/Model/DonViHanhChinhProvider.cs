using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
    public class DonViHanhChinhProvider
    {
        // Chuỗi kết nối đến SQL Server
        private static string connectionString = "LAPTOP-ULJ4Q7AM\\KTPMUD20241;Initial Catalog=QuanLyTrongTrot;Persist Security Info=True;User ID=mailinh;Trust Server Certificate=True";

        // Lấy danh sách đơn vị hành chính
        public static List<DonViHanhChinh> GetDonViHanhChinh()
        {
            List<DonViHanhChinh> donViList = new List<DonViHanhChinh>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaDonVi, TenDonVi, CapDoID, CapTrenID FROM DonViHanhChinh";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DonViHanhChinh donVi = new DonViHanhChinh
                            {
                                MaDonVi = reader["MaDonVi"].ToString(),
                                TenDonVi = reader["TenDonVi"].ToString(),
                                CapDoID = Convert.ToInt32(reader["CapDoID"]),
                                CapTrenID = reader["CapTrenID"].ToString()
                            };

                            donViList.Add(donVi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return donViList;
        }

        // Thêm đơn vị hành chính mới
        public static bool AddDonViHanhChinh(DonViHanhChinh donVi)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO DonViHanhChinh (MaDonVi, TenDonVi, CapDoID, CapTrenID) VALUES (@MaDonVi, @TenDonVi, @CapDoID, @CapTrenID)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDonVi", donVi.MaDonVi);
                        cmd.Parameters.AddWithValue("@TenDonVi", donVi.TenDonVi);
                        cmd.Parameters.AddWithValue("@CapDoID", donVi.CapDoID);
                        cmd.Parameters.AddWithValue("@CapTrenID", donVi.CapTrenID ?? (object)DBNull.Value); // Nếu CapTrenID là null, sẽ truyền DBNull

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

        // Cập nhật đơn vị hành chính
        public static bool UpdateDonViHanhChinh(DonViHanhChinh donVi)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE DonViHanhChinh SET TenDonVi = @TenDonVi, CapDoID = @CapDoID, CapTrenID = @CapTrenID WHERE MaDonVi = @MaDonVi";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDonVi", donVi.MaDonVi);
                        cmd.Parameters.AddWithValue("@TenDonVi", donVi.TenDonVi);
                        cmd.Parameters.AddWithValue("@CapDoID", donVi.CapDoID);
                        cmd.Parameters.AddWithValue("@CapTrenID", donVi.CapTrenID ?? (object)DBNull.Value);

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

        // Xóa đơn vị hành chính theo MaDonVi
        public static bool DeleteDonViHanhChinh(string maDonVi)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM DonViHanhChinh WHERE MaDonVi = @MaDonVi";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDonVi", maDonVi);

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
    }
}
