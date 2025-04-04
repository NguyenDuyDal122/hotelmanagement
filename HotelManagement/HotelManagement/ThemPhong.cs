using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class ThemPhong : Form
    {
        private ThemPhongBLL roomBLL = new ThemPhongBLL();

        public ThemPhong()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ThemPhong_Load);
        }

        private void ThemPhong_Load(object sender, EventArgs e)
        {
            LoadDataToComboBox();
        }

        private void btn__Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txt_maxroom_TextChanged(object sender, EventArgs e)
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

        private void LoadDataToComboBox()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    conn.Open();

                    // Load danh sách loại phòng
                    string queryRoomType = "SELECT id, type_name FROM RoomType";
                    SqlDataAdapter adapterRoomType = new SqlDataAdapter(queryRoomType, conn);
                    DataTable dtRoomType = new DataTable();
                    adapterRoomType.Fill(dtRoomType);
                    comboBox_loaiphong.DataSource = dtRoomType;
                    comboBox_loaiphong.DisplayMember = "type_name";
                    comboBox_loaiphong.ValueMember = "id";

                    // Load danh sách tầng
                    string queryFloor = "SELECT id, description FROM Floor";
                    SqlDataAdapter adapterFloor = new SqlDataAdapter(queryFloor, conn);
                    DataTable dtFloor = new DataTable();
                    adapterFloor.Fill(dtFloor);
                    comboBox_tang.DataSource = dtFloor;
                    comboBox_tang.DisplayMember = "description";
                    comboBox_tang.ValueMember = "id";

                    // Load danh sách trạng thái
                    comboBox_trangthai.Items.Clear();
                    comboBox_trangthai.Items.Add("available");
                    comboBox_trangthai.Items.Add("occupied");
                    comboBox_trangthai.Items.Add("maintenance");
                    comboBox_trangthai.SelectedIndex = 0; // Mặc định chọn "available"
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường nhập
            string roomNumber = txt_sophong.Text.Trim();
            int typeId = Convert.ToInt32(comboBox_loaiphong.SelectedValue);
            int floorId = Convert.ToInt32(comboBox_tang.SelectedValue);
            string status = comboBox_trangthai.SelectedItem.ToString();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(roomNumber))
            {
                MessageBox.Show("Vui lòng nhập số phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số phòng có trùng không
            if (roomBLL.IsRoomNumberExists(roomNumber))
            {
                MessageBox.Show("Số phòng đã tồn tại, vui lòng nhập số khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số lượng phòng trên tầng
            if (roomBLL.IsRoomLimitReached(floorId))
            {
                MessageBox.Show("Số lượng phòng trên tầng đã đạt tối đa, không thể thêm phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng DTO và thêm phòng vào cơ sở dữ liệu
            ThemPhongDTO roomDTO = new ThemPhongDTO
            {
                RoomNumber = roomNumber,
                TypeId = typeId,
                FloorId = floorId,
                Status = status
            };

            if (roomBLL.AddRoom(roomDTO))
            {
                MessageBox.Show("Thêm phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields(); // Xóa nội dung nhập sau khi thêm thành công
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm phòng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm xóa nội dung nhập sau khi thêm thành công
        private void ClearFields()
        {
            txt_sophong.Clear();
            comboBox_loaiphong.SelectedIndex = 0;
            comboBox_tang.SelectedIndex = 0;
            comboBox_trangthai.SelectedIndex = 0;
        }
    }
}
