using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
    public class VaiTroProvider
    {
        // Chuỗi kết nối đến SQL Server
        private static string connectionString = "LAPTOP-ULJ4Q7AM\\KTPMUD20241;Initial Catalog=QuanLyTrongTrot;Persist Security Info=True;User ID=mailinh;Trust Server Certificate=True";

        // Lấy danh sách vai trò
        public static List<VaiTro> GetVaiTro()
        {
            List<VaiTro> vaiTroList = new List<VaiTro>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ID, TenVaiTro FROM VaiTro";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            VaiTro vaiTro = new VaiTro
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                TenVaiTro = reader["TenVaiTro"].ToString()
                            };

                            vaiTroList.Add(vaiTro);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return vaiTroList;
        }

        // Thêm vai trò mới
        public static bool AddVaiTro(VaiTro vaiTro)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO VaiTro (ID, TenVaiTro) VALUES (@ID, @TenVaiTro)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", vaiTro.ID);
                        cmd.Parameters.AddWithValue("@TenVaiTro", vaiTro.TenVaiTro);

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

        // Cập nhật vai trò
        public static bool UpdateVaiTro(VaiTro vaiTro)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE VaiTro SET TenVaiTro = @TenVaiTro WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", vaiTro.ID);
                        cmd.Parameters.AddWithValue("@TenVaiTro", vaiTro.TenVaiTro);

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

        // Xóa vai trò theo ID
        public static bool DeleteVaiTro(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM VaiTro WHERE ID = @ID";

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
    }
}


