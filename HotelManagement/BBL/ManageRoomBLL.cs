using System;
using System.Data;

namespace HotelManagement
{
    public class ManageRoomBLL
    {
        private ManageRoomDAL roomDAL = new ManageRoomDAL();

        public DataTable GetRoomData()
        {
            return roomDAL.GetRoomData();
        }

        public DataTable SearchRoom(string roomNumber)
        {
            return roomDAL.SearchRoom(roomNumber);
        }

        public bool DeleteRoom(int roomId)
        {
            return roomDAL.DeleteRoom(roomId);
        }

        public void ExportToExcel(DataTable dt, string filePath)
        {
            roomDAL.ExportToExcel(dt, filePath);
        }
    }
}