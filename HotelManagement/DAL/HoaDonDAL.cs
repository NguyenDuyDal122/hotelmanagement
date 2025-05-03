using HotelManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HotelManagement.DAL
{
    public class HoaDonDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public List<HoaDonDTO> GetAllHoaDon()
        {
            List<HoaDonDTO> list = new List<HoaDonDTO>();
            string query = @"
                SELECT 
                    i.id AS InvoiceID,
                    b.id AS BookingID,
                    c.full_name AS CustomerName,
                    u.full_name AS StaffName,
                    r.room_number AS RoomNumber,
                    b.check_in AS CheckInDate,
                    i.check_out AS CheckOutDate,
                    b.total_price_service AS TotalService,
                    i.total_amount AS TotalAmount,
                    i.payment_method AS PaymentMethod,
                    i.created_at AS CreatedAt
                FROM Invoice i
                JOIN Booking b ON i.booking_id = b.id
                JOIN Customer c ON b.customer_id = c.id
                JOIN [User] u ON b.staff_id = u.id
                JOIN Room r ON b.room_id = r.id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    HoaDonDTO hoaDon = new HoaDonDTO()
                    {
                        InvoiceID = reader.GetInt32(0),
                        BookingID = reader.GetString(1),
                        CustomerName = reader.GetString(2),
                        StaffName = reader.GetString(3),
                        RoomNumber = reader.GetString(4),
                        CheckInDate = reader.GetDateTime(5),
                        CheckOutDate = reader.GetDateTime(6),
                        TotalService = reader.GetDecimal(7),
                        TotalAmount = reader.GetDecimal(8),
                        PaymentMethod = reader.GetString(9),
                        CreatedAt = reader.GetDateTime(10)
                    };
                    list.Add(hoaDon);
                }
            }

            return list;
        }
        public List<HoaDonDTO> SearchHoaDonByBookingID(string bookingID)
        {
            List<HoaDonDTO> list = new List<HoaDonDTO>();
            string query = @"
        SELECT 
            i.id AS InvoiceID,
            b.id AS BookingID,
            c.full_name AS CustomerName,
            u.full_name AS StaffName,
            r.room_number AS RoomNumber,
            b.check_in AS CheckInDate,
            i.check_out AS CheckOutDate,
            b.total_price_service AS TotalService,
            i.total_amount AS TotalAmount,
            i.payment_method AS PaymentMethod,
            i.created_at AS CreatedAt
        FROM Invoice i
        JOIN Booking b ON i.booking_id = b.id
        JOIN Customer c ON b.customer_id = c.id
        JOIN [User] u ON b.staff_id = u.id
        JOIN Room r ON b.room_id = r.id
        WHERE b.id LIKE @bookingID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@bookingID", $"%{bookingID}%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    HoaDonDTO hoaDon = new HoaDonDTO()
                    {
                        InvoiceID = reader.GetInt32(0),
                        BookingID = reader.GetString(1),
                        CustomerName = reader.GetString(2),
                        StaffName = reader.GetString(3),
                        RoomNumber = reader.GetString(4),
                        CheckInDate = reader.GetDateTime(5),
                        CheckOutDate = reader.GetDateTime(6),
                        TotalService = reader.GetDecimal(7),
                        TotalAmount = reader.GetDecimal(8),
                        PaymentMethod = reader.GetString(9),
                        CreatedAt = reader.GetDateTime(10)
                    };
                    list.Add(hoaDon);
                }
            }

            return list;
        }
        public bool DeleteHoaDon(int invoiceID)
        {
            string query = "DELETE FROM Invoice WHERE id = @InvoiceID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}