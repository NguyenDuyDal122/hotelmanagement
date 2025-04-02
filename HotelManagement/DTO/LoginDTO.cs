namespace HotelManagement.DTO
{
    public class LoginDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public LoginDTO(int id, string username, string role)
        {
            Id = id;
            Username = username;
            Role = role;
        }
    }
}