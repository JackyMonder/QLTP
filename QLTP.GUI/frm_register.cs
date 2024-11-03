using QLTP.BLL;
using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QLTP.GUI
{
    public partial class frm_register : Form
    {
        private Account_service accountService; // Changed from Account_service to AccountService
        private Customer_service customerService; // Changed from Customer_service to CustomerService

        public frm_register()
        {
            InitializeComponent();
            accountService = new Account_service(); // Initialize the service
            customerService = new Customer_service(); // Initialize the service
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            // Retrieve data from text boxes
            string username = txt_username.Text;
            string password = txt_password.Text;
            string passwordConfirm = txt_confirmPassword.Text;
            string gmail = txt_gmail.Text;
            string fullName = txt_fullName.Text;
            string address = txt_address.Text;
            string phoneNumber = txt_phoneNumber.Text;

            // Default sex value or check if not selected
            string sex = rdo_nam.Checked ? "Nam" : rdo_nu.Checked ? "Nữ" : string.Empty;
            if (string.IsNullOrEmpty(sex))
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return; // Exit the method if sex is not selected
            }

            // Default role for customer
            byte role = 2;

            // Validate input information
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(passwordConfirm) ||
                string.IsNullOrWhiteSpace(gmail) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Không được để trống thông tin!"); // Show error message
                return; // Exit the method if input is invalid
            }

            // Check if username is available through BLL
            if (!accountService.IsUsernameAvailable(username))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn Username khác.");
                return; // Exit the method if username is not available
            }

            // Check if passwords match
            if (password != passwordConfirm)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp! Vui lòng nhập lại.");
                return; // Exit the method if passwords do not match
            }

            try
            {
                Account newAccount = new Account
                {
                    Username = username,
                    Password = password,
                    Role = role // Default role is 2
                };

                // Add new account to Account table through BLL
                var accountResult = accountService.Account_add(newAccount);

                if (accountResult == 0)
                {
                    // Generate new customer_id from BLL
                    string customerId = customerService.GenerateNewCustomerId();

                    // Create a new Customer entity to save in the database
                    Customer newCustomer = new Customer
                    {
                        Cus_id = customerId,
                        Full_name = fullName,
                        Sex = sex,
                        Address = address,
                        Email = gmail,
                        Phone_number = phoneNumber,
                        Experience = 0,   // Default experience point = 0
                        Username = username,
                        Rank_id = "CP"    // Default rank is CP
                    };

                    // Add new customer to Customer table through BLL
                    var customerResult = customerService.Customer_add(newCustomer);

                    if (customerResult == 0)
                    {
                        // If successful, notify account registration success
                        MessageBox.Show("Đăng ký tài khoản thành công!");

                        // Hide register form and show main form
                        this.Hide();
                        frm_mainPage frm = new frm_mainPage();
                        frm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        // If there is an error adding customer, show error message
                        MessageBox.Show("Lỗi trong lúc lưu trữ dữ liệu khách hàng!");
                    }
                }
                else
                {
                    // Handle errors in adding account
                    HandleAccountAddError(accountResult);
                }
            }
            catch (Exception ex) // Catch general exceptions
            {
                MessageBox.Show($"Lỗi trong quá trình đăng ký: {ex.Message}");
            }
        }

        // Handle error when adding account
        private void HandleAccountAddError(int accountResult)
        {
            switch (accountResult)
            {
                case -2:
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng thay đổi Username.");
                    break;
                default:
                    MessageBox.Show("Lỗi trong lúc lưu trữ dữ liệu tài khoản!");
                    break;
            }
        }

        // Event when cancel button is clicked
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide registration form
            frm_mainPage frm = new frm_mainPage(); // Open main form
            frm.ShowDialog();
            this.Close(); // Close registration form
        }
    }
}
