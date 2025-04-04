using ClosedXML.Excel;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class ManageStaff : Form
    {
        private ManageStaffBLL staffBLL = new ManageStaffBLL();

        public ManageStaff()
        {
            InitializeComponent();
            LoadStaffList();
        }

        private void LoadStaffList()
        {
            dataGridView_danhsachnhanvien.DataSource = staffBLL.GetAllStaff();
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string phone = txt_phone.Text.Trim();

            var result = staffBLL.SearchStaff(username, phone); 
            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dataGridView_danhsachnhanvien.DataSource = result;
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            txt_username.Clear();
            txt_phone.Clear();
            LoadStaffList();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachnhanvien.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView_danhsachnhanvien.SelectedRows[0].Cells["Id"].Value);

                SuaTaiKhoan suaForm = new SuaTaiKhoan(id);
                if (suaForm.ShowDialog() == DialogResult.OK)
                {
                    LoadStaffList();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachnhanvien.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView_danhsachnhanvien.SelectedRows[0].Cells["Id"].Value);
                string role = dataGridView_danhsachnhanvien.SelectedRows[0].Cells["Role"].Value.ToString();

                if (role.ToLower() == "admin")
                {
                    MessageBox.Show("Không thể xóa tài khoản Admin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (staffBLL.DeleteStaff(id))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo");
                        LoadStaffList();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Lỗi");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_xuatexcel_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachnhanvien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo");
                return;
            }

            SaveFileDialog save = new SaveFileDialog
            {
                Filter = "Excel file (*.xlsx)|*.xlsx",
                FileName = "DanhSachNhanVien.xlsx"
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = staffBLL.GetAllStaffsDataTable();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "DanhSachNhanVien");
                    wb.SaveAs(save.FileName);
                }

                MessageBox.Show("Xuất file Excel thành công!", "Thông báo");
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            ThemTaiKhoan formThemTaiKhoan = new ThemTaiKhoan();
            formThemTaiKhoan.ShowDialog();
        }
        private void dataGridView_danhsachnhanvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
