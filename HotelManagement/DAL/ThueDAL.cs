using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelManagement.DTO;

namespace HotelManagement.DAL
{
    public class DatabaseHelper
    {
        private static string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }

    public class Customer1DAL
    {
        public List<Customer1DTO> GetAllCustomers()
        {
            List<Customer1DTO> customers = new List<Customer1DTO>();
            string query = "SELECT * FROM Customer";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer1DTO
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FullName = reader.GetString(reader.GetOrdinal("full_name")),
                            Address = reader.GetString(reader.GetOrdinal("address")),
                            Phone = reader.GetString(reader.GetOrdinal("phone")),
                            IdCard = reader.GetString(reader.GetOrdinal("identity_card"))
                        });
                    }
                }
            }
            return customers;
        }

        public List<Customer1DTO> FindCustomersByPhone(string phone)
        {
            List<Customer1DTO> customers = new List<Customer1DTO>();
            string query = "SELECT * FROM Customer WHERE phone LIKE @phone";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@phone", "%" + phone + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer1DTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                FullName = reader.GetString(reader.GetOrdinal("full_name")),
                                Address = reader.GetString(reader.GetOrdinal("address")),
                                Phone = reader.GetString(reader.GetOrdinal("phone")),
                                IdCard = reader.GetString(reader.GetOrdinal("identity_card"))
                            });
                        }
                    }
                }
            }
            return customers;
        }
    }

    public class Service1DAL
    {
        public Dictionary<string, decimal> GetAllServices()
        {
            Dictionary<string, decimal> services = new Dictionary<string, decimal>();
            string query = "SELECT service_name, price FROM Service";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        services.Add(reader.GetString(reader.GetOrdinal("service_name")), reader.GetDecimal(reader.GetOrdinal("price")));
                    }
                }
            }
            return services;
        }
    }

    public class Floor1DAL
    {
        public List<Floor1DTO> GetAllFloors()
        {
            List<Floor1DTO> floors = new List<Floor1DTO>();
            string query = "SELECT id, description FROM Floor";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        floors.Add(new Floor1DTO
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Description = reader.GetString(reader.GetOrdinal("description"))
                        });
                    }
                }
            }
            return floors;
        }
    }

    public class Room1DAL
    {
        public List<Room1DTO> GetRoomsByFloorDescription(string floorDescription)
        {
            List<Room1DTO> rooms = new List<Room1DTO>();
            string query = @"
                SELECT Room.id, Room.room_number, Room.status, Room.price_per_day, Room.price_per_hour, Room.floor_id
                FROM Room
                INNER JOIN Floor ON Room.floor_id = Floor.id
                WHERE Floor.description = @description";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@description", floorDescription);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room1DTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                FloorId = reader.GetInt32(reader.GetOrdinal("floor_id")),
                                RoomNumber = reader.GetString(reader.GetOrdinal("room_number")),
                                Status = reader.GetString(reader.GetOrdinal("status")),
                                PricePerDay = reader.GetDecimal(reader.GetOrdinal("price_per_day")),
                                PricePerHour = reader.GetDecimal(reader.GetOrdinal("price_per_hour"))
                            });
                        }
                    }
                }
            }
            return rooms;
        }

        public void UpdateRoomStatus(int roomId, string status)
        {
            string query = "UPDATE Room SET status = @status WHERE id = @roomId";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@roomId", roomId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Room1DTO GetRoomById(int roomId)
        {
            string query = "SELECT id, floor_id, room_number, status, price_per_day, price_per_hour FROM Room WHERE id = @roomId";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@roomId", roomId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Room1DTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                FloorId = reader.GetInt32(reader.GetOrdinal("floor_id")),
                                RoomNumber = reader.GetString(reader.GetOrdinal("room_number")),
                                Status = reader.GetString(reader.GetOrdinal("status")),
                                PricePerDay = reader.GetDecimal(reader.GetOrdinal("price_per_day")),
                                PricePerHour = reader.GetDecimal(reader.GetOrdinal("price_per_hour"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public Room1DTO GetRoomByNumber(string roomNumber)
        {
            string query = "SELECT id, floor_id, room_number, status, price_per_day, price_per_hour FROM Room WHERE room_number = @roomNumber";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Room1DTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                FloorId = reader.GetInt32(reader.GetOrdinal("floor_id")),
                                RoomNumber = reader.GetString(reader.GetOrdinal("room_number")),
                                Status = reader.GetString(reader.GetOrdinal("status")),
                                PricePerDay = reader.GetDecimal(reader.GetOrdinal("price_per_day")),
                                PricePerHour = reader.GetDecimal(reader.GetOrdinal("price_per_hour"))
                            };
                        }
                    }
                }
            }
            return null;
        }
    }

    public class Booking1DAL
    {
        public void AddBooking(Booking1DTO booking)
        {
            string query = @"
                INSERT INTO Booking (id, customer_id, staff_id, room_id, check_in, total_price_service)
                VALUES (@id, @customer_id, @staff_id, @room_id, @check_in, @total_price_service)";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", booking.Id);
                    cmd.Parameters.AddWithValue("@customer_id", booking.CustomerId);
                    cmd.Parameters.AddWithValue("@staff_id", booking.StaffId);
                    cmd.Parameters.AddWithValue("@room_id", booking.RoomId);
                    cmd.Parameters.AddWithValue("@check_in", booking.CheckIn);
                    cmd.Parameters.AddWithValue("@total_price_service", booking.TotalPriceService);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<BookingInfoDTO> GetAllBookingsWithDetails()
        {
            List<BookingInfoDTO> bookings = new List<BookingInfoDTO>();
            string query = @"
                SELECT
                    Booking.id AS BookingId,
                    Customer.full_name AS CustomerName,
                    [User].full_name AS StaffName,
                    Room.room_number AS RoomNumber,
                    Booking.check_in AS CheckInDate,
                    Booking.total_price_service AS TotalServicePrice,
                    Booking.created_at AS CreatedAt
                FROM Booking
                INNER JOIN Customer ON Booking.customer_id = Customer.id
                INNER JOIN [User] ON Booking.staff_id = [User].id
                INNER JOIN Room ON Booking.room_id = Room.id
                WHERE Booking.status = 'checkin'";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookings.Add(new BookingInfoDTO
                        {
                            BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                            CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                            StaffName = reader.GetString(reader.GetOrdinal("StaffName")),
                            RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                            CheckInDate = reader.GetDateTime(reader.GetOrdinal("CheckInDate")),
                            TotalServicePrice = reader.GetDecimal(reader.GetOrdinal("TotalServicePrice")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        });
                    }
                }
            }
            return bookings;
        }

        public void DeleteBooking(string bookingId)
        {
            string query = "DELETE FROM Booking WHERE id = @id";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", bookingId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    // DTO để chứa thông tin chi tiết của phiếu thuê
    public class BookingInfoDTO
    {
        public string BookingId { get; set; }
        public string CustomerName { get; set; }
        public string StaffName { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public decimal TotalServicePrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}