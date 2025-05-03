using System;
using System.Collections.Generic;
using HotelManagement.DTO;
using HotelManagement.DAL;
using System.Linq;

namespace HotelManagement.BLL
{
    public class Customer1BLL
    {
        private Customer1DAL customerDAL = new Customer1DAL();

        public List<Customer1DTO> GetAllCustomers()
        {
            return customerDAL.GetAllCustomers();
        }

        public List<Customer1DTO> FindCustomersByPhone(string phone)
        {
            return customerDAL.FindCustomersByPhone(phone);
        }
    }

    public class Service1BLL
    {
        private Service1DAL serviceDAL = new Service1DAL(); // Khởi tạo tại đây

        public Dictionary<string, decimal> GetAllServices()
        {
            return serviceDAL.GetAllServices();
        }
    }

    public class Floor1BLL
    {
        private Floor1DAL floorDAL = new Floor1DAL();

        public List<Floor1DTO> GetAllFloors()
        {
            return floorDAL.GetAllFloors();
        }
    }

    public class Room1BLL
    {
        private Room1DAL roomDAL = new Room1DAL();

        public List<Room1DTO> GetRoomsByFloorDescription(string floorDescription)
        {
            return roomDAL.GetRoomsByFloorDescription(floorDescription);
        }

        public void UpdateRoomStatus(int roomId, string status)
        {
            roomDAL.UpdateRoomStatus(roomId, status);
        }

        public Room1DTO GetRoomById(int roomId)
        {
            return roomDAL.GetRoomById(roomId);
        }

        public Room1DTO GetRoomByNumber(string roomNumber)
        {
            return roomDAL.GetRoomByNumber(roomNumber);
        }
    }

    public class Booking1BLL
    {
        private Booking1DAL bookingDAL = new Booking1DAL();
        private Room1BLL roomBLL = new Room1BLL();

        public void AddBooking(Booking1DTO booking)
        {
            bookingDAL.AddBooking(booking);
            roomBLL.UpdateRoomStatus(booking.RoomId, "occupied");
        }

        public List<BookingInfoDTO> GetAllBookingsWithDetails()
        {
            return bookingDAL.GetAllBookingsWithDetails();
        }

        public void DeleteBooking(string bookingId)
        {
            BookingInfoDTO bookingInfo = bookingDAL.GetAllBookingsWithDetails().FirstOrDefault(b => b.BookingId == bookingId);
            if (bookingInfo != null)
            {
                roomBLL.UpdateRoomStatus(roomBLL.GetRoomByNumber(bookingInfo.RoomNumber).Id, "available");
                bookingDAL.DeleteBooking(bookingId);
            }
        }

        public string GenerateRentalId()
        {
            string prefix = "PT";
            string timeStamp = DateTime.Now.ToString("yyMMddHHmmss");
            return prefix + timeStamp;
        }
    }
}