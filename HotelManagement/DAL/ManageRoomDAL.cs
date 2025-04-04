using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagement
{
    public class ManageRoomDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public DataTable GetRoomData()
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
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tải danh sách phòng: " + ex.Message);
                }
            }
        }

        public DataTable SearchRoom(string roomNumber)
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
                        INNER JOIN Floor ON Room.floor_id = Floor.id
                        WHERE Room.room_number = @roomNumber"; // Tìm theo số phòng
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tìm kiếm phòng: " + ex.Message);
                }
            }
        }

        public bool DeleteRoom(int roomId)
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
                        throw new Exception("Không thể xóa phòng vì phòng đang có khách đặt!");
                    }

                    string deleteQuery = "DELETE FROM Room WHERE id = @roomId";
                    SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@roomId", roomId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi xóa phòng: " + ex.Message);
                }
            }
        }

        public void ExportToExcel(DataTable dt, string filePath)
        {
            using (ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DanhSachPhong");
                wb.SaveAs(filePath);
            }
        }
    }
}
