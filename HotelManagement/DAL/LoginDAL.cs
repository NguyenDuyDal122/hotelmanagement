using System;
using System.Data.SqlClient;
using HotelManagement.DTO;

namespace HotelManagement.DAL
{
    public class LoginDAL
    {
        private string connectString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public LoginDTO GetUser(string username, string password)
        {
            using (SqlConnection connect = new SqlConnection(connectString))
            {
                connect.Open();
                string query = "SELECT id, role FROM [User] WHERE username = @username AND password_hash = @password";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string role = reader.GetString(1);
                            return new LoginDTO(id, username, role);
                        }
                    }
                }
            }
            return null;
        }
    }
}