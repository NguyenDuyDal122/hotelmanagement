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
    public partial class Form1 : Form
    {
        string connectstring = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
        SqlConnection connect;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connect = new SqlConnection(connectstring);
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

            try
            {
                using (SqlConnection connect = new SqlConnection(connectstring))
                {
                    connect.Open();
                    string query = "SELECT role FROM [User] WHERE username = @username AND password_hash = @password";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        object result = cmd.ExecuteScalar(); // Lấy vai trò user

                        if (result != null)
                        {
                            string role = result.ToString();

                            // Lấy ID của user
                            string getIdQuery = "SELECT id FROM [User] WHERE username = @username";
                            using (SqlCommand getIdCmd = new SqlCommand(getIdQuery, connect))
                            {
                                getIdCmd.Parameters.AddWithValue("@username", username);
                                int userId = (int)getIdCmd.ExecuteScalar(); // Lấy userId từ database

                                Form mainForm = null;

                                if (role == "admin")
                                {
                                    mainForm = new HomeAdmin(userId); // ✅ Truyền userId
                                }
                                else if (role == "staff")
                                {
                                    mainForm = new HomeStaff(userId); // ✅ Truyền userId
                                }

                                if (mainForm != null)
                                {
                                    mainForm.Show();
                                    this.Hide();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
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
    }
}
