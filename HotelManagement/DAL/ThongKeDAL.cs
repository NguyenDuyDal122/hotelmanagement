using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class ThongKeDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public List<ThongKeDTO> GetInvoiceListByMonth(int month)
        {
            List<ThongKeDTO> list = new List<ThongKeDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
        SELECT i.id, u.id AS staff_id, c.full_name, r.room_number, i.check_out, i.total_amount, i.payment_method
        FROM Invoice i
        JOIN Booking b ON i.booking_id = b.id
        JOIN [User] u ON b.staff_id = u.id
        JOIN Customer c ON b.customer_id = c.id
        JOIN Room r ON b.room_id = r.id
        WHERE MONTH(i.check_out) = @month";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongKeDTO dto = new ThongKeDTO(
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetDateTime(4),
                                reader.GetDecimal(5),
                                reader.GetString(6)
                            );
                            list.Add(dto);
                        }
                    }
                }
            }
            return list;
        }

    }
}
