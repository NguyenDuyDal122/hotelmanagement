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
    public partial class ManageFloor : Form
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
        public ManageFloor()
        {
            InitializeComponent();
            LoadStaffList();
            this.dataGridView_danhsachtang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_danhsachtang_CellClick);

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadStaffList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, max_rooms, description FROM [Floor]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView_danhsachtang.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tầng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            LoadStaffList();
        }

        private void btn_themtang_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các TextBox
                int maxRooms;
                if (!int.TryParse(txt_maxroom.Text.Trim(), out maxRooms) || maxRooms <= 0)
                {
                    MessageBox.Show("Số phòng tối đa phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string description = txt_description.Text.Trim();

                // Tạo kết nối đến SQL Server
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Floor (max_rooms, description) VALUES (@max_rooms, @description)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@max_rooms", maxRooms);
                        cmd.Parameters.AddWithValue("@description", description);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm tầng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txt_maxroom.Clear();
                            txt_description.Clear();
                            LoadStaffList(); // Cập nhật lại danh sách sau khi thêm mới
                        }
                        else
                        {
                            MessageBox.Show("Thêm tầng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tầng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_danhsachtang.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn tầng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int floorId = Convert.ToInt32(dataGridView_danhsachtang.SelectedRows[0].Cells["id"].Value);
                int maxRooms;
                if (!int.TryParse(txt_maxroom.Text.Trim(), out maxRooms) || maxRooms <= 0)
                {
                    MessageBox.Show("Số phòng tối đa phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string description = txt_description.Text.Trim();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Floor SET max_rooms = @max_rooms, description = @description WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@max_rooms", maxRooms);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@id", floorId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật tầng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txt_maxroom.Clear();
                            txt_description.Clear();
                            LoadStaffList();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tầng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_danhsachtang.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn tầng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int floorId = Convert.ToInt32(dataGridView_danhsachtang.SelectedRows[0].Cells["id"].Value);

                DialogResult confirm = MessageBox.Show("Bạn có chắc muốn xóa tầng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.No)
                {
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Floor WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", floorId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa tầng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txt_maxroom.Clear();
                            txt_description.Clear();
                            LoadStaffList();
                        }
                        else
                        {
                            MessageBox.Show("Xóa tầng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa tầng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView_danhsachtang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView_danhsachtang.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DataGridViewRow row = dataGridView_danhsachtang.Rows[e.RowIndex];
                txt_maxroom.Text = row.Cells["max_rooms"].Value?.ToString() ?? "";
                txt_description.Text = row.Cells["description"].Value?.ToString() ?? "";
            }
            else
            {
                txt_maxroom.Clear();
                txt_description.Clear();
            }
        }

        private void txt_maxroom_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
