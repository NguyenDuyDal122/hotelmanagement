using System.Collections.Generic;

namespace HotelManagement
{
    public class ManageCustomerBLL
    {
        private ManageCustomerDAL customerDAL = new ManageCustomerDAL();

        // Lấy danh sách khách hàng
        public List<ManageCustomerDTO> GetCustomerList()
        {
            return customerDAL.GetCustomerList();
        }

        // Tìm kiếm khách hàng
        public List<ManageCustomerDTO> SearchCustomers(string email, string phone)
        {
            return customerDAL.SearchCustomers(email, phone);
        }

        // Xóa khách hàng
        public bool DeleteCustomer(int id)
        {
            return customerDAL.DeleteCustomer(id);
        }

        public bool UpdateCustomer(ManageCustomerDTO customer)
        {
            return customerDAL.UpdateCustomer(customer);
        }

        // Xuất Excel
        public void ExportToExcel(string filePath)
        {
            customerDAL.ExportToExcel(filePath);
        }
    }
}
