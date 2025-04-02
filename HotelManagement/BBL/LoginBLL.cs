using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement.BLL
{
    public class LoginBLL
    {
        private LoginDAL loginDAL;

        public LoginBLL()
        {
            loginDAL = new LoginDAL();
        }

        public LoginDTO AuthenticateUser(string username, string password)
        {
            return loginDAL.GetUser(username, password);
        }
    }
}