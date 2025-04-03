using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement.BLL
{
    public class ThemTaiKhoanBLL
    {
        private ThemTaiKhoanDAL dal = new ThemTaiKhoanDAL();

        public string AddUser(ThemTaiKhoanDTO user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.FullName) ||
                string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.ConfirmPassword))
            {
                return "Vui lòng nhập đầy đủ thông tin!";
            }

            if (user.Password != user.ConfirmPassword)
            {
                return "Mật khẩu nhập lại không khớp!";
            }

            if (dal.CheckUserExists(user)) // Kiểm tra nếu đã tồn tại
            {
                return "Username, số điện thoại hoặc email đã tồn tại!";
            }

            if (dal.InsertUser(user)) // Thêm tài khoản vào DB
            {
                return "Thêm tài khoản thành công!";
            }
            else
            {
                return "Thêm tài khoản thất bại!";
            }
        }
    }
}
