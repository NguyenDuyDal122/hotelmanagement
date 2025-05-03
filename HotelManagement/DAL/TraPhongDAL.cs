using HotelManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagement.DAL
{
    public class TraPhongDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public List<TraPhongDTO> GetAllBookings()
        {
            List<TraPhongDTO> list = new List<TraPhongDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        b.id,
                        b.customer_id,
                        c.full_name AS customer_name,
                        c.phone AS customer_phone,
                        b.staff_id,
                        u.full_name AS staff_name,
                        b.room_id,
                        r.room_number,
                        rt.type_name AS room_type,
                        r.price_per_day,
                        r.price_per_hour,
                        b.check_in,
                        b.total_price_service,
                        b.created_at
                    FROM Booking b
                    JOIN Customer c ON b.customer_id = c.id
                    JOIN [User] u ON b.staff_id = u.id
                    JOIN Room r ON b.room_id = r.id
                    JOIN RoomType rt ON r.type_id = rt.id
                    WHERE b.status = 'checkin'";  // ✅ Chỉ lấy booking chưa trả phòng



                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TraPhongDTO booking = new TraPhongDTO
                    {
                        Id = reader["id"].ToString(),
                        CustomerId = (int)reader["customer_id"],
                        CustomerName = reader["customer_name"].ToString(),
                        CustomerPhone = reader["customer_phone"].ToString(),
                        StaffId = (int)reader["staff_id"],
                        StaffName = reader["staff_name"].ToString(),
                        RoomId = (int)reader["room_id"],
                        RoomNumber = reader["room_number"].ToString(),
                        RoomType = reader["room_type"].ToString(),
                        PriceByDay = (decimal)reader["price_per_day"],
                        PriceByHour = (decimal)reader["price_per_hour"],
                        CheckIn = (DateTime)reader["check_in"],
                        TotalPriceService = (decimal)reader["total_price_service"],
                        CreatedAt = (DateTime)reader["created_at"]
                    };
                    list.Add(booking);
                }
            }
            return list;
        }
        public TraPhongDTO GetBookingById(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        b.id,
                        b.customer_id,
                        c.full_name AS customer_name,
                        c.phone AS customer_phone,
                        b.staff_id,
                        u.full_name AS staff_name,
                        b.room_id,
                        r.room_number,
                        rt.type_name AS room_type,
                        r.price_per_day,
                        r.price_per_hour,
                        b.check_in,
                        b.total_price_service,
                        b.created_at
                    FROM Booking b
                    JOIN Customer c ON b.customer_id = c.id
                    JOIN [User] u ON b.staff_id = u.id
                    JOIN Room r ON b.room_id = r.id
                    JOIN RoomType rt ON r.type_id = rt.id
                    WHERE b.id = @id AND b.status = 'checkin'"; // ✅ Thêm điều kiện status


                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new TraPhongDTO
                    {
                        Id = reader["id"].ToString(),
                        CustomerId = (int)reader["customer_id"],
                        CustomerName = reader["customer_name"].ToString(),
                        CustomerPhone = reader["customer_phone"].ToString(),
                        StaffId = (int)reader["staff_id"],
                        StaffName = reader["staff_name"].ToString(),
                        RoomId = (int)reader["room_id"],
                        RoomNumber = reader["room_number"].ToString(),
                        RoomType = reader["room_type"].ToString(),
                        PriceByDay = (decimal)reader["price_per_day"],
                        PriceByHour = (decimal)reader["price_per_hour"],
                        CheckIn = (DateTime)reader["check_in"],
                        TotalPriceService = (decimal)reader["total_price_service"],
                        CreatedAt = (DateTime)reader["created_at"]
                    };
                }
            }
            return null;
        }
        public bool InsertInvoice(string bookingId, DateTime checkOut, decimal totalAmount, string paymentMethod)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Lấy room_id từ Booking
                    string query = "SELECT room_id FROM Booking WHERE id = @bookingId";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);
                    int roomId = (int)cmd.ExecuteScalar();
                    if (roomId == 0)
                    {
                        throw new Exception("Không tìm thấy phòng liên kết với phiếu thuê.");
                    }

                    // Cập nhật trạng thái phòng thành 'available'
                    query = "UPDATE Room SET status = 'available' WHERE id = @roomId";
                    cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@roomId", roomId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("Không thể cập nhật trạng thái phòng.");
                    }

                    // Thêm dữ liệu vào bảng Invoice
                    query = @"
                INSERT INTO Invoice (booking_id, check_out, total_amount, payment_method)
                VALUES (@bookingId, @checkOut, @totalAmount, @paymentMethod)";
                    cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);
                    cmd.Parameters.AddWithValue("@checkOut", checkOut);
                    cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                    cmd.ExecuteNonQuery();

                    // Cập nhật status trong bảng Booking thành 'checkout'
                    query = "UPDATE Booking SET status = 'checkout' WHERE id = @bookingId";
                    cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);
                    int updatedRows = cmd.ExecuteNonQuery();
                    if (updatedRows == 0)
                    {
                        throw new Exception("Không thể cập nhật trạng thái phiếu thuê.");
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Lỗi trong quá trình xử lý: " + ex.Message);
                }
            }
        }
    }
}
