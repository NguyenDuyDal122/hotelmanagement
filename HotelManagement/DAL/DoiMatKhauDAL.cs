namespace HotelManagement.DAL
{
    using System.Data.SqlClient;
    using HotelManagement.DTO;

    public class DoiMatKhauDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False"; // Cập nhật chuỗi kết nối

        public bool CheckOldPassword(DoiMatKhauDTO user, string oldPassword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryCheck = "SELECT COUNT(*) FROM [User] WHERE id = @userId AND password_hash = @password";
                using (SqlCommand cmd = new SqlCommand(queryCheck, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", user.Id);
                    cmd.Parameters.AddWithValue("@password", oldPassword);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool UpdatePassword(DoiMatKhauDTO user, string newPassword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryUpdate = "UPDATE [User] SET password_hash = @newPassword WHERE id = @userId";
                using (SqlCommand cmd = new SqlCommand(queryUpdate, conn))
                {
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.Parameters.AddWithValue("@userId", user.Id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
