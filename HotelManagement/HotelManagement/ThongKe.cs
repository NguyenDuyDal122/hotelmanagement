using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO;
using BBL;
using System.Linq;

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

                    // Tính tổng doanh thu và hiển thị lên textbox
                    decimal tongDoanhThu = list.Sum(x => x.TotalAmount);
                    txt_tongdoanhthu.Text = tongDoanhThu.ToString("N0") + " VNĐ";
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
