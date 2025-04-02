namespace HotelManagement.BLL
{
    using HotelManagement.DAL;
    using HotelManagement.DTO;

    public class DoiMatKhauBLL
    {
        private DoiMatKhauDAL userDAL;

        public DoiMatKhauBLL()
        {
            userDAL = new DoiMatKhauDAL();
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                return false; // Mật khẩu mới không khớp
            }

            DoiMatKhauDTO user = new DoiMatKhauDTO(userId, oldPassword);

            // Kiểm tra mật khẩu cũ
            if (!userDAL.CheckOldPassword(user, oldPassword))
            {
                return false; // Mật khẩu cũ không đúng
            }

            // Cập nhật mật khẩu mới
            return userDAL.UpdatePassword(user, newPassword);
        }
    }
}
