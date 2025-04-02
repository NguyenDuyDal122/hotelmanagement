using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Wordprocessing;
using HotelManagement.BLL;
using HotelManagement.DTO;

namespace HotelManagement
{
    public partial class Form1 : Form
    {
        private LoginBLL loginBLL;

        public Form1()
        {
            InitializeComponent();
            loginBLL = new LoginBLL();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoginDTO user = loginBLL.AuthenticateUser(username, password);

            if (user != null)
            {
                Form mainForm = null;

                if (user.Role == "admin")
                {
                    mainForm = new HomeAdmin(user.Id);
                }
                else if (user.Role == "staff")
                {
                    mainForm = new HomeStaff(user.Id);
                }

                if (mainForm != null)
                {
                    mainForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void pic_showPassword_Click(object sender, EventArgs e)
        {
            // Nếu đang ẩn thì hiện mật khẩu, ngược lại thì ẩn
            if (txt_password.UseSystemPasswordChar)
            {
                txt_password.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                txt_password.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
