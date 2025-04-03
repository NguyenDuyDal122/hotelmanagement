using System;
using System.Data.SqlClient;
using HotelManagement.DTO;

namespace HotelManagement.DAL
{
    public class ThongTinCaNhanDAL
    {
        private string connectstring = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public ThongTinCaNhanDTO GetUserInfo(int userId)
        {
            ThongTinCaNhanDTO userInfo = null;
            try
            {
                using (SqlConnection connect = new SqlConnection(connectstring))
                {
                    connect.Open();
                    string query = "SELECT id, username, full_name, phone, email, role FROM [User] WHERE id = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userInfo = new ThongTinCaNhanDTO
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Username = reader["username"].ToString(),
                                    FullName = reader["full_name"].ToString(),
                                    Phone = reader["phone"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Role = reader["role"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải thông tin người dùng: " + ex.Message);
            }
            return userInfo;
        }
    }
}
