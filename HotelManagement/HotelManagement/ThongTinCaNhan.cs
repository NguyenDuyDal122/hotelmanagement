using System;
using System.Windows.Forms;
using HotelManagement.BLL;
using HotelManagement.DTO;

namespace HotelManagement
{
    public partial class ThongTinCaNhan : Form
    {
        private int userId;
        private ThongTinCaNhanBLL thongTinBLL = new ThongTinCaNhanBLL();

        public ThongTinCaNhan(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserInfo();
        }
        private void ThongTinCaNhan_Load(object sender, EventArgs e)
        {

        }

        private void LoadUserInfo()
        {
            try
            {
                ThongTinCaNhanDTO user = thongTinBLL.GetUserInfo(userId);
                if (user != null)
                {
                    txt_username.Text = user.Username;
                    txt_fullname.Text = user.FullName;
                    txt_phone.Text = user.Phone;
                    txt_email.Text = user.Email;
                    txt_role.Text = user.Role;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
