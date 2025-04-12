namespace HotelManagement
{
    public class SuaPhongDTO
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public int FloorId { get; set; }
        public string Status { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PricePerHour { get; set; }

        public SuaPhongDTO(int roomId, string roomNumber, int roomTypeId, int floorId, string status, decimal pricePerDay, decimal pricePerHour)
        {
            RoomId = roomId;
            RoomNumber = roomNumber;
            RoomTypeId = roomTypeId;
            FloorId = floorId;
            Status = status;
            PricePerDay = pricePerDay;
            PricePerHour = pricePerHour;
        }
    }
}
