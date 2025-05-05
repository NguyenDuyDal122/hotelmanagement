using System;

namespace DTO
{
    public class ThongKeDTO
    {
        public int InvoiceId { get; set; }
        public int StaffId { get; set; }
        public string CustomerName { get; set; }
        public string RoomNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }

        public ThongKeDTO(int invoiceId, int staffId, string customerName, string roomNumber, DateTime paymentDate, decimal totalAmount, string paymentMethod)
        {
            InvoiceId = invoiceId;
            StaffId = staffId;
            CustomerName = customerName;
            RoomNumber = roomNumber;
            PaymentDate = paymentDate;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
        }
    }
}
