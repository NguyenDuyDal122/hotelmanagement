using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class SuaKhachHang : Form
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
        private int cusId;
        public SuaKhachHang(int id)
        {
            InitializeComponent();
            cusId = id;
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT full_name, phone, email, address, identity_card FROM [Customer] WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", cusId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txt_full_name.Text = reader["full_name"].ToString();
                            txt_phone.Text = reader["phone"].ToString();
                            txt_email.Text = reader["email"].ToString();
                            txt_diachi.Text = reader["address"].ToString();
                            txt_cccd.Text = reader["identity_card"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string full_name = txt_full_name.Text.Trim();
            string phone = txt_phone.Text.Trim();
            string email = txt_email.Text.Trim();
            string address = txt_diachi.Text.Trim();
            string identity_card = txt_cccd.Text.Trim();

            if (string.IsNullOrEmpty(full_name) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(identity_card))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra trùng username, phone, email nhưng bỏ qua user hiện tại
                    string checkQuery = "SELECT COUNT(*) FROM [Customer] WHERE (phone = @phone OR email = @email OR identity_card = @identity_card) AND id != @id";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@identity_card", identity_card);
                        checkCmd.Parameters.AddWithValue("@phone", phone);
                        checkCmd.Parameters.AddWithValue("@email", email);
                        checkCmd.Parameters.AddWithValue("@id", cusId);

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Căn cước công dân, số điện thoại hoặc email đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Cập nhật dữ liệu
                    string updateQuery = "UPDATE [Customer] SET full_name=@full_name, phone=@phone, email=@email, address=@address, identity_card=@identity_card WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@full_name", full_name);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@identity_card", identity_card);
                        cmd.Parameters.AddWithValue("@id", cusId);

                        int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi lệnh cập nhật

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK; // Trả về kết quả thành công
                            this.Close(); // Đóng form sau khi cập nhật
                        }
                        else
                        {
                            MessageBox.Show("Không có bản ghi nào được cập nhật. Kiểm tra lại thông tin khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
