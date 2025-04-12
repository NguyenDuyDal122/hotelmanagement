using ClosedXML.Excel;
using HotelManagement.BLL;
using HotelManagement.DTO;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class ManageService : Form
    {
        private ServiceBLL serviceBLL = new ServiceBLL();
        private int selectedServiceId = -1;

        public ManageService()
        {
            InitializeComponent();
            LoadServiceData();
            dataGridView_danhsachdichvu.CellClick += dataGridView_danhsachdichvu_CellClick;
        }

        private void LoadServiceData()
        {
            var list = serviceBLL.GetAllServices();
            dataGridView_danhsachdichvu.DataSource = list;
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            LoadServiceData();
            txt_tendichvu.Clear();
            txt_mota.Clear();
            txt_gia.Clear();
            selectedServiceId = -1;
        }

        private void btn_themtang_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txt_gia.Text, out decimal price) || price < 0 || string.IsNullOrEmpty(txt_tendichvu.Text))
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
                return;
            }

            var service = new ServiceDTO
            {
                ServiceName = txt_tendichvu.Text.Trim(),
                Description = txt_mota.Text.Trim(),
                Price = price
            };

            if (serviceBLL.AddService(service))
            {
                MessageBox.Show("Thêm thành công!");
                btn_lammoi_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Tên dịch vụ đã tồn tại!");
            }
        }

        private void dataGridView_danhsachdichvu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView_danhsachdichvu.Rows[e.RowIndex];
                selectedServiceId = Convert.ToInt32(row.Cells["Id"].Value);
                txt_tendichvu.Text = row.Cells["ServiceName"].Value.ToString();
                txt_mota.Text = row.Cells["Description"].Value.ToString();
                txt_gia.Text = row.Cells["Price"].Value.ToString();
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (selectedServiceId == -1)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để sửa.");
                return;
            }

            if (!decimal.TryParse(txt_gia.Text, out decimal price) || string.IsNullOrEmpty(txt_tendichvu.Text))
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
                return;
            }

            var service = new ServiceDTO
            {
                Id = selectedServiceId,
                ServiceName = txt_tendichvu.Text.Trim(),
                Description = txt_mota.Text.Trim(),
                Price = price
            };

            if (serviceBLL.UpdateService(service))
            {
                MessageBox.Show("Cập nhật thành công!");
                btn_lammoi_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Tên dịch vụ đã tồn tại!");
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (selectedServiceId == -1)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (serviceBLL.DeleteService(selectedServiceId))
                {
                    MessageBox.Show("Xóa thành công!");
                    btn_lammoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void btn_xuatexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_danhsachdichvu.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel Files|*.xlsx";
                    sfd.FileName = "DanhSachDichVu.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel(sfd.FileName);
                        MessageBox.Show("Xuất Excel thành công!");
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để xuất!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message);
            }
        }

        private void ExportToExcel(string path)
        {
            using (var wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn col in dataGridView_danhsachdichvu.Columns)
                {
                    dt.Columns.Add(col.HeaderText);
                }

                foreach (DataGridViewRow row in dataGridView_danhsachdichvu.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var dataRow = dt.NewRow();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dataRow[i] = row.Cells[i].Value?.ToString();
                        }
                        dt.Rows.Add(dataRow);
                    }
                }

                wb.Worksheets.Add(dt, "DanhSachDichVu");
                wb.SaveAs(path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
