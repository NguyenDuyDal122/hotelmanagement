using System;

namespace HotelManagement
{
    public class ThemKhachHangBLL
    {
        private ThemKhachHangDAL customerDAL = new ThemKhachHangDAL();

        // Kiểm tra dữ liệu trước khi thêm
        public bool ValidateAndAddCustomer(ThemKhachHangDTO customer)
        {
            // Kiểm tra xem dữ liệu có hợp lệ không
            if (string.IsNullOrEmpty(customer.FullName) || string.IsNullOrEmpty(customer.Phone) || string.IsNullOrEmpty(customer.Email) ||
                string.IsNullOrEmpty(customer.Address) || string.IsNullOrEmpty(customer.IdentityCard))
            {
                throw new Exception("Vui lòng nhập đầy đủ thông tin!");
            }

            // Kiểm tra xem dữ liệu đã tồn tại trong cơ sở dữ liệu
            if (customerDAL.CheckIfExists(customer.Phone, customer.Email, customer.IdentityCard))
            {
                throw new Exception("Căn cước công dân, số điện thoại hoặc email đã tồn tại!");
            }

            // Thêm khách hàng vào cơ sở dữ liệu
            return customerDAL.AddCustomer(customer);
        }
    }
}
