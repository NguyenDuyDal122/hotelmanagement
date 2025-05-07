using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.DTO;
using HotelManagement.BLL;
using System.Globalization;
using HotelManagement.DAL;

namespace HotelManagement
{
    public partial class ThuePhong : Form
    {
        private int staffId;
        private Dictionary<string, decimal> servicePriceMap = new Dictionary<string, decimal>();
        private Dictionary<string, Service1DTO> serviceMap = new Dictionary<string, Service1DTO>();
        private Customer1BLL customerBLL = new Customer1BLL();
        private Service1BLL serviceBLL = new Service1BLL();
        private Floor1BLL floorBLL = new Floor1BLL();
        private Room1BLL roomBLL = new Room1BLL();
        private Booking1BLL bookingBLL = new Booking1BLL();
        private Booking1DAL bookingDAL = new Booking1DAL();

        public ThuePhong(int staffId)
        {
            InitializeComponent();
            this.staffId = staffId;
            dgv.CellClick += dgv_CellClick;
        }

        private void ThuePhong_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
            txt_maNV.Text = staffId.ToString();
            LoadServiceData();
            LoadFloorDescriptions();
            LoadBookingData();
        }

        private void LoadCustomerData()
        {
            List<Customer1DTO> customers = customerBLL.GetAllCustomers();
            dgv.DataSource = customers;
        }

