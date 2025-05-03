using System;

namespace HotelManagement.DTO
{
    public class Customer1DTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string IdCard { get; set; }
    }

    public class Service1DTO
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }

    public class Floor1DTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class Room1DTO
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public string RoomNumber { get; set; }
        public string Status { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PricePerHour { get; set; }
    }

    public class Booking1DTO
    {
        public string Id { get; set; }
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public decimal TotalPriceService { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}