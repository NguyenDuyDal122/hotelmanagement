using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class ManageRoom : Form
    {
        private ManageRoomBLL roomBLL = new ManageRoomBLL();

        public ManageRoom()
        {
            InitializeComponent();
            this.Load += new EventHandler(this.ManageRoom_Load);
        }

        private void ManageRoom_Load(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void LoadRoomData()
        {
            try
            {
                DataTable dt = roomBLL.GetRoomData();
                dataGridView_danhsachphong.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            try
            {
                DataTable dt = roomBLL.SearchRoom(roomNumber);
                if (dt.Rows.Count > 0)
                {
                    dataGridView_danhsachphong.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phòng có số: " + roomNumber, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView_danhsachphong.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    try
                    {
                        bool success = roomBLL.DeleteRoom(roomId);
                        if (success)
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
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Chọn nơi lưu file Excel";
                    saveFileDialog.FileName = "DanhSachPhong.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        DataTable dt = (DataTable)dataGridView_danhsachphong.DataSource;
                        roomBLL.ExportToExcel(dt, filePath);
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
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void dataGridView_danhsachphong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}
