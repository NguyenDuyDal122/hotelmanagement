using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement.BLL
{
    public class ThongTinCaNhanBLL
    {
        private ThongTinCaNhanDAL thongTinDAL = new ThongTinCaNhanDAL();

        public ThongTinCaNhanDTO GetUserInfo(int userId)
        {
            return thongTinDAL.GetUserInfo(userId);
        }
    }
}
