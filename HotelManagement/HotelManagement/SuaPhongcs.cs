using System.Windows.Forms;
using System;

namespace HotelManagement
{
    public partial class SuaPhongcs : Form
    {
        private SuaPhongBLL roomBLL = new SuaPhongBLL();
        private int roomId;

        public SuaPhongcs(int roomId)
        {
            InitializeComponent();
            this.roomId = roomId;
            LoadRoomDetails();
        }
        private void txt_sophong_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_loaiphong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_tang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_trangthai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadRoomDetails()
        {
            // Load dữ liệu cho các combobox
            comboBox_loaiphong.DataSource = roomBLL.LoadRoomTypes();
            comboBox_loaiphong.DisplayMember = "type_name";
            comboBox_loaiphong.ValueMember = "id";

            comboBox_tang.DataSource = roomBLL.LoadFloors();
            comboBox_tang.DisplayMember = "description";
            comboBox_tang.ValueMember = "id";

            // Nạp dữ liệu trạng thái vào comboBox_trangthai
            comboBox_trangthai.Items.Clear();
            comboBox_trangthai.Items.Add("available");
            comboBox_trangthai.Items.Add("occupied");
            comboBox_trangthai.Items.Add("maintenance");

            // Lấy thông tin phòng cần sửa
            SuaPhongDTO room = roomBLL.LoadRoomDetails(roomId);
            if (room != null)
            {
                txt_sophong.Text = room.RoomNumber;
                comboBox_loaiphong.SelectedValue = room.RoomTypeId;
                comboBox_tang.SelectedValue = room.FloorId;

                // Đảm bảo comboBox_trangthai có giá trị tương ứng
                if (comboBox_trangthai.Items.Contains(room.Status))
                {
                    comboBox_trangthai.SelectedItem = room.Status;
                }
                else
                {
                    comboBox_trangthai.SelectedIndex = 0; // fallback nếu giá trị không khớp
                }
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string roomNumber = txt_sophong.Text.Trim();
            int roomTypeId = Convert.ToInt32(comboBox_loaiphong.SelectedValue);
            int floorId = Convert.ToInt32(comboBox_tang.SelectedValue);
            string status = comboBox_trangthai.Text;

            if (string.IsNullOrEmpty(roomNumber))
            {
                MessageBox.Show("Vui lòng nhập số phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem số phòng có bị trùng không
            if (roomBLL.CheckRoomNumberExists(roomId, roomNumber))
            {
                MessageBox.Show("Số phòng này đã tồn tại. Vui lòng nhập số khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra nếu tầng đã đầy thì không cho sửa
            if (roomBLL.IsFloorFull(floorId, roomId))
            {
                MessageBox.Show("Tầng đã đủ số lượng phòng tối đa. Không thể chuyển phòng này sang tầng đó!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng SuaPhongDTO
            SuaPhongDTO room = new SuaPhongDTO(roomId, roomNumber, roomTypeId, floorId, status);

            // Cập nhật phòng
            if (roomBLL.UpdateRoom(room))
            {
                MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi cập nhật thành công
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn__Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
