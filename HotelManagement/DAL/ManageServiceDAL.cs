using HotelManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagement
{
    public class ServiceDAL
    {
        private readonly string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public List<ServiceDTO> GetAllServices()
        {
            List<ServiceDTO> list = new List<ServiceDTO>();
            string query = "SELECT id, service_name, description, price FROM Service";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ServiceDTO
                        {
                            Id = reader.GetInt32(0),
                            ServiceName = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Price = reader.GetDecimal(3)
                        });
                    }
                }
            }
            return list;
        }

        public bool InsertService(ServiceDTO service)
        {
            string checkQuery = "SELECT COUNT(*) FROM Service WHERE service_name = @name";
            string insertQuery = "INSERT INTO Service (service_name, description, price) VALUES (@name, @desc, @price)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@name", service.ServiceName);
                conn.Open();
                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    return false; // Đã tồn tại service_name
                }

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@name", service.ServiceName);
                    insertCmd.Parameters.AddWithValue("@desc", service.Description ?? "");
                    insertCmd.Parameters.AddWithValue("@price", service.Price);

                    return insertCmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateService(ServiceDTO service)
        {
            string checkQuery = "SELECT COUNT(*) FROM Service WHERE service_name = @name AND id != @id";
            string updateQuery = "UPDATE Service SET service_name = @name, description = @desc, price = @price WHERE id = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@name", service.ServiceName);
                checkCmd.Parameters.AddWithValue("@id", service.Id);
                conn.Open();

                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    return false; // Đã có bản ghi khác trùng tên
                }

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@id", service.Id);
                    updateCmd.Parameters.AddWithValue("@name", service.ServiceName);
                    updateCmd.Parameters.AddWithValue("@desc", service.Description ?? "");
                    updateCmd.Parameters.AddWithValue("@price", service.Price);

                    return updateCmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteService(int id)
        {
            string query = "DELETE FROM Service WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<ServiceDTO> GetServicesByBookingId(int bookingId)
        {
            List<ServiceDTO> services = new List<ServiceDTO>();
            string query = @"SELECT s.id, s.service_name, s.price, su.quantity 
                     FROM ServiceUsed su
                     JOIN Service s ON su.service_id = s.id
                     WHERE su.booking_id = @BookingId";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@BookingId", bookingId);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    services.Add(new ServiceDTO
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        ServiceName = reader["service_name"].ToString(),
                        Price = Convert.ToDecimal(reader["price"]),
                        Quantity = Convert.ToInt32(reader["quantity"])
                    });
                }
            }
            return services;


        }
    }
}
