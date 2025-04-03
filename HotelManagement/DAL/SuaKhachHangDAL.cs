using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagement
{
    public class SuaKhachHangDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        // Lấy dữ liệu khách hàng theo ID
        public ManageCustomerDTO GetCustomerById(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT full_name, phone, email, address, identity_card FROM [Customer] WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return new ManageCustomerDTO
                            {
                                Id = id,
                                FullName = reader["full_name"].ToString(),
                                Phone = reader["phone"].ToString(),
                                Email = reader["email"].ToString(),
                                Address = reader["address"].ToString(),
                                IdentityCard = reader["identity_card"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Lỗi khi tải dữ liệu khách hàng.");
            }
            return null;
        }

        // Kiểm tra xem có trùng lặp thông tin (phone, email, identity card) với những khách hàng khác
        public bool CheckIfExists(ManageCustomerDTO customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM [Customer] WHERE (phone = @phone OR email = @email OR identity_card = @identity_card) AND id != @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@email", customer.Email);
                        cmd.Parameters.AddWithValue("@identity_card", customer.IdentityCard);
                        cmd.Parameters.AddWithValue("@id", customer.Id);
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

        // Cập nhật dữ liệu khách hàng
        public bool UpdateCustomer(ManageCustomerDTO customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE [Customer] SET full_name=@full_name, phone=@phone, email=@email, address=@address, identity_card=@identity_card WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@full_name", customer.FullName);
                        cmd.Parameters.AddWithValue("@phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@email", customer.Email);
                        cmd.Parameters.AddWithValue("@address", customer.Address);
                        cmd.Parameters.AddWithValue("@identity_card", customer.IdentityCard);
                        cmd.Parameters.AddWithValue("@id", customer.Id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Lỗi khi cập nhật thông tin khách hàng.");
            }
        }
    }
}