        private void btn_TimKH_Click(object sender, EventArgs e)
        {
            string phoneNumber = txt_timSDT.Text.Trim();
            if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại để tìm kiếm!");
                return;
            }
            List<Customer1DTO> customers = customerBLL.FindCustomersByPhone(phoneNumber);
            if (customers.Count > 0)
            {
                dgv.DataSource = customers;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng với số điện thoại này.");
                dgv.DataSource = null;
            }
        }

        private void btn_Lammoi_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
            txt_timSDT.Clear();
        }

        private void btn_Themmoi_Click(object sender, EventArgs e)
        {
            ThemKhachHang formThemKhachHang = new ThemKhachHang(); // Tạo instance form
            formThemKhachHang.Show();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv.Rows[e.RowIndex];
                if (row.Cells["id"].Value != null)
                {
                    txt_maKH.Text = row.Cells["id"].Value.ToString();
                }
            }
        }

        private void cbTenDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedService = cbTenDichVu.Text;
            if (!string.IsNullOrEmpty(selectedService) && serviceMap.ContainsKey(selectedService))
            {
                numberric_DonGia.Value = serviceMap[selectedService].Price;
            }
        }

        private void LoadServiceData()
        {
            List<Service1DTO> allServices = serviceBLL.GetAllServices();

            cbTenDichVu.Items.Clear();
            cbTenDichVu.DisplayMember = "ServiceName";  // Hiển thị tên trong ComboBox
            cbTenDichVu.ValueMember = "Id";             // Lưu giá trị là Id
            cbTenDichVu.DataSource = allServices;

            // Tạo map để tra cứu giá, id
            serviceMap = allServices.ToDictionary(s => s.ServiceName, s => s);
        }

        private void btn_themDV_Click(object sender, EventArgs e)
        {
            if (cbTenDichVu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ!");
                return;
            }

            Service1DTO selectedServiceDto = cbTenDichVu.SelectedItem as Service1DTO;
            if (selectedServiceDto == null)
            {
                MessageBox.Show("Dịch vụ không hợp lệ!");
                return;
            }

            int soLuong = (int)numberic_SoLuong.Value;
            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng dịch vụ phải lớn hơn 0!");
                return;
            }

            string selectedServiceName = selectedServiceDto.ServiceName;
            decimal donGia = selectedServiceDto.Price; // Hoặc: numberric_DonGia.Value;
            decimal thanhTien = donGia * soLuong;

            // Tạo cột nếu chưa có
            if (dgv_DichVu.Columns.Count == 0)
            {
                dgv_DichVu.Columns.Add("ServiceName", "Tên dịch vụ");
                dgv_DichVu.Columns.Add("UnitPrice", "Đơn giá");
                dgv_DichVu.Columns.Add("Quantity", "Số lượng");
                dgv_DichVu.Columns.Add("TotalPrice", "Thành tiền");
            }

            dgv_DichVu.Rows.Add(selectedServiceName, donGia, soLuong, thanhTien);
            HienThiTongLenPhieu();
        }

        private void HienThiTongLenPhieu()
        {
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dgv_DichVu.Rows)
            {
                if (row.Cells["TotalPrice"].Value != null && row.Cells["TotalPrice"].Value != DBNull.Value)
                {
                    tongTien += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                }
            }
            txt_tongtiendichvu.Text = tongTien.ToString("0.00");
        }

        private void btn_xoaDV_Click(object sender, EventArgs e)
        {
            if (dgv_DichVu.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_DichVu.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dgv_DichVu.Rows.Remove(row);
                    }
                }
                HienThiTongLenPhieu();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa!");
            }
        }

        private void btn_TaoPhieu_Click(object sender, EventArgs e)
        {
            txt_TaoMaPhieu.Text = bookingBLL.GenerateRentalId();
        }

        private void LoadFloorDescriptions()
        {
            combo_Tang.Items.Clear();
            List<Floor1DTO> floors = floorBLL.GetAllFloors();
            foreach (Floor1DTO floor in floors)
            {
                combo_Tang.Items.Add(floor.Description);
            }
        }

        private void LoadRoomsByFloor(string floorDescription)
        {
            panel_DanhsachPhong.Controls.Clear();
            List<Room1DTO> rooms = roomBLL.GetRoomsByFloorDescription(floorDescription);
            int x = 10, y = 10;
            int buttonWidth = 99;
            int buttonHeight = 80;
            int spacing = 10;
            int count = 0;

            foreach (Room1DTO room in rooms)
            {
                Button roomButton = new Button();
                roomButton.Width = buttonWidth;
                roomButton.Height = buttonHeight;
                roomButton.Text = room.RoomNumber;
                roomButton.Tag = $"{room.Id}|{room.PricePerDay}|{room.PricePerHour}";
                roomButton.Click += RoomButton_Click;

                if (room.Status == "available")
                    roomButton.BackColor = System.Drawing.Color.Blue;
                else if (room.Status == "occupied")
                    roomButton.BackColor = System.Drawing.Color.Red;
                else
                    roomButton.BackColor = System.Drawing.Color.Green;

                roomButton.Location = new Point(x, y);
                panel_DanhsachPhong.Controls.Add(roomButton);

                x += buttonWidth + spacing;
                count++;
                if (count % 7 == 0)
                {
                    x = 10;
                    y += buttonHeight + spacing;
                }
            }
        }

        private void combo_Tang_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (combo_Tang.SelectedItem != null)
            {
                string selectedFloor = combo_Tang.SelectedItem.ToString();
                LoadRoomsByFloor(selectedFloor);
            }
        }

        private void RoomButton_Click(object sender, EventArgs e)
        {
            Button roomButton = sender as Button;

            if (roomButton != null)
            {
                if (roomButton.BackColor == System.Drawing.Color.Blue)
                {
                    string roomNumber = roomButton.Text;
                    string tagData = roomButton.Tag.ToString();
                    string[] parts = tagData.Split('|');
                    if (parts.Length == 3)
                    {
                        int roomId = int.Parse(parts[0]);
                        decimal pricePerDay = decimal.Parse(parts[1]);
                        decimal pricePerHour = decimal.Parse(parts[2]);

                        txt_Pdangchon.Text = roomNumber;
                        txt_maPhong.Text = roomId.ToString();
                        txt_GiaPhongTheoNgay.Text = pricePerDay.ToString("N0");
                        txt_GiaPhongTheoGio.Text = pricePerHour.ToString("N0");
                    }
                }
                else if (roomButton.BackColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("Phòng này đã có người ở!");
                }
                else if (roomButton.BackColor == System.Drawing.Color.Green)
                {
                    MessageBox.Show("Phòng này đang bảo trì!");
                }
            }
        }

        private void btn_themThue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_TaoMaPhieu.Text) ||
                string.IsNullOrWhiteSpace(txt_maKH.Text) ||
                string.IsNullOrWhiteSpace(txt_maNV.Text) ||
                string.IsNullOrWhiteSpace(txt_maPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi thêm phiếu thuê!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_tongtiendichvu.Text))
            {
                txt_tongtiendichvu.Text = "0";
            }

            string processedTotalPrice = txt_tongtiendichvu.Text.Replace(",", ".");

            string bookingId = txt_TaoMaPhieu.Text.Trim();

            if (!int.TryParse(txt_maKH.Text, out int customerId) ||
                !int.TryParse(txt_maNV.Text, out int staffId) ||
                !int.TryParse(txt_maPhong.Text, out int roomId) ||
                !decimal.TryParse(processedTotalPrice, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out decimal totalPriceService))
            {
                MessageBox.Show("Thông tin nhập chưa đúng định dạng số! Vui lòng kiểm tra lại.");
                return;
            }

            Booking1DTO newBooking = new Booking1DTO
            {
                Id = bookingId,
                CustomerId = customerId,
                StaffId = staffId,  // Sử dụng staffId được lấy từ txt_maNV
                RoomId = roomId,
                CheckIn = dtimengayden.Value.Date,
                TotalPriceService = totalPriceService
            };

            bookingBLL.AddBooking(newBooking);
            MessageBox.Show("Thêm phiếu thuê thành công và cập nhật trạng thái phòng!");

            foreach (DataGridViewRow row in dgv_DichVu.Rows)
            {
                if (row.IsNewRow) continue;

                object serviceNameObj = row.Cells["ServiceName"].Value;
                object quantityObj = row.Cells["Quantity"].Value;

                if (serviceNameObj == null || quantityObj == null) continue;

                string serviceName = serviceNameObj.ToString().Trim();

                if (!serviceMap.ContainsKey(serviceName))
                {
                    MessageBox.Show($"Dịch vụ '{serviceName}' không tồn tại trong danh sách!");
                    continue;
                }

                int serviceId = serviceMap[serviceName].Id;

                if (!int.TryParse(quantityObj.ToString(), out int quantity))
                {
                    MessageBox.Show($"Số lượng cho dịch vụ '{serviceName}' không hợp lệ.");
                    continue;
                }

                bookingDAL.AddBookingService(bookingId, serviceId, quantity);
            }

            // Load lại danh sách phòng và phiếu thuê
            string selectedFloor = combo_Tang.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedFloor))
            {
                LoadRoomsByFloor(selectedFloor);
            }
            LoadBookingData();
            txt_TaoMaPhieu.Clear();
            txt_maKH.Clear();
            txt_maPhong.Clear();
            txt_tongtiendichvu.Clear();
            dgv_DichVu.DataSource = null;
            dgv_DichVu.Rows.Clear();
        }

        private void LoadBookingData()
        {
            List<BookingInfoDTO> bookings = bookingBLL.GetAllBookingsWithDetails();
            dgv_PhieuThue.DataSource = bookings.Select(b => new
            {
                MaPhieuThue = b.BookingId,
                TenKhachHang = b.CustomerName,
                TenNhanVien = b.StaffName,
                SoPhong = b.RoomNumber,
                NgayNhanPhong = b.CheckInDate,
                TongTienDichVu = b.TotalServicePrice,
                NgayTaoPhieu = b.CreatedAt
            }).ToList();
        }

        private void btn_xoaThue_Click(object sender, EventArgs e)
        {
            if (dgv_PhieuThue.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phiếu thuê để xóa!");
                return;
            }

            DataGridViewRow selectedRow = dgv_PhieuThue.SelectedRows[0];
            string bookingId = selectedRow.Cells["MaPhieuThue"].Value.ToString();
            string roomNumber = selectedRow.Cells["SoPhong"].Value.ToString();

            if (string.IsNullOrEmpty(bookingId) || string.IsNullOrEmpty(roomNumber))
            {
                MessageBox.Show("Không thể xác định phiếu thuê hoặc phòng cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa phiếu thuê này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            bookingBLL.DeleteBooking(bookingId);
            MessageBox.Show("Xóa phiếu thuê thành công và cập nhật trạng thái phòng!");

            // Load lại dữ liệu
            LoadBookingData();
            if (combo_Tang.SelectedItem != null)
            {
                LoadRoomsByFloor(combo_Tang.SelectedItem.ToString());
            }
        }

        private void btn_thoatThue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadBookingData();
        }
    }
}