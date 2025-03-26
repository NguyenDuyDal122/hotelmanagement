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
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
        public ManageCustomer()
        {
            InitializeComponent();
            LoadCustomerList();
        }
        private void LoadCustomerList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, full_name, phone, email, address, identity_card FROM [Customer]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView_danhsachkhachhang.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string email = txt_email.Text.Trim();
            string phone = txt_phone.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Xây dựng truy vấn SQL động
                    string query = "SELECT id, full_name, phone, email, address, identity_card FROM [Customer] WHERE 1=1";

                    if (!string.IsNullOrEmpty(email))
                    {
                        query += " AND email LIKE @email";
                    }
                    if (!string.IsNullOrEmpty(phone))
                    {
                        query += " AND phone LIKE @phone";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(email))
                        {
                            cmd.Parameters.AddWithValue("@email", "%" + email + "%");
                        }
                        if (!string.IsNullOrEmpty(phone))
                        {
                            cmd.Parameters.AddWithValue("@phone", "%" + phone + "%");
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Kiểm tra nếu không có dữ liệu
                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy khách hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // Hiển thị kết quả lên DataGridView
                        dataGridView_danhsachkhachhang.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            txt_email.Text = "";
            txt_phone.Text = "";

            LoadCustomerList();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachkhachhang.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView_danhsachkhachhang.SelectedRows[0].Cells["id"].Value);

                // Mở form sửa tài khoản và truyền id sang
                SuaKhachHang formSuaKhachHang = new SuaKhachHang(id);
                if (formSuaKhachHang.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomerList();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "DELETE FROM [Customer] WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadCustomerList(); // Tải lại danh sách sau khi xóa
                                }
                                else
                                {
                                    MessageBox.Show("Không thể xóa khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
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
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        ExportToExcel(filePath);
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
        private void ExportToExcel(string filePath)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();

                // Tạo các cột từ DataGridView
                foreach (DataGridViewColumn col in dataGridView_danhsachkhachhang.Columns)
                {
                    dt.Columns.Add(col.HeaderText);
                }

                // Thêm dữ liệu từ DataGridView vào DataTable
                foreach (DataGridViewRow row in dataGridView_danhsachkhachhang.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < dataGridView_danhsachkhachhang.Columns.Count; j++)
                        {
                            dr[j] = row.Cells[j].Value?.ToString() ?? "";
                        }
                        dt.Rows.Add(dr);
                    }
                }

                // Thêm DataTable vào file Excel
                wb.Worksheets.Add(dt, "DanhSachKhachHang");
                wb.SaveAs(filePath);
            }
        }
    }
}
