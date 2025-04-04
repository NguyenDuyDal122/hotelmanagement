using HotelManagement.BLL;
using HotelManagement.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class ManageFloor : Form
    {
        private ManageFloorBLL floorBLL = new ManageFloorBLL();

        public ManageFloor()
        {
            InitializeComponent();
            LoadFloorList();
            this.dataGridView_danhsachtang.CellClick += new DataGridViewCellEventHandler(this.dataGridView_danhsachtang_CellClick);
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadFloorList()
        {
            try
            {
                List<ManageFloorDTO> floorList = floorBLL.GetAllFloors();
                dataGridView_danhsachtang.DataSource = floorList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tầng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            txt_maxroom.Clear();
            txt_description.Clear();
            LoadFloorList();
        }

        private void btn_themtang_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txt_maxroom.Text.Trim(), out int maxRooms) || maxRooms <= 0)
                {
                    MessageBox.Show("Số phòng tối đa phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ManageFloorDTO floor = new ManageFloorDTO
                {
                    MaxRooms = maxRooms,
                    Description = txt_description.Text.Trim()
                };

                if (floorBLL.AddFloor(floor))
                {
                    MessageBox.Show("Thêm tầng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_lammoi_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Thêm tầng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (!int.TryParse(txt_maxroom.Text.Trim(), out int maxRooms) || maxRooms <= 0)
                {
                    MessageBox.Show("Số phòng tối đa phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int floorId = Convert.ToInt32(dataGridView_danhsachtang.SelectedRows[0].Cells["Id"].Value);
                string description = txt_description.Text.Trim();

                ManageFloorDTO floor = new ManageFloorDTO
                {
                    Id = floorId,
                    MaxRooms = maxRooms,
                    Description = description
                };

                if (floorBLL.UpdateFloor(floor))
                {
                    MessageBox.Show("Cập nhật tầng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_lammoi_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                int floorId = Convert.ToInt32(dataGridView_danhsachtang.SelectedRows[0].Cells["Id"].Value);

                DialogResult confirm = MessageBox.Show("Bạn có chắc muốn xóa tầng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No) return;

                if (floorBLL.DeleteFloor(floorId))
                {
                    MessageBox.Show("Xóa tầng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_lammoi_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Xóa tầng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa tầng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_danhsachtang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView_danhsachtang.Rows[e.RowIndex].Cells["Id"].Value != null)
            {
                txt_maxroom.Text = dataGridView_danhsachtang.Rows[e.RowIndex].Cells["MaxRooms"].Value?.ToString();
                txt_description.Text = dataGridView_danhsachtang.Rows[e.RowIndex].Cells["Description"].Value?.ToString();
            }
        }

        private void txt_maxroom_TextChanged(object sender, EventArgs e)
        {
            // Không cần xử lý gì ở đây, nếu không có nhu cầu validate realtime
        }
    }
}
