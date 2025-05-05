using System.Collections.Generic;
using DAL;
using DTO;

namespace BBL
{
    public class ThongKeBLL
    {
        private ThongKeDAL dal = new ThongKeDAL();

        public List<ThongKeDTO> GetInvoicesByMonth(int month)
        {
            return dal.GetInvoiceListByMonth(month);
        }
    }
}