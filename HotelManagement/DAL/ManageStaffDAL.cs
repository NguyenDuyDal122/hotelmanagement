using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;

namespace HotelManagement
{
    public class ManageStaffDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public List<ManageStaffDTO> GetAllStaff()
        {
            List<ManageStaffDTO> list = new List<ManageStaffDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, username, full_name, phone, email, role FROM [User]";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new ManageStaffDTO
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Username = reader["username"].ToString(),
                        FullName = reader["full_name"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Email = reader["email"].ToString(),
                        Role = reader["role"].ToString()
                    });
                }
            }

            return list;
        }

        public List<ManageStaffDTO> SearchStaff(string username, string phone)
        {
            List<ManageStaffDTO> list = new List<ManageStaffDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, username, full_name, phone, email, role FROM [User] WHERE 1=1";

                if (!string.IsNullOrEmpty(username))
                    query += " AND username LIKE @username";

                if (!string.IsNullOrEmpty(phone))
                    query += " AND phone LIKE @phone";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(username))
                        cmd.Parameters.AddWithValue("@username", "%" + username + "%");

                    if (!string.IsNullOrEmpty(phone))
                        cmd.Parameters.AddWithValue("@phone", "%" + phone + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new ManageStaffDTO
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString(),
                            FullName = reader["full_name"].ToString(),
                            Phone = reader["phone"].ToString(),
                            Email = reader["email"].ToString(),
                            Role = reader["role"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public bool UpdateStaff(ManageStaffDTO staff)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE [User] SET username=@username, full_name=@full_name, phone=@phone, email=@email, role=@role WHERE id=@id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", staff.Id);
                    cmd.Parameters.AddWithValue("@username", staff.Username);
                    cmd.Parameters.AddWithValue("@full_name", staff.FullName);
                    cmd.Parameters.AddWithValue("@phone", staff.Phone);
                    cmd.Parameters.AddWithValue("@email", staff.Email);
                    cmd.Parameters.AddWithValue("@role", staff.Role);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public void ExportStaffListToExcel(string filePath)
        {
            List<ManageStaffDTO> staffList = GetAllStaff();
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Username");
            dt.Columns.Add("Full Name");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Email");
            dt.Columns.Add("Role");

            foreach (var staff in staffList)
            {
                dt.Rows.Add(staff.Id, staff.Username, staff.FullName, staff.Phone, staff.Email, staff.Role);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DanhSachNhanVien");
                wb.SaveAs(filePath);
            }
        }

        public bool DeleteStaff(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM [User] WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
