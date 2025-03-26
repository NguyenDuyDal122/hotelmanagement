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
    public partial class ThemPhong : Form
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
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
            using (SqlConnection conn = new SqlConnection(connectionString))
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kiểm tra xem số phòng có bị trùng không
                    string checkQuery = "SELECT COUNT(*) FROM Room WHERE room_number = @room_number";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@room_number", roomNumber);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Số phòng đã tồn tại, vui lòng nhập số khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra số lượng phòng trên tầng hiện tại
                    string countRoomQuery = "SELECT COUNT(*) FROM Room WHERE floor_id = @floor_id";
                    SqlCommand countRoomCmd = new SqlCommand(countRoomQuery, conn);
                    countRoomCmd.Parameters.AddWithValue("@floor_id", floorId);
                    int currentRooms = (int)countRoomCmd.ExecuteScalar();

                    // Lấy max_rooms của tầng
                    string maxRoomQuery = "SELECT max_rooms FROM Floor WHERE id = @floor_id";
                    SqlCommand maxRoomCmd = new SqlCommand(maxRoomQuery, conn);
                    maxRoomCmd.Parameters.AddWithValue("@floor_id", floorId);
                    int maxRooms = (int)maxRoomCmd.ExecuteScalar();

                    // Kiểm tra nếu số phòng hiện tại đã đạt max_rooms
                    if (currentRooms >= maxRooms)
                    {
                        MessageBox.Show("Số lượng phòng trên tầng đã đạt tối đa, không thể thêm phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Thêm phòng vào database
                    string insertQuery = @"
                INSERT INTO Room (room_number, type_id, floor_id, status) 
                VALUES (@room_number, @type_id, @floor_id, @status)";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@room_number", roomNumber);
                    insertCmd.Parameters.AddWithValue("@type_id", typeId);
                    insertCmd.Parameters.AddWithValue("@floor_id", floorId);
                    insertCmd.Parameters.AddWithValue("@status", status);

                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
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
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
