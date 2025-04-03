using System;
using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement.BLL
{
    public class SuaTaiKhoanBLL
    {
        private SuaTaiKhoanDAL dal = new SuaTaiKhoanDAL();

        public SuaTaiKhoanDTO GetUser(int userId)
        {
            return dal.GetUserById(userId);
        }

        public string UpdateUser(SuaTaiKhoanDTO user, string confirmPassword)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.FullName) ||
                string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Email))
            {
                return "Vui lòng nhập đầy đủ thông tin!";
            }

            if (!string.IsNullOrEmpty(user.Password) && user.Password != confirmPassword)
            {
                return "Mật khẩu nhập lại không khớp!";
            }

            if (dal.CheckDuplicate(user))
            {
                return "Username, số điện thoại hoặc email đã tồn tại!";
            }

            if (dal.UpdateUser(user))
            {
                return "Cập nhật tài khoản thành công!";
            }
            else
            {
                return "Cập nhật thất bại!";
            }
        }
    }
}
