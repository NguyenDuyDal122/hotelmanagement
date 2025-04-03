using ClosedXML.Excel;
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
    public partial class ManageCustomer : Form
    {
        private ManageCustomerBLL customerBLL = new ManageCustomerBLL();

        public ManageCustomer()
        {
            InitializeComponent();
            LoadCustomerList();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ThemKhachHang formThemKhachHang = new ThemKhachHang();
            formThemKhachHang.ShowDialog();
        }
        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            txt_email.Text = "";
            txt_phone.Text = "";

            LoadCustomerList();
        }

        private void LoadCustomerList()
        {
            try
            {
                var customerList = customerBLL.GetCustomerList();
                dataGridView_danhsachkhachhang.DataSource = customerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string email = txt_email.Text.Trim();
            string phone = txt_phone.Text.Trim();

            try
            {
                var customerList = customerBLL.SearchCustomers(email, phone);
                if (customerList.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dataGridView_danhsachkhachhang.DataSource = customerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachkhachhang.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView_danhsachkhachhang.SelectedRows[0].Cells["id"].Value);

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool success = customerBLL.DeleteCustomer(id);
                        if (success)
                        {
                            MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCustomerList(); // Tải lại danh sách sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachkhachhang.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView_danhsachkhachhang.SelectedRows[0].Cells["id"].Value);

                // Lấy thông tin khách hàng đã chọn
                ManageCustomerDTO selectedCustomer = new ManageCustomerDTO
                {
                    Id = id,
                    FullName = dataGridView_danhsachkhachhang.SelectedRows[0].Cells["FullName"].Value.ToString(),
                    Phone = dataGridView_danhsachkhachhang.SelectedRows[0].Cells["Phone"].Value.ToString(),
                    Email = dataGridView_danhsachkhachhang.SelectedRows[0].Cells["Email"].Value.ToString(),
                    Address = dataGridView_danhsachkhachhang.SelectedRows[0].Cells["Address"].Value.ToString(),
                    IdentityCard = dataGridView_danhsachkhachhang.SelectedRows[0].Cells["IdentityCard"].Value.ToString()
                };

                // Mở form sửa và truyền khách hàng cần sửa vào
                SuaKhachHang formSuaKhachHang = new SuaKhachHang(id);
                if (formSuaKhachHang.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomerList(); // Tải lại danh sách sau khi sửa
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_xuatexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_danhsachkhachhang.Rows.Count > 0)
                {
                    // Tạo SaveFileDialog để chọn nơi lưu file
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Chọn nơi lưu file Excel";
                    saveFileDialog.FileName = "DanhSachKhachHang.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        customerBLL.ExportToExcel(filePath); // Gọi phương thức xuất Excel
                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

