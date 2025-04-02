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
using HotelManagement.BLL;

namespace HotelManagement
{
    public partial class DoiMatKhau : Form
    {
        private int userId;
        private DoiMatKhauBLL userBLL;

        public DoiMatKhau(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            userBLL = new DoiMatKhauBLL();
        }

        private void pic_1_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txt_matkhaucu);
        }

        private void pic_2_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txt_matkhaumoi);
        }

        private void pic_3_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txt_nhaplai);
        }

        private void TogglePasswordVisibility(TextBox txtBox)
        {
            if (txtBox.UseSystemPasswordChar)
            {
                txtBox.UseSystemPasswordChar = false;
            }
            else
            {
                txtBox.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string oldPassword = txt_matkhaucu.Text.Trim();
            string newPassword = txt_matkhaumoi.Text.Trim();
            string confirmPassword = txt_nhaplai.Text.Trim();

            // 1. Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Gọi BLL để xử lý logic
            bool success = userBLL.ChangePassword(userId, oldPassword, newPassword, confirmPassword);

            if (success)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi đổi mật khẩu thành công
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại! Kiểm tra lại mật khẩu cũ hoặc mật khẩu mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
