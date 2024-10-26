using QLTP.DAL;
//using QLTP.DAL.MetaData_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTP.BLL
{
    public class Bill_detail_service
    {
        // Thêm hóa đơn
        public int Bill_detail_add(Bill_detail bill)
        {
            if (bill == null)
                return -1; // Lỗi null, Lỗi 1
            using (QLTP_Entities db = new QLTP_Entities())
            {
                db.Bill_detail.Add(bill);
                db.SaveChanges();
                return 0; // Thành công
            }
        }
        // Xóa hóa đơn
        public int Bill_detail_delete(string bill_id)
        {
            if (bill_id == null) return -1; // Lỗi bill_id không hợp lệ

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var bill_detail_Delete = db.Bill_detail.FirstOrDefault(b => b.Bill_id == bill_id);
                if (bill_detail_Delete != null)
                {
                    db.Bill_detail.Remove(bill_detail_Delete);
                    db.SaveChanges();
                    return 0; // Thành công
                }
                return -2; // Không tìm thấy hóa đơn
            }
        }
        // Truy xuất tất cả hóa đơn
        public List<Bill_detail> Bill_detail_search_all()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Bill_detail.ToList(); // Trả về danh sách tất cả các hóa đơn
            }
        }

        // Truy xuất hóa đơn theo ID
        public Bill_detail Bill_detail_search_unit(string bill_id)
        {
            if (bill_id == null) return null; // bill_id không hợp lệ

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Bill_detail.FirstOrDefault(b => b.Bill_id == bill_id); // Tìm hóa đơn theo bill_id
            }
        }
    }
}
