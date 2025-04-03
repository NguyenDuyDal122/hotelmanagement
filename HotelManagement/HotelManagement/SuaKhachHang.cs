using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class SuaKhachHang : Form
    {
        private SuaKhachHangBLL customerBLL = new SuaKhachHangBLL();
        private int cusId;

        public SuaKhachHang(int selectedCustomer)
        {
            InitializeComponent();
            cusId = selectedCustomer;
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                var customer = customerBLL.GetCustomerById(cusId);
                if (customer != null)
                {
                    txt_full_name.Text = customer.FullName;
                    txt_phone.Text = customer.Phone;
                    txt_email.Text = customer.Email;
                    txt_diachi.Text = customer.Address;
                    txt_cccd.Text = customer.IdentityCard;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string full_name = txt_full_name.Text.Trim();
            string phone = txt_phone.Text.Trim();
            string email = txt_email.Text.Trim();
            string address = txt_diachi.Text.Trim();
            string identity_card = txt_cccd.Text.Trim();

            ManageCustomerDTO customer = new ManageCustomerDTO
            {
                Id = cusId,
                FullName = full_name,
                Phone = phone,
                Email = email,
                Address = address,
                IdentityCard = identity_card
            };

            try
            {
                bool success = customerBLL.ValidateAndUpdateCustomer(customer);
                if (success)
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close(); // Đóng form sau khi cập nhật
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

