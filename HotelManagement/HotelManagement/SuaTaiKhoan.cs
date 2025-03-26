using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class SuaTaiKhoan : Form
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
        private int userId; 
        public SuaTaiKhoan(int id)
        {
            InitializeComponent();
            userId = id;
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT username, full_name, phone, email, role FROM [User] WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", userId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txt_username.Text = reader["username"].ToString();
                            txt_fullname.Text = reader["full_name"].ToString();
                            txt_phone.Text = reader["phone"].ToString();
                            txt_email.Text = reader["email"].ToString();

                            comboBox_vaitro.Items.Add("admin");
                            comboBox_vaitro.Items.Add("staff");
                            comboBox_vaitro.SelectedItem = reader["role"].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string fullName = txt_fullname.Text.Trim();
            string phone = txt_phone.Text.Trim();
            string email = txt_email.Text.Trim();
            string role = comboBox_vaitro.SelectedItem.ToString();
            string password = txt_matkhau.Text.Trim();
            string confirmPassword = txt_nhaplai.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(password) && password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra trùng username, phone, email nhưng bỏ qua user hiện tại
                    string checkQuery = "SELECT COUNT(*) FROM [User] WHERE (username = @username OR phone = @phone OR email = @email) AND id != @id";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        checkCmd.Parameters.AddWithValue("@phone", phone);
                        checkCmd.Parameters.AddWithValue("@email", email);
                        checkCmd.Parameters.AddWithValue("@id", userId);

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Username, số điện thoại hoặc email đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Cập nhật dữ liệu
                    string updateQuery = "UPDATE [User] SET username=@username, full_name=@fullName, phone=@phone, email=@email, role=@role";

                    if (!string.IsNullOrEmpty(password))
                    {
                        updateQuery += ", password=@password";
                    }

                    updateQuery += " WHERE id=@id";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@fullName", fullName);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@id", userId);

                        if (!string.IsNullOrEmpty(password))
                        {
                            cmd.Parameters.AddWithValue("@password", HashPassword(password));
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox_nhaplai_Click(object sender, EventArgs e)
        {
            if (txt_nhaplai.UseSystemPasswordChar)
            {
                txt_nhaplai.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                txt_nhaplai.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }

        private void pictureBox_matkhau_Click(object sender, EventArgs e)
        {
            if (txt_matkhau.UseSystemPasswordChar)
            {
                txt_matkhau.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                txt_matkhau.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }
    }
}
