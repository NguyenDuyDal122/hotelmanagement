namespace HotelManagement
{
    public class ThemPhongBLL
    {
        private ThemPhongDAL roomDAL = new ThemPhongDAL();

        public bool IsRoomNumberExists(string roomNumber)
        {
            return roomDAL.IsRoomNumberExists(roomNumber);
        }

        public bool IsRoomLimitReached(int floorId)
        {
            return roomDAL.IsRoomLimitReached(floorId);
        }

        public bool AddRoom(ThemPhongDTO roomDTO)
        {
            return roomDAL.AddRoom(roomDTO);
        }
    }
}