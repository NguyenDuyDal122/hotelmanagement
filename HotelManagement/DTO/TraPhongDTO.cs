using System;

public class TraPhongDTO
{
    public string Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }     // ✅ Mới
    public int StaffId { get; set; }
    public string StaffName { get; set; }
    public int RoomId { get; set; }
    public string RoomNumber { get; set; }
    public string RoomType { get; set; }          // ✅ Mới
    public DateTime CheckIn { get; set; }
    public decimal TotalPriceService { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal PriceByDay { get; set; }
    public decimal PriceByHour { get; set; }
}