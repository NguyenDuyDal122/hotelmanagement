using HotelManagement.DAL;
using HotelManagement.DTO;
using System;
using System.Collections.Generic;

namespace HotelManagement.BLL
{
    public class TraPhongBLL
    {
        private TraPhongDAL bookingDAL = new TraPhongDAL();

        public List<TraPhongDTO> GetAllBookings()
        {
            return bookingDAL.GetAllBookings();
        }
        public TraPhongDTO GetBookingById(string id)
        {
            return bookingDAL.GetBookingById(id);
        }
        public bool AddInvoice(string bookingId, DateTime checkOut, decimal totalAmount, string paymentMethod)
        {
            return bookingDAL.InsertInvoice(bookingId, checkOut, totalAmount, paymentMethod);
        }
    }
}
