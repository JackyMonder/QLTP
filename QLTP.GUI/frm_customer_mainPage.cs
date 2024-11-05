using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLTP.BLL;
using QLTP.DAL; // Assuming your BLL is in this namespace

namespace QLTP.GUI
{
    public partial class frm_customer_mainPage : Form
    {
        private readonly Customer_service _customerService;
        private Customer customer;  // Declare the customer object
        //private string user; // Placeholder, use actual customer ID from your login

        public frm_customer_mainPage(string User)
        {
            InitializeComponent();
            _customerService = new Customer_service();
            LoadCustomerData(User);
        }

        private void LoadCustomerData(string User)
        {
            try
            {
                // Fetch and update customer data
                customer = _customerService.GetCustomerByUsername(User);

                if (customer != null)
                {
                    // Update form controls based on customer data
                    lbl_fname.Text = customer.Full_name;
                    lbl_mail.Text = customer.Email;
                    lbl_phoneNumber.Text = customer.Phone_number;
                    lbl_sex.Text = customer.Sex;
                    lbl_address.Text = customer.Address;
                }
                else
                {
                    MessageBox.Show("Customer not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching customer IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //// Fetch and update points/ranking (replace with your BLL logic)
            //int points = _customerService.GetCustomerPoints(CustomerId);
            //ranking.Text = _customerService.GetCustomerRanking(points);
            //progressBar1.Value = points;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
        //    // Handle "Đăng xuất" (Logout) action
        //    // ...
        //    // Example:
        //    DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (result == DialogResult.Yes)
        //    {
        //        this.Close(); // Close the current form
        //        frm_Login loginForm = new frm_Login(); // Assuming frm_Login exists
        //        loginForm.Show(); // Show the login form
        //    }
        }

        private void btn_printRep_Click(object sender, EventArgs e)
        {
            // Handle "In" (Print) action for orders
            // ... 
            // Example:
            // PrintInvoice(currentCustomerId); // Assuming a PrintInvoice method in your BLL or DAL
        }

        private void btn_viewRep_Click(object sender, EventArgs e)
        {
            // Handle "Xem" (View) action for orders
            // ...
            // Example:
            // ViewOrders(currentCustomerId); // Assuming a ViewOrders method in your BLL or DAL
        }

        private void btn_viewCP_Click(object sender, EventArgs e)
        {
            // Handle "Xem" (View) action for vouchers
            // ...
            // Example:
            // ViewVouchers(currentCustomerId); // Assuming a ViewVouchers method in your BLL or DAL
        }

        private void btn_buy_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_customer_BuyingGoods frm = new frm_customer_BuyingGoods(customer.Username);
            frm.ShowDialog();
            this.Close();
        }
    }
}