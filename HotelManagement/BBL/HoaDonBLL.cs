using HotelManagement.DTO;
using HotelManagement.DAL;
using System.Collections.Generic;

namespace HotelManagement.BLL
{
    public class HoaDonBLL
    {
        private HoaDonDAL dal = new HoaDonDAL();

        public List<HoaDonDTO> GetAllHoaDon()
        {
            return dal.GetAllHoaDon();
        }
        public List<HoaDonDTO> SearchHoaDonByBookingID(string bookingID)
        {
            return dal.SearchHoaDonByBookingID(bookingID);
        }
        public bool DeleteHoaDon(int invoiceID)
        {
            return dal.DeleteHoaDon(invoiceID);
        }
    }
}