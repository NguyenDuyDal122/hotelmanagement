using System.Collections.Generic;

namespace HotelManagement
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
