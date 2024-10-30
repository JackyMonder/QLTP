﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private Customer customer;
        private int CustomerId = 1; // Placeholder, use actual customer ID from your login

        public frm_customer_mainPage()
        {
            InitializeComponent();
            _customerService = new Customer_service();
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                List<Customer_service> listCustomer = _customerService.Customer_get_all_user_ids().Select(id => new Customer_service { Cus_Id = id }).ToList();

                fname.Text = customer.Full_name;
                mail.Text = customer.Email;
                ph_num.Text = customer.Phone_number;
                sex.Text = customer.Sex;
                address.Text = customer.Address;

                //// Fetch and update points/ranking (replace with your BLL logic)
                //int points = _customerService.GetCustomerPoints(CustomerId);
                //ranking.Text = _customerService.GetCustomerRanking(points);
                //progressBar1.Value = points;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    // Handle "Mua hàng" (Buy) action
        //    frm_ProductSelection productSelection = new frm_ProductSelection(currentCustomerId); // Assuming frm_ProductSelection exists
        //    productSelection.ShowDialog(); // Open product selection form as a dialog
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
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
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            // Handle "In" (Print) action for orders
            // ... 
            // Example:
            // PrintInvoice(currentCustomerId); // Assuming a PrintInvoice method in your BLL or DAL
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Handle "Xem" (View) action for orders
            // ...
            // Example:
            // ViewOrders(currentCustomerId); // Assuming a ViewOrders method in your BLL or DAL
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Handle "Xem" (View) action for vouchers
            // ...
            // Example:
            // ViewVouchers(currentCustomerId); // Assuming a ViewVouchers method in your BLL or DAL
        }
    }
}