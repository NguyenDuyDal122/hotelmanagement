namespace HotelManagement
{
    public class ThemPhongDTO
    {
        public string RoomNumber { get; set; }
        public int TypeId { get; set; }
        public int FloorId { get; set; }
        public string Status { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PricePerHour { get; set; }
    }
}