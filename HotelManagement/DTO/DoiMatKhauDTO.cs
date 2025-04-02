namespace HotelManagement.DTO
{
    public class DoiMatKhauDTO
    {
        public int Id { get; set; }
        public string PasswordHash { get; set; }

        public DoiMatKhauDTO(int id, string passwordHash)
        {
            Id = id;
            PasswordHash = passwordHash;
        }
    }
}
