using QLTP.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTP.GUI
{
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string username = txt_Username.Text.Trim();
            string password = txt_Password.Text.Trim();

            if (username == String.Empty)
            {
                MessageBox.Show("Username không được để trống");
                return;
            }
            else if (password == String.Empty)
            {
                MessageBox.Show("Password không được để trống");
                return;
            }

            Account_service account_service = new Account_service();
            var Account = account_service.Account_search_unit(username);
            if (Account == null)
            {
                MessageBox.Show("Tài khoản không tồn tại!");
                return;
            }
            if (Account.Password != password)
            {
                MessageBox.Show("Password không đúng!");
                return;
            }

            //
            //role 0 là admin   
            //

            if (Account.Role == 1) //role 1 quản lý cửa hàng
            {
                this.Hide();
                frm_manager_mainPage frm = new frm_manager_mainPage();
                frm.ShowDialog();
                this.Close();
            }
            else if (Account.Role == 2) //role 2 = khách hàng 
            {
                this.Hide();
                frm_customer_mainPage frm = new frm_customer_mainPage(username);
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                // các số còn lại là khách vãng lai
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_mainPage frm = new frm_mainPage();
            frm.ShowDialog();
            this.Close();
        }
    }
}
