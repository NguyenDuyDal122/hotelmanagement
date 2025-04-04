using System.Collections.Generic;
using System.Data;

namespace HotelManagement
{
    public class ManageStaffBLL
    {
        private ManageStaffDAL dal = new ManageStaffDAL();

        public List<ManageStaffDTO> GetAllStaff()
        {
            return dal.GetAllStaff();
        }

        public List<ManageStaffDTO> SearchStaff(string username, string phone)
        {
            return dal.SearchStaff(username, phone);
        }

        public bool UpdateStaff(ManageStaffDTO staff)
        {
            return dal.UpdateStaff(staff);
        }

        public void ExportToExcel(string filePath)
        {
            dal.ExportStaffListToExcel(filePath);
        }

        public bool DeleteStaff(int id)
        {
            return dal.DeleteStaff(id);
        }

        public DataTable GetAllStaffsDataTable()
        {
            List<ManageStaffDTO> staffList = dal.GetAllStaff();
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Username");
            dt.Columns.Add("Full Name");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Email");
            dt.Columns.Add("Role");

            foreach (var staff in staffList)
            {
                dt.Rows.Add(staff.Id, staff.Username, staff.FullName, staff.Phone, staff.Email, staff.Role);
            }

            return dt;
        }
    }
}
