using System.Data.SqlClient;
using System;
using System.Data;

namespace HotelManagement
{
    public class SuaPhongDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public DataTable GetRoomTypes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, type_name FROM RoomType";
                SqlDataAdapter daRoomType = new SqlDataAdapter(query, conn);
                DataTable dtRoomType = new DataTable();
                daRoomType.Fill(dtRoomType);
                return dtRoomType;
            }
        }

        public DataTable GetFloors()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, description FROM Floor";
                SqlDataAdapter daFloor = new SqlDataAdapter(query, conn);
                DataTable dtFloor = new DataTable();
                daFloor.Fill(dtFloor);
                return dtFloor;
            }
        }

        public SuaPhongDTO GetRoomDetails(int roomId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT room_number, type_id, floor_id, status FROM Room WHERE id = @roomId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@roomId", roomId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new SuaPhongDTO(
                        roomId,
                        reader["room_number"].ToString(),
                        Convert.ToInt32(reader["type_id"]),
                        Convert.ToInt32(reader["floor_id"]),
                        reader["status"].ToString()
                    );
                }
                return null;
            }
        }

        public bool CheckRoomNumberExists(int roomId, string roomNumber)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Room WHERE room_number = @roomNumber AND id != @roomId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                cmd.Parameters.AddWithValue("@roomId", roomId);
                conn.Open();

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool UpdateRoom(SuaPhongDTO room)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Room
                    SET room_number = @roomNumber, type_id = @roomTypeId, floor_id = @floorId, status = @status
                    WHERE id = @roomId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@roomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@roomTypeId", room.RoomTypeId);
                cmd.Parameters.AddWithValue("@floorId", room.FloorId);
                cmd.Parameters.AddWithValue("@status", room.Status);
                cmd.Parameters.AddWithValue("@roomId", room.RoomId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool IsFloorFull(int floorId, int excludeRoomId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                (SELECT COUNT(*) FROM Room WHERE floor_id = @floorId AND id != @excludeRoomId) AS CurrentRooms,
                (SELECT max_rooms FROM Floor WHERE id = @floorId) AS MaxRooms";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@floorId", floorId);
                cmd.Parameters.AddWithValue("@excludeRoomId", excludeRoomId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int currentRooms = Convert.ToInt32(reader["CurrentRooms"]);
                    int maxRooms = Convert.ToInt32(reader["MaxRooms"]);
                    return currentRooms >= maxRooms;
                }
                return true; // Nếu lỗi gì đó thì cho là đầy để tránh sửa sai
            }
        }
    }
}
