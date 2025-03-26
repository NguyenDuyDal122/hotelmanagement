using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ClosedXML.Excel;

namespace HotelManagement
{
    public partial class ManageRoom : Form
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
        public ManageRoom()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ManageRoom_Load);
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void dataGridView_danhsachphong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ManageRoom_Load(object sender, EventArgs e)
        {
            LoadRoomData();
        }


        private void LoadRoomData()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT Room.id, Room.room_number, RoomType.type_name, Floor.description AS floor_name, Room.status
                FROM Room
                INNER JOIN RoomType ON Room.type_id = RoomType.id
                INNER JOIN Floor ON Room.floor_id = Floor.id";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView_danhsachphong.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách phòng: " + ex.Message);
                }
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string roomNumber = txt_sophong.Text.Trim(); // Lấy số phòng từ TextBox

            if (string.IsNullOrEmpty(roomNumber))
            {
                MessageBox.Show("Vui lòng nhập số phòng cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT Room.id, Room.room_number, RoomType.type_name, Floor.description AS floor_name, Room.status
                FROM Room
                INNER JOIN RoomType ON Room.type_id = RoomType.id
                INNER JOIN Floor ON Room.floor_id = Floor.id
                WHERE Room.room_number = @roomNumber"; // Tìm theo số phòng

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView_danhsachphong.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng có số: " + roomNumber, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_sophong.Clear();
                        dataGridView_danhsachphong.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemPhong manageThemPhongForm = new ThemPhong();
            manageThemPhongForm.ShowDialog();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachphong.SelectedRows.Count > 0) // Kiểm tra xem có dòng nào được chọn không
            {
                // Lấy ID phòng từ dòng được chọn
                int roomId = Convert.ToInt32(dataGridView_danhsachphong.SelectedRows[0].Cells["id"].Value);

                // Mở form sửa phòng và truyền ID phòng
                SuaPhongcs suaPhongForm = new SuaPhongcs(roomId);
                suaPhongForm.ShowDialog();

                // Cập nhật lại danh sách phòng sau khi sửa
                LoadRoomData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView_danhsachphong.SelectedRows.Count > 0)
            {
                int roomId = Convert.ToInt32(dataGridView_danhsachphong.SelectedRows[0].Cells["id"].Value);
                string roomNumber = dataGridView_danhsachphong.SelectedRows[0].Cells["room_number"].Value.ToString();

                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng {roomNumber} không?",
                                                      "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            // Kiểm tra xem phòng có đang được sử dụng trong bảng khác không
                            string checkQuery = "SELECT COUNT(*) FROM Booking WHERE room_id = @roomId";
                            SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                            checkCmd.Parameters.AddWithValue("@roomId", roomId);
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Không thể xóa phòng vì phòng đang có khách đặt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Thực hiện xóa phòng
                            string deleteQuery = "DELETE FROM Room WHERE id = @roomId";
                            SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                            cmd.Parameters.AddWithValue("@roomId", roomId);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadRoomData(); // Cập nhật lại danh sách phòng
                            }
                            else
                            {
                                MessageBox.Show("Xóa phòng thất bại, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_xuatexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_danhsachphong.Rows.Count > 0)
                {
                    // Tạo SaveFileDialog để chọn nơi lưu file
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Chọn nơi lưu file Excel";
                    saveFileDialog.FileName = "DanhSachPhong.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        ExportToExcel(filePath);
                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportToExcel(string filePath)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();

                // Tạo các cột từ DataGridView
                foreach (DataGridViewColumn col in dataGridView_danhsachphong.Columns)
                {
                    dt.Columns.Add(col.HeaderText);
                }

                // Thêm dữ liệu từ DataGridView vào DataTable
                foreach (DataGridViewRow row in dataGridView_danhsachphong.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < dataGridView_danhsachphong.Columns.Count; j++)
                        {
                            dr[j] = row.Cells[j].Value?.ToString() ?? "";
                        }
                        dt.Rows.Add(dr);
                    }
                }

                // Thêm DataTable vào file Excel
                wb.Worksheets.Add(dt, "DanhSachPhong");
                wb.SaveAs(filePath);
            }
        }
    }
}
