namespace HotelManagement
{
    public class ManageRoomDTO
    {
        public int Id { get; set; }                    // Mã phòng
        public string RoomNumber { get; set; }         // Số phòng
        public string RoomName { get; set; }           // Tên phòng (nếu có)
        public int FloorId { get; set; }               // Mã tầng (nếu phân tầng)
        public string RoomType { get; set; }           // Loại phòng (VIP, Standard, v.v.)
        public decimal PricePerDay { get; set; }       // Giá theo ngày
        public decimal PricePerHour { get; set; }      // Giá theo giờ
        public bool IsAvailable { get; set; }          // Còn trống không
        public string Description { get; set; }        // Mô tả phòng
        public string ImagePath { get; set; }          // Đường dẫn ảnh (nếu có)
    }
}