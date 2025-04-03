using System;

namespace HotelManagement
{
    public class SuaKhachHangBLL
    {
        private SuaKhachHangDAL customerDAL = new SuaKhachHangDAL();

        // Lấy dữ liệu khách hàng theo ID
        public ManageCustomerDTO GetCustomerById(int id)
        {
            return customerDAL.GetCustomerById(id);
        }

        // Kiểm tra tính hợp lệ của dữ liệu trước khi cập nhật
        public bool ValidateAndUpdateCustomer(ManageCustomerDTO customer)
        {
            if (string.IsNullOrEmpty(customer.FullName) || string.IsNullOrEmpty(customer.Phone) ||
                string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Address) || string.IsNullOrEmpty(customer.IdentityCard))
            {
                throw new Exception("Vui lòng nhập đầy đủ thông tin!");
            }

            // Kiểm tra xem dữ liệu có trùng lặp không
            if (customerDAL.CheckIfExists(customer))
            {
                throw new Exception("Căn cước công dân, số điện thoại hoặc email đã tồn tại!");
            }

            // Cập nhật dữ liệu khách hàng
            return customerDAL.UpdateCustomer(customer);
        }
    }
}
