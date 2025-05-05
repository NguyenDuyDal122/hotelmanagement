using HotelManagement.BLL;
using HotelManagement.DTO;
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
    public partial class TPhong : Form
    {
        private TraPhongBLL bookingBLL = new TraPhongBLL();
        public TPhong()
        {
            InitializeComponent();
            
        }

        private void TPhong_Load(object sender, EventArgs e)
        {
            LoadBookingData();
            comboBox_phuongthucthanhtoan.Items.Add("cash");
            comboBox_phuongthucthanhtoan.Items.Add("online");

            // Chọn mặc định phương thức thanh toán là Tiền mặt
            comboBox_phuongthucthanhtoan.SelectedIndex = 0;
        }

        private void LoadBookingData()
        {
            var bookings = bookingBLL.GetAllBookings();

            var displayData = bookings.Select(b => new {
                MãPhiếu = b.Id,
                TênKhách = b.CustomerName,
                SĐT = b.CustomerPhone,
                TênNhânViên = b.StaffName,
                MãPhòng = b.RoomNumber,
                LoạiPhòng = b.RoomType,
                NgàyNhận = b.CheckIn,
                DịchVụ = b.TotalPriceService,
                NgàyTạo = b.CreatedAt
            }).ToList();

            dgv_Phieuthue.DataSource = displayData;
        }

        private void btn_TimPhieu_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_Phieuthue_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_TinhTongTien_Click(object sender, EventArgs e)
        {
            try
            {
                int soNgayThue = (int)numberic_SoNgayThue.Value;
                int soGioThue = (int)numberic_SoGioThue.Value;

                // Kiểm tra nếu cả hai đều bằng 0
                if (soNgayThue == 0 && soGioThue == 0)
                {
                    MessageBox.Show("Vui lòng nhập ít nhất số ngày hoặc số giờ thuê!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy dữ liệu từ các textbox
                decimal giaTheoNgay = decimal.Parse(txt_giatheongay.Text);
                decimal giaTheoGio = decimal.Parse(txt_giatheogio.Text);
                decimal tienDichVu = decimal.Parse(txt_tongtiendichvu.Text);

                // Tính tiền thuê phòng
                decimal tienThuePhong = (giaTheoNgay * soNgayThue) + (giaTheoGio * soGioThue);
                txt_tienthuephong.Text = tienThuePhong.ToString("N0");

                // Tính tổng tiền
                decimal tongTien = tienThuePhong + tienDichVu;
                txt_tongtien.Text = tongTien.ToString("N0");
            }
            catch (FormatException)
            {
                MessageBox.Show("Giá trị nhập vào không hợp lệ. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_TraPhong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_tongtien.Text))
            {
                MessageBox.Show("Vui lòng tính tổng tiền trước khi trả phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgv_Phieuthue.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu thuê để trả phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string paymentMethod = comboBox_phuongthucthanhtoan.SelectedItem?.ToString().ToLower();

            if (string.IsNullOrEmpty(paymentMethod))
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string bookingId = dgv_Phieuthue.CurrentRow.Cells["MãPhiếu"].Value.ToString();
            DateTime checkOut = dtpNgayTra.Value;
            decimal totalAmount = decimal.Parse(txt_tongtien.Text.Replace(",", ""));

            if (paymentMethod == "online")
            {
                // 1. Tạo link thanh toán PayPal
                string paymentUrl = await PayPalPayment.CreatePayment(totalAmount);

                if (!string.IsNullOrEmpty(paymentUrl))
                {
                    // 2. Mở trình duyệt để người dùng thanh toán
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = paymentUrl,
                        UseShellExecute = true
                    });

                    // 3. Bắt đầu lắng nghe phản hồi từ PayPal
                    await Task.Run(() =>
                    {
                        PayPalListener.StartListeningAsync((token, payerId) =>
                        {
                            Invoke(new Action(() =>
                            {
                                bool success = bookingBLL.AddInvoice(bookingId, checkOut, totalAmount, paymentMethod);
                                if (success)
                                {
                                    MessageBox.Show("Thanh toán và trả phòng thành côn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadBookingData();
                                    txt_tienthuephong.Clear();
                                    numberic_SoNgayThue.Value = 0;
                                    numberic_SoGioThue.Value = 0;
                                    txt_tongtien.Clear();
                                    txt_giatheongay.Clear();
                                    txt_giatheogio.Clear();
                                    txt_tongtiendichvu.Clear();
                                    comboBox_phuongthucthanhtoan.SelectedIndex = 0;
                                }
                                else
                                {
                                    MessageBox.Show("Trả phòng thất bại sau thanh toán.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }));
                        });
                    });
                }
                else
                {
                    MessageBox.Show("Không thể tạo thanh toán PayPal.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return; // ❌ Không tiếp tục xử lý phần bên dưới
            }

            // Với phương thức thanh toán trực tiếp
            try
            {
                bool success = bookingBLL.AddInvoice(bookingId, checkOut, totalAmount, paymentMethod);
                if (success)
                {
                    MessageBox.Show("Trả phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBookingData();
                    txt_tienthuephong.Clear();
                    numberic_SoNgayThue.Value = 0;
                    numberic_SoGioThue.Value = 0;
                    txt_tongtien.Clear();
                    txt_giatheongay.Clear();
                    txt_giatheogio.Clear();
                    txt_tongtiendichvu.Clear();
                    comboBox_phuongthucthanhtoan.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Trả phòng thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình trả phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_timphieuthue_Click(object sender, EventArgs e)
        {
            string maPhieu = txt_maphieu.Text.Trim();

            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var booking = bookingBLL.GetBookingById(maPhieu);

            if (booking == null)
            {
                MessageBox.Show("Không tìm thấy phiếu thuê với mã đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hiển thị dữ liệu tìm được lên DataGridView
            var displayList = new List<object>
            {
        new {
            MãPhiếu = booking.Id,
            TênKhách = booking.CustomerName,
            SĐT = booking.CustomerPhone,
            TênNhânViên = booking.StaffName,
            MãPhòng = booking.RoomNumber,
            LoạiPhòng = booking.RoomType,
            NgàyNhận = booking.CheckIn,
            DịchVụ = booking.TotalPriceService,
            NgàyTạo = booking.CreatedAt
        }
            };  

            dgv_Phieuthue.DataSource = displayList;
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            LoadBookingData();
            txt_maphieu.Clear();
        }

        private void dgv_Phieuthue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Phieuthue.CurrentRow != null)
            {
                var selectedRow = dgv_Phieuthue.CurrentRow;
                string maPhieu = selectedRow.Cells["MãPhiếu"].Value.ToString();

                var booking = bookingBLL.GetBookingById(maPhieu);
                if (booking != null)
                {
                    txt_giatheongay.Text = booking.PriceByDay.ToString("N0");
                    txt_giatheogio.Text = booking.PriceByHour.ToString("N0");
                    txt_tongtiendichvu.Text = booking.TotalPriceService.ToString("N0");
                }
            }
        }
    }
}
