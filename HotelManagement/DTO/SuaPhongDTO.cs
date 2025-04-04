namespace HotelManagement
{
    public class SuaPhongDTO
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public int FloorId { get; set; }
        public string Status { get; set; }

        public SuaPhongDTO(int roomId, string roomNumber, int roomTypeId, int floorId, string status)
        {
            RoomId = roomId;
            RoomNumber = roomNumber;
            RoomTypeId = roomTypeId;
            FloorId = floorId;
            Status = status;
        }
    }
}
