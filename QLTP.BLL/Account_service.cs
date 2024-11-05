using System;
using System.Collections.Generic;
using System.Linq;
using QLTP.DAL;
using System.Text;
using System.Threading.Tasks;


namespace QLTP.BLL
{
    public class Account_service
    {
        public int Account_add(Account account)
        {
            if (account == null)
                return -1; //Lỗi null, Lỗi 1
            using (QLTP_Entities db = new QLTP_Entities())
            {
                if (db.Account.Any(n => n.Username.Equals(account.Username, StringComparison.OrdinalIgnoreCase)))
                    return -2; //Lỗi user name không tồn tại. Lỗi 2
                db.Account.Add(account);
                db.SaveChanges();
                return 0; // Chạy bthg không lỗi
            }
        }

        public int Account_update(Account account)
        {
            if (account == null) return -1; //lỗi 1

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var account_Update = db.Account.FirstOrDefault(n => n.Username.Equals(account.Username, StringComparison.OrdinalIgnoreCase));
                if (account_Update == null)
                {
                    db.Account.Add(account);
                    return -2; //Lỗi 2
                }
                else
                {
                    account_Update.Password = account.Password;
                    account_Update.Role = account.Role;

                    db.Entry(account_Update).State =
                        System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return 0;
                }
            }
        }
        public int Account_delete(String username)
        {
            if (String.IsNullOrEmpty(username)) return -1;
            using (QLTP_Entities db = new QLTP_Entities())
            {
                var account_Delete = db.Account.FirstOrDefault(n => n.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (account_Delete != null)
                {
                    db.Account.Remove(account_Delete);
                    db.SaveChanges();
                }
                return 0;
            }
        }
        public List<Account> Account_search_all()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Account.ToList();
            }
        }

        public Account Account_search_unit(String username) //tìm theo username
        {
            if (String.IsNullOrEmpty(username)) return null;

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Account.FirstOrDefault(n => n.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false; // Username is invalid

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return !db.Account.Any(n => n.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
