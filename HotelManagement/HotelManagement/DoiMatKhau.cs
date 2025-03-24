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
    public partial class DoiMatKhau : Form
    {
        private int userId; // Lưu ID người dùng

        public DoiMatKhau(int userId) // Nhận userId từ form trước
        {
            InitializeComponent();
            this.userId = userId; // Gán giá trị userId
        }

        private void pic_1_Click(object sender, EventArgs e)
        {
            // Nếu đang ẩn thì hiện mật khẩu, ngược lại thì ẩn
            if (txt_matkhaucu.UseSystemPasswordChar)
            {
                txt_matkhaucu.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                txt_matkhaucu.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }

        private void pic_2_Click(object sender, EventArgs e)
        {
            // Nếu đang ẩn thì hiện mật khẩu, ngược lại thì ẩn
            if (txt_matkhaumoi.UseSystemPasswordChar)
            {
                txt_matkhaumoi.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                txt_matkhaumoi.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }

        private void pic_3_Click(object sender, EventArgs e)
        {
            // Nếu đang ẩn thì hiện mật khẩu, ngược lại thì ẩn
            if (txt_nhaplai.UseSystemPasswordChar)
            {
                txt_nhaplai.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                txt_nhaplai.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string matKhauCu = txt_matkhaucu.Text.Trim();
            string matKhauMoi = txt_matkhaumoi.Text.Trim();
            string nhapLaiMatKhau = txt_nhaplai.Text.Trim();

            // 1. Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(nhapLaiMatKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauMoi != nhapLaiMatKhau)
            {
                MessageBox.Show("Mật khẩu mới không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra mật khẩu cũ có đúng không
            string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False"; // Cập nhật chuỗi kết nối
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryCheck = "SELECT COUNT(*) FROM [User] WHERE id = @userId AND password_hash = @password";
                using (SqlCommand cmd = new SqlCommand(queryCheck, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@password", matKhauCu); // Cần mã hóa nếu mật khẩu đã mã hóa

                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 3. Cập nhật mật khẩu mới
                string queryUpdate = "UPDATE [User] SET password_hash = @newPassword WHERE id = @userId";
                using (SqlCommand cmd = new SqlCommand(queryUpdate, conn))
                {
                    cmd.Parameters.AddWithValue("@newPassword", matKhauMoi); // Cần mã hóa nếu cần
                    cmd.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Đóng form sau khi đổi mật khẩu thành công
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
