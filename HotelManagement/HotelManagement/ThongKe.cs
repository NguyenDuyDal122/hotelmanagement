using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO;
using BBL;
using System.Linq;
using System.Drawing;

namespace HotelManagement
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
            this.Load += ThongKe_Load; // Gán sự kiện load form
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            LoadComboBoxThang();
            dataGridViewThongKe.DataSource = null;
        }
        private void LoadComboBoxThang()
        {
            comboBox_chonthang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                comboBox_chonthang.Items.Add("Tháng " + i);
            }
            comboBox_chonthang.SelectedIndex = 0;
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_xemdoanhthu_Click(object sender, EventArgs e)
        {
            if (comboBox_chonthang.SelectedIndex >= 0)
            {
                int selectedMonth = comboBox_chonthang.SelectedIndex + 1;

                ThongKeBLL bll = new ThongKeBLL();
                List<ThongKeDTO> list = bll.GetInvoicesByMonth(selectedMonth);
                dataGridViewThongKe.DataSource = list;

                if (list.Count > 0)
                {
                    dataGridViewThongKe.Columns["InvoiceId"].HeaderText = "Mã hóa đơn";
                    dataGridViewThongKe.Columns["StaffId"].HeaderText = "Mã nhân viên";
                    dataGridViewThongKe.Columns["CustomerName"].HeaderText = "Tên khách hàng";
                    dataGridViewThongKe.Columns["RoomNumber"].HeaderText = "Số phòng";
                    dataGridViewThongKe.Columns["PaymentDate"].HeaderText = "Ngày thanh toán";
                    dataGridViewThongKe.Columns["TotalAmount"].HeaderText = "Tổng tiền";
                    dataGridViewThongKe.Columns["PaymentMethod"].HeaderText = "Phương thức";

                    dataGridViewThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Tính tổng doanh thu
                    decimal tongDoanhThu = list.Sum(x => x.TotalAmount);
                    txt_tongdoanhthu.Text = tongDoanhThu.ToString("N0") + " VNĐ";

                    // Đếm tổng số lượt khách hàng (kể cả trùng tên)
                    int soLuotKhach = list.Count;
                    txt_sokhachhang.Text = soLuotKhach.ToString();

                    // Vẽ biểu đồ doanh thu gộp theo ngày
                    chartDoanhThu.Series.Clear();
                    chartDoanhThu.ChartAreas.Clear();
                    chartDoanhThu.Titles.Clear();
                    chartDoanhThu.ChartAreas.Add("ChartArea1");

                    var series = chartDoanhThu.Series.Add("Doanh thu theo ngày");
                    series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    series.IsValueShownAsLabel = true;
                    series.Color = Color.SkyBlue;

                    // Gộp theo ngày thanh toán và tính tổng tiền từng ngày
                    var doanhThuTheoNgay = list
                        .GroupBy(x => x.PaymentDate.Date)
                        .Select(g => new
                        {
                            Ngay = g.Key,
                            TongTien = g.Sum(x => x.TotalAmount)
                        })
                        .OrderBy(x => x.Ngay)
                        .ToList();

                    foreach (var item in doanhThuTheoNgay)
                    {
                        string ngayStr = item.Ngay.ToString("dd/MM/yyyy");
                        series.Points.AddXY(ngayStr, item.TongTien);
                    }

                    chartDoanhThu.ChartAreas[0].AxisX.Title = "Ngày thanh toán";
                    chartDoanhThu.ChartAreas[0].AxisY.Title = "Tổng tiền (VNĐ)";
                    chartDoanhThu.Titles.Add("Biểu đồ doanh thu trong tháng " + selectedMonth);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu trong tháng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridViewThongKe.DataSource = null;
                    txt_tongdoanhthu.Text = "0 VNĐ";
                }
            }
        }
    }
}
