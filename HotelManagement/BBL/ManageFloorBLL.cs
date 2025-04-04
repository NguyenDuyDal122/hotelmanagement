using System.Collections.Generic;
using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement.BLL
{
    public class ManageFloorBLL
    {
        private ManageFloorDAL dal = new ManageFloorDAL();

        public List<ManageFloorDTO> GetAllFloors()
        {
            return dal.GetAllFloors();
        }

        public bool AddFloor(ManageFloorDTO floor)
        {
            return dal.AddFloor(floor);
        }

        public bool UpdateFloor(ManageFloorDTO floor)
        {
            return dal.UpdateFloor(floor);
        }

        public bool DeleteFloor(int id)
        {
            return dal.DeleteFloor(id);
        }
    }
}