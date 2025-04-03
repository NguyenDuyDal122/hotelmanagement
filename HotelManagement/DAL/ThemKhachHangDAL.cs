using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagement
{
    public class ThemKhachHangDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        // Kiểm tra xem dữ liệu đã tồn tại trong cơ sở dữ liệu chưa
        public bool CheckIfExists(string phone, string email, string identityCard)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM [Customer] WHERE phone = @phone OR email = @email OR identity_card = @identity_card";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@identity_card", identityCard);

                        int exists = (int)cmd.ExecuteScalar();
                        return exists > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Lỗi khi kiểm tra dữ liệu.");
            }
        }

        // Thêm khách hàng vào cơ sở dữ liệu
        public bool AddCustomer(ThemKhachHangDTO customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO [Customer] (full_name, phone, email, address, identity_card) VALUES (@full_name, @phone, @email, @address, @identity_card)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@full_name", customer.FullName);
                        cmd.Parameters.AddWithValue("@phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@email", customer.Email);
                        cmd.Parameters.AddWithValue("@address", customer.Address);
                        cmd.Parameters.AddWithValue("@identity_card", customer.IdentityCard);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Lỗi khi thêm khách hàng.");
            }
        }
    }
}
