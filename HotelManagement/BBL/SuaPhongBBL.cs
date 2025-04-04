using System.Data;

namespace HotelManagement
{
    public class SuaPhongBLL
    {
        private SuaPhongDAL roomDAL = new SuaPhongDAL();

        public DataTable LoadRoomTypes()
        {
            return roomDAL.GetRoomTypes();
        }

        public DataTable LoadFloors()
        {
            return roomDAL.GetFloors();
        }

        public SuaPhongDTO LoadRoomDetails(int roomId)
        {
            return roomDAL.GetRoomDetails(roomId);
        }

        public bool CheckRoomNumberExists(int roomId, string roomNumber)
        {
            return roomDAL.CheckRoomNumberExists(roomId, roomNumber);
        }

        public bool UpdateRoom(SuaPhongDTO room)
        {
            return roomDAL.UpdateRoom(room);
        }
        public bool IsFloorFull(int floorId, int excludeRoomId)
        {
            return roomDAL.IsFloorFull(floorId, excludeRoomId);
        }
    }
}
