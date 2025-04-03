using System;
using System.Windows.Forms;
using HotelManagement.BLL;
using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement
{
    public partial class ThemTaiKhoan : Form
    {
        private ThemTaiKhoanBLL bll = new ThemTaiKhoanBLL();

        public ThemTaiKhoan()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            ThemTaiKhoanDTO taiKhoan = new ThemTaiKhoanDTO
            {
                Username = txt_username.Text.Trim(),
                FullName = txt_fullname.Text.Trim(),
                Phone = txt_phone.Text.Trim(),
                Email = txt_email.Text.Trim(),
                Password = txt_matkhau.Text,
                ConfirmPassword = txt_nhaplai.Text,
                Role = comboBox_vaitro.SelectedItem?.ToString() ?? "staff" // Xử lý null nếu không có giá trị trong ComboBox
            };

            string errorMessage = bll.AddUser(taiKhoan); // Gọi AddUser từ BLL để thêm tài khoản

            if (errorMessage == "Thêm tài khoản thành công!")
            {
                MessageBox.Show(errorMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form nếu thêm thành công
            }
            else
            {
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hiển thị thông báo lỗi
            }
        }

        private void LoadRoles()
        {
            comboBox_vaitro.Items.Add("admin");
            comboBox_vaitro.Items.Add("staff");
            comboBox_vaitro.SelectedIndex = 0;
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void comboBox_vaitro_SelectedIndexChanged(object sender, EventArgs e)
        {

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