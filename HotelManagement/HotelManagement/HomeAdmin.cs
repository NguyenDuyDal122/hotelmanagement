using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class HomeAdmin : Form
    {
        private int userId; // Lưu ID của người đăng nhập
        public HomeAdmin(int userId)
        {
            InitializeComponent();
            this.userId = userId; // ✅ Gán giá trị userId từ Form1
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

        private void btn_thongtin_Click(object sender, EventArgs e)
        {
            ThongTinCaNhan thongTinForm = new ThongTinCaNhan(userId);
            thongTinForm.ShowDialog(); // Hiển thị form thông tin cá nhân
        }

        private void btn_doimatkhau_Click(object sender, EventArgs e)
        {
            DoiMatKhau doiMatKhauForm = new DoiMatKhau(userId); // Truyền userId vào form đổi mật khẩu
            doiMatKhauForm.ShowDialog(); // Mở form đổi mật khẩu
        }

        private void btn_qlnhanvien_Click(object sender, EventArgs e)
        {
            ManageStaff manageStaffForm = new ManageStaff();
            manageStaffForm.ShowDialog();
        }

        private void btn_qlkhachhang_Click(object sender, EventArgs e)
        {
            ManageCustomer manageCustomerForm = new ManageCustomer();
            manageCustomerForm.ShowDialog();
        }

        private void btn_qltang_Click(object sender, EventArgs e)
        {
            ManageFloor manageFloorForm = new ManageFloor();
            manageFloorForm.ShowDialog();
        }

        private void btn_qlphong_Click(object sender, EventArgs e)
        {
            ManageRoom manageRoomForm = new ManageRoom();
            manageRoomForm.ShowDialog();
        }
    }
}
