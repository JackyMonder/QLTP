using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTP.BLL
{
    public class Bill_service
    {
        // Thêm hóa đơn
        public int Bill_add(Bill bill)
        {
            if (bill == null)
                return -1; // Lỗi null, Lỗi 1
            using (QLTP_Entities db = new QLTP_Entities())
            {
                db.Bill.Add(bill);
                db.SaveChanges();
                return 0; // Thành công
            }
        }

        // Cập nhật hóa đơn
        public int Bill_update(Bill bill)
        {
            if (bill == null) return -1; // Lỗi null, Lỗi 1

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var bill_Update = db.Bill.FirstOrDefault(b => b.Bill_id == bill.Bill_id);
                if (bill_Update == null)
                {
                    return -2; // Hóa đơn không tồn tại
                }
                else
                {
                    bill_Update.Create_date = bill.Create_date;
                    bill_Update.Cus_id = bill.Cus_id;
                    bill_Update.Emp_id = bill.Emp_id;

                    db.Entry(bill_Update).State = EntityState.Modified;
                    db.SaveChanges();
                    return 0; // Thành công
                }
            }
        }

        // Xóa hóa đơn
        public int Bill_delete(string bill_id)
        {
            if (bill_id == null) return -1; // Lỗi bill_id không hợp lệ

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var bill_Delete = db.Bill.FirstOrDefault(b => b.Bill_id == bill_id);
                if (bill_Delete != null)
                {
                    db.Bill.Remove(bill_Delete);
                    db.SaveChanges();
                    return 0; // Thành công
                }
                return -2; // Không tìm thấy hóa đơn
            }
        }

        // Truy xuất tất cả hóa đơn
        public List<Bill> Bill_search_all()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Bill.ToList(); // Trả về danh sách tất cả các hóa đơn
            }
        }

        // Truy xuất hóa đơn theo ID
        public Bill Bill_search_unit(string bill_id)
        {
            if (bill_id == null) return null; // bill_id không hợp lệ

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Bill.FirstOrDefault(b => b.Bill_id == bill_id); // Tìm hóa đơn theo bill_id
            }
        }
    }
}