using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using ClosedXML.Excel;

namespace HotelManagement
{
    public class ManageCustomerDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        // Lấy danh sách khách hàng
        public List<ManageCustomerDTO> GetCustomerList()
        {
            List<ManageCustomerDTO> customerList = new List<ManageCustomerDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, full_name, phone, email, address, identity_card FROM [Customer]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        customerList.Add(new ManageCustomerDTO
                        {
                            Id = Convert.ToInt32(row["id"]),
                            FullName = row["full_name"].ToString(),
                            Phone = row["phone"].ToString(),
                            Email = row["email"].ToString(),
                            Address = row["address"].ToString(),
                            IdentityCard = row["identity_card"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải danh sách khách hàng: " + ex.Message);
            }
            return customerList;
        }

        // Tìm kiếm khách hàng theo email và/hoặc số điện thoại
        public List<ManageCustomerDTO> SearchCustomers(string email, string phone)
        {
            List<ManageCustomerDTO> customerList = new List<ManageCustomerDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, full_name, phone, email, address, identity_card FROM [Customer] WHERE 1=1";

                    if (!string.IsNullOrEmpty(email))
                    {
                        query += " AND email LIKE @email";
                    }
                    if (!string.IsNullOrEmpty(phone))
                    {
                        query += " AND phone LIKE @phone";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(email))
                        {
                            cmd.Parameters.AddWithValue("@email", "%" + email + "%");
                        }
                        if (!string.IsNullOrEmpty(phone))
                        {
                            cmd.Parameters.AddWithValue("@phone", "%" + phone + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            customerList.Add(new ManageCustomerDTO
                            {
                                Id = Convert.ToInt32(row["id"]),
                                FullName = row["full_name"].ToString(),
                                Phone = row["phone"].ToString(),
                                Email = row["email"].ToString(),
                                Address = row["address"].ToString(),
                                IdentityCard = row["identity_card"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm khách hàng: " + ex.Message);
            }
            return customerList;
        }

        // Xóa khách hàng
        public bool DeleteCustomer(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM [Customer] WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa khách hàng: " + ex.Message);
            }
        }

        public bool UpdateCustomer(ManageCustomerDTO customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE [Customer] SET full_name = @full_name, phone = @phone, email = @email, address = @address, identity_card = @identity_card WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", customer.Id);
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
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa khách hàng: " + ex.Message);
            }
        }

        // Xuất dữ liệu khách hàng ra Excel
        public void ExportToExcel(string filePath)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    // Lấy dữ liệu từ cơ sở dữ liệu
                    List<ManageCustomerDTO> customerList = GetCustomerList();
                    DataTable dt = new DataTable();

                    // Tạo các cột từ dữ liệu khách hàng
                    dt.Columns.Add("Id");
                    dt.Columns.Add("Full Name");
                    dt.Columns.Add("Phone");
                    dt.Columns.Add("Email");
                    dt.Columns.Add("Address");
                    dt.Columns.Add("Identity Card");

                    // Thêm dữ liệu vào DataTable
                    foreach (var customer in customerList)
                    {
                        dt.Rows.Add(customer.Id, customer.FullName, customer.Phone, customer.Email, customer.Address, customer.IdentityCard);
                    }

                    // Thêm DataTable vào file Excel
                    wb.Worksheets.Add(dt, "DanhSachKhachHang");
                    wb.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xuất Excel: " + ex.Message);
            }
        }
    }
}
