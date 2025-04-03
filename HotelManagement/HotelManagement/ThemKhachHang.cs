using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class ThemKhachHang : Form
    {
        private ThemKhachHangBLL customerBLL = new ThemKhachHangBLL();

        public ThemKhachHang()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string full_name = txt_fullname.Text.Trim();
            string phone = txt_phone.Text.Trim();
            string email = txt_email.Text.Trim();
            string address = txt_diachi.Text;
            string identity_card = txt_cccd.Text;

            ThemKhachHangDTO newCustomer = new ThemKhachHangDTO()
            {
                FullName = full_name,
                Phone = phone,
                Email = email,
                Address = address,
                IdentityCard = identity_card
            };

            try
            {
                // Gọi BLL để kiểm tra và thêm khách hàng
                bool success = customerBLL.ValidateAndAddCustomer(newCustomer);
                if (success)
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form sau khi thêm thành công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

