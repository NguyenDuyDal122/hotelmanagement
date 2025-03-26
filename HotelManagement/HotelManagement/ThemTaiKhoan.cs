using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HotelManagement
{
    public partial class ThemTaiKhoan : Form
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
        public ThemTaiKhoan()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string fullName = txt_fullname.Text.Trim();
            string phone = txt_phone.Text.Trim();
            string email = txt_email.Text.Trim();
            string password = txt_matkhau.Text;
            string confirmPassword = txt_nhaplai.Text;
            string role = comboBox_vaitro.SelectedItem.ToString();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra username, phone, email có trùng hay không
                    string checkQuery = "SELECT COUNT(*) FROM [User] WHERE username = @username OR phone = @phone OR email = @email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        checkCmd.Parameters.AddWithValue("@phone", phone);
                        checkCmd.Parameters.AddWithValue("@email", email);

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Username, số điện thoại hoặc email đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Mã hóa mật khẩu (ở đây đơn giản hóa, bạn có thể sử dụng hash bảo mật hơn)
                    string passwordHash = password;

                    // Chèn dữ liệu mới vào bảng [User]
                    string insertQuery = "INSERT INTO [User] (username, full_name, phone, email, role, password_hash) VALUES (@username, @fullName, @phone, @email, @role, @password)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@fullName", fullName);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@password", passwordHash);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Đóng form sau khi thêm thành công
                        }
                        else
                        {
                            MessageBox.Show("Thêm tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_vaitro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadRoles()
        {
            comboBox_vaitro.Items.Clear(); // Xóa các mục cũ (nếu có)
            comboBox_vaitro.Items.Add("admin");
            comboBox_vaitro.Items.Add("staff");

            // Đặt giá trị mặc định là "Staff"
            comboBox_vaitro.SelectedIndex = 0;
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
    }
}
