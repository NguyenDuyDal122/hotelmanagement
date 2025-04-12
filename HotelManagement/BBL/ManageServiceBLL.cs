using HotelManagement.DAL;
using HotelManagement.DTO;
using System.Collections.Generic;

namespace HotelManagement
{
    public class ServiceBLL
    {
        private ServiceDAL dal = new ServiceDAL();

        public List<ServiceDTO> GetAllServices()
        {
            return dal.GetAllServices();
        }

        public bool AddService(ServiceDTO service)
        {
            return dal.InsertService(service);
        }

        public bool UpdateService(ServiceDTO service)
        {
            return dal.UpdateService(service);
        }

        public bool DeleteService(int id)
        {
            return dal.DeleteService(id);
        }
    }
}
