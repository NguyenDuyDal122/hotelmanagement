using System;
using System.Data.SqlClient;
using HotelManagement.DTO;

namespace HotelManagement.DAL
{
    public class SuaTaiKhoanDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public SuaTaiKhoanDTO GetUserById(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, username, full_name, phone, email, role FROM [User] WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new SuaTaiKhoanDTO
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
            return null;
        }

        public bool CheckDuplicate(SuaTaiKhoanDTO user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM [User] WHERE (username = @username OR phone = @phone OR email = @email) AND id != @id";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@username", user.Username);
                    checkCmd.Parameters.AddWithValue("@phone", user.Phone);
                    checkCmd.Parameters.AddWithValue("@email", user.Email);
                    checkCmd.Parameters.AddWithValue("@id", user.Id);
                    int exists = (int)checkCmd.ExecuteScalar();
                    return exists > 0;
                }
            }
        }

        public bool UpdateUser(SuaTaiKhoanDTO user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string updateQuery = "UPDATE [User] SET username=@username, full_name=@fullName, phone=@phone, email=@email, role=@role";
                if (!string.IsNullOrEmpty(user.Password))
                {
                    updateQuery += ", password_hash=@password_hash";
                }
                updateQuery += " WHERE id=@id";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@fullName", user.FullName);
                    cmd.Parameters.AddWithValue("@phone", user.Phone);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        cmd.Parameters.AddWithValue("@password_hash", user.Password);
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
