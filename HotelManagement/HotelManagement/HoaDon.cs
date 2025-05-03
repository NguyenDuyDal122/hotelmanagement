using HotelManagement.BLL;
using HotelManagement.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;

namespace HotelManagement
{
    public partial class HoaDon : Form
    {
        private HoaDonBLL bll = new HoaDonBLL();

        public HoaDon()
        {
            InitializeComponent();
            LoadDataGridView();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            try
            {
                List<HoaDonDTO> danhSachHoaDon = bll.GetAllHoaDon();

                comboBox_trangthai.Items.Clear(); // Xóa các mục cũ (nếu có)
                comboBox_trangthai.Items.Add("Đã thanh toán");
                comboBox_trangthai.SelectedIndex = 0;

                if (danhSachHoaDon == null || danhSachHoaDon.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu hóa đơn để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView_danhsachhoadon.DataSource = null;
                    return;
                }

                dataGridView_danhsachhoadon.DataSource = danhSachHoaDon;
                dataGridView_danhsachhoadon.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
                SetupDataGridViewStyle();

                // Tùy chỉnh tiêu đề các cột
                if (dataGridView_danhsachhoadon.Columns.Count > 0)
                {
                    dataGridView_danhsachhoadon.Columns["InvoiceID"].HeaderText = "Mã hóa đơn";
                    dataGridView_danhsachhoadon.Columns["BookingID"].HeaderText = "Mã phiếu";
                    dataGridView_danhsachhoadon.Columns["CustomerName"].HeaderText = "Khách hàng";
                    dataGridView_danhsachhoadon.Columns["StaffName"].HeaderText = "Nhân viên";
                    dataGridView_danhsachhoadon.Columns["RoomNumber"].HeaderText = "Số phòng";
                    dataGridView_danhsachhoadon.Columns["CheckInDate"].HeaderText = "Ngày vào";
                    dataGridView_danhsachhoadon.Columns["CheckOutDate"].HeaderText = "Ngày ra";
                    dataGridView_danhsachhoadon.Columns["TotalService"].HeaderText = "Tiền dịch vụ";
                    dataGridView_danhsachhoadon.Columns["TotalAmount"].HeaderText = "Tổng tiền";
                    dataGridView_danhsachhoadon.Columns["PaymentMethod"].HeaderText = "Phương thức thanh toán";
                    dataGridView_danhsachhoadon.Columns["CreatedAt"].HeaderText = "Ngày tạo hóa đơn";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải danh sách hóa đơn:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView_danhsachhoadon.DataSource = null;
            }
        }

        private void SetupDataGridViewStyle()
        {
            dataGridView_danhsachhoadon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView_danhsachhoadon.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_danhsachhoadon.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView_danhsachhoadon.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView_danhsachhoadon.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView_danhsachhoadon.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView_danhsachhoadon.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView_danhsachhoadon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }


        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_timkiemhoadon_Click(object sender, EventArgs e)
        {
            string bookingID = txt_timkiemhoadon.Text.Trim();

            if (string.IsNullOrEmpty(bookingID))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var ketQua = bll.SearchHoaDonByBookingID(bookingID);

                if (ketQua == null || ketQua.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn phù hợp.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView_danhsachhoadon.DataSource = null;
                    return;
                }

                dataGridView_danhsachhoadon.DataSource = ketQua;
                dataGridView_danhsachhoadon.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
                SetupDataGridViewStyle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm hóa đơn:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachhoadon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                DataGridViewRow selectedRow = dataGridView_danhsachhoadon.SelectedRows[0];
                int invoiceID = Convert.ToInt32(selectedRow.Cells["InvoiceID"].Value);

                bool isDeleted = bll.DeleteHoaDon(invoiceID);
                if (isDeleted)
                {
                    MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView(); // Refresh lại danh sách
                }
                else
                {
                    MessageBox.Show("Không thể xóa hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hóa đơn:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xuatexcel_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachhoadon.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Lưu file Excel",
                FileName = "DanhSachHoaDon.xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("HoaDon");

                            // Header
                            for (int i = 0; i < dataGridView_danhsachhoadon.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dataGridView_danhsachhoadon.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                            }

                            // Data
                            for (int i = 0; i < dataGridView_danhsachhoadon.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridView_danhsachhoadon.Columns.Count; j++)
                                {
                                    var value = dataGridView_danhsachhoadon.Rows[i].Cells[j].Value;
                                    worksheet.Cell(i + 2, j + 1).Value = value?.ToString();
                                }
                            }

                            worksheet.Columns().AdjustToContents();

                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất Excel:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}