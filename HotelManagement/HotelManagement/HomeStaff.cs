using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagement
{
    public partial class HomeStaff : Form
    {
        private int userId;
        public HomeStaff(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void HomeStaff_Load(object sender, EventArgs e)
        {

        }

      

        private void btn_doimatkhau_Click(object sender, EventArgs e)
        {
            DoiMatKhau doiMatKhauForm = new DoiMatKhau(userId); // Truyền userId vào form đổi mật khẩu
            doiMatKhauForm.ShowDialog(); // Mở form đổi mật khẩu
        }

        private void btn_ThuePhong_Click(object sender, EventArgs e)
        {
            ThuePhong thuePhongForm = new ThuePhong(userId); // Truyền userId (id của nhân viên) vào
            thuePhongForm.ShowDialog();
        }

        private void btn_TraPhong_Click(object sender, EventArgs e)
        {
            TPhong traPhongForm = new TPhong();
            traPhongForm.ShowDialog();
        }

        private void btn_dangxuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide(); // Ẩn form hiện tại
                Form1 loginForm = new Form1(); // Form1 là form đăng nhập
                loginForm.Show();
            }
        }

        private void btn_thongtin_Click_1(object sender, EventArgs e)
        {
            ThongTinCaNhan thongTinForm = new ThongTinCaNhan(userId);
            thongTinForm.ShowDialog(); // Hiển thị form thông tin cá nhân
        }

        private void btn_doimatkhau_Click_1(object sender, EventArgs e)
        {
            DoiMatKhau ttForm = new DoiMatKhau(userId);
            ttForm.ShowDialog(); // Hiển thị form thông tin cá nhân
        }
    }
}
