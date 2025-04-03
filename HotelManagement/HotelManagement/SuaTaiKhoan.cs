using System;
using System.Windows.Forms;
using HotelManagement.BLL;
using HotelManagement.DTO;

namespace HotelManagement
{
    public partial class SuaTaiKhoan : Form
    {
        private SuaTaiKhoanBLL bll = new SuaTaiKhoanBLL();
        private int userId;

        public SuaTaiKhoan(int id)
        {
            InitializeComponent();
            userId = id;
            LoadUserData();
        }

        private void LoadUserData()
        {
            SuaTaiKhoanDTO user = bll.GetUser(userId);
            if (user != null)
            {
                txt_username.Text = user.Username;
                txt_fullname.Text = user.FullName;
                txt_phone.Text = user.Phone;
                txt_email.Text = user.Email;
                comboBox_vaitro.Items.Add("admin");
                comboBox_vaitro.Items.Add("staff");
                comboBox_vaitro.SelectedItem = user.Role;
            }
            else
            {
                MessageBox.Show("Không tìm thấy người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            SuaTaiKhoanDTO user = new SuaTaiKhoanDTO
            {
                Id = userId,
                Username = username,
                FullName = fullName,
                Phone = phone,
                Email = email,
                Role = role,
                Password = password
            };

            string result = bll.UpdateUser(user, confirmPassword);
            MessageBox.Show(result, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result == "Cập nhật tài khoản thành công!")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox_nhaplai_Click(object sender, EventArgs e)
        {
            txt_nhaplai.UseSystemPasswordChar = !txt_nhaplai.UseSystemPasswordChar;
        }

        private void pictureBox_matkhau_Click(object sender, EventArgs e)
        {
            txt_matkhau.UseSystemPasswordChar = !txt_matkhau.UseSystemPasswordChar;
        }
    }
}
