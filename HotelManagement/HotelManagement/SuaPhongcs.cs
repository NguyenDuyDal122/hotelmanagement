using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class SuaPhongcs : Form
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Nạp dữ liệu vào combobox loại phòng
                    string queryRoomType = "SELECT id, type_name FROM RoomType";
                    SqlDataAdapter daRoomType = new SqlDataAdapter(queryRoomType, conn);
                    DataTable dtRoomType = new DataTable();
                    daRoomType.Fill(dtRoomType);
                    comboBox_loaiphong.DataSource = dtRoomType;
                    comboBox_loaiphong.DisplayMember = "type_name";
                    comboBox_loaiphong.ValueMember = "id";

                    // Nạp dữ liệu vào combobox tầng
                    string queryFloor = "SELECT id, description FROM Floor";
                    SqlDataAdapter daFloor = new SqlDataAdapter(queryFloor, conn);
                    DataTable dtFloor = new DataTable();
                    daFloor.Fill(dtFloor);
                    comboBox_tang.DataSource = dtFloor;
                    comboBox_tang.DisplayMember = "description";
                    comboBox_tang.ValueMember = "id";

                    // Lấy thông tin phòng cần sửa
                    string query = @"
                SELECT room_number, type_id, floor_id, status
                FROM Room
                WHERE id = @roomId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@roomId", roomId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txt_sophong.Text = reader["room_number"].ToString();
                        comboBox_loaiphong.SelectedValue = Convert.ToInt32(reader["type_id"]);
                        comboBox_tang.SelectedValue = Convert.ToInt32(reader["floor_id"]);
                        comboBox_trangthai.Text = reader["status"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải thông tin phòng: " + ex.Message);
                }
            }
        }

        private void btn__Click(object sender, EventArgs e)
        {
            this.Close();
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kiểm tra xem số phòng có bị trùng không (trừ phòng hiện tại)
                    string checkQuery = "SELECT COUNT(*) FROM Room WHERE room_number = @roomNumber AND id != @roomId";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                    checkCmd.Parameters.AddWithValue("@roomId", roomId);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Số phòng này đã tồn tại. Vui lòng nhập số khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Cập nhật thông tin phòng
                    string updateQuery = @"
                UPDATE Room
                SET room_number = @roomNumber, type_id = @roomTypeId, floor_id = @floorId, status = @status
                WHERE id = @roomId";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                    cmd.Parameters.AddWithValue("@roomTypeId", roomTypeId);
                    cmd.Parameters.AddWithValue("@floorId", floorId);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@roomId", roomId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Đóng form sau khi cập nhật thành công
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}