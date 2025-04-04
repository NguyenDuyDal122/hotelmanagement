using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagement
{
    public class ThemPhongDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public bool IsRoomNumberExists(string roomNumber)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Room WHERE room_number = @room_number";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@room_number", roomNumber);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool IsRoomLimitReached(int floorId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kiểm tra số lượng phòng hiện tại trên tầng
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
                return currentRooms >= maxRooms;
            }
        }

        public bool AddRoom(ThemPhongDTO roomDTO)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = @"
                    INSERT INTO Room (room_number, type_id, floor_id, status) 
                    VALUES (@room_number, @type_id, @floor_id, @status)";

                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@room_number", roomDTO.RoomNumber);
                insertCmd.Parameters.AddWithValue("@type_id", roomDTO.TypeId);
                insertCmd.Parameters.AddWithValue("@floor_id", roomDTO.FloorId);
                insertCmd.Parameters.AddWithValue("@status", roomDTO.Status);

                int rowsAffected = insertCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
