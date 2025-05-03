using System;

namespace HotelManagement.DTO
{
    public class HoaDonDTO
    {
        public int InvoiceID { get; set; }
        public string BookingID { get; set; }
        public string CustomerName { get; set; }
        public string StaffName { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalService { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}