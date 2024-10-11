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
        public frm_register()
        {
            InitializeComponent();
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            //Nhân dữ liệu từ textbox đưa vào các biến tạm thời
            string username = txt_username.Text;
            string password = txt_password.Text;
            string passwordConfirm = txt_confirmPassword.Text;
            string gmail = txt_gmail.Text;
            string fullName = txt_fullName.Text;
            string address = txt_address.Text;
            string phoneNumber = txt_phoneNumber.Text;

            // mặc định giá trị role của kh là 2/ quản lý là 1/ admin là 0
            byte role = 2;

            //tự sinh account user_id và customer customer_id 
            string accountIdPrefix = $"AC{role}_"; // AC = Account {role} nghĩa là nhét số 2 vô
            // thành AC2 nghĩa là Account loại 2
            string customerIdPrefix = "CTM_"; // CTM = Customer

            // Lấy về các giá trị đã tồn tại id đã tồn tại trong sql để tránh lập ra các số trùng lập
            List<string> existingAccountIds = GetExistingAccountIds();
            List<string> existingCustomerIds = GetExistingCustomerIds();

            // Tạo user_id mới
            string accountId = GenerateNextId(accountIdPrefix, existingAccountIds); //truyền vào accountIdPrefix
            // là AC{role}_, example: AC2, AC1, AC0, truyền vào số id tiếp theo vào existingAccountIds

            // Tạo thực thể mới thuộc bảng account để lưu trữ dữ liệu
            Account Account_new = new Account
            {
                User_id = accountId,
                Username = username,
                Password = password,
                Role = role 
            };

            Account_service accountService = new Account_service();
            try
            {
                int accountResult = accountService.Account_add(Account_new);

                // Đầu tiền là kiểm tra xem username có bị trùng hay không.
                // nếu kh thì == 0  else thì lỗi um từ bên Account_add rồi :v
                if (accountResult == 0)
                {
                    // Tạo userid mới kh trùng với cái cũ giống như account với prefix và số tự tăng
                    string customerId = GenerateNextId(customerIdPrefix, existingCustomerIds);

                    // tạo thực thể Customer để chứa dữ liệu
                    Customer Customer_new = new Customer
                    {
                        Customer_id = customerId,
                        Full_name = fullName,
                        Address = address,
                        Gmail = gmail,
                        Phone_number = phoneNumber,
                        Experience_point = 0,
                        Level = 1,
                        User_id = accountId,
                        Rank_id = "CP" // set default = CP luôn.
                    };

                    Customer_service customerService = new Customer_service();
                    int customerResult = customerService.Customer_add(Customer_new);

                    if (customerResult == 0)
                    {
                        MessageBox.Show("Đăng ký tài khoản thành công!");
                        this.Hide();
                        frm_mainPage frm = new frm_mainPage();
                        frm.ShowDialog();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi trong lúc lưu trữ dữ liệu khách hàng!");
                    }
                }
                else if (accountResult == -2)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng thay đổi Username.");
                }
                else
                {
                    MessageBox.Show("Lỗi trong lúc lưu trữ dữ liệu tài khoản!");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }

        private List<string> GetExistingAccountIds()
        {
            Account_service accountService = new Account_service();
            return accountService.Account_get_all_user_ids(); // trả về id gần nhất được tạo của account
        }

        private List<string> GetExistingCustomerIds()
        {
            Customer_service customerService = new Customer_service();
            return customerService.Customer_get_all_user_ids(); //trả về id gần nhất đc tạo của customer
        }

        private string GenerateNextId(string prefix, List<string> existingIds)
        {
            // 
            var filteredIds = existingIds
                .Where(id => id.StartsWith(prefix))
                .Select(id => int.Parse(id.Substring(prefix.Length))) // thì ở đây nó đọc id có kèm cái prefix
                .ToList();  //là AC{role}_ hay là CTM_ thì cái này nó chỉ lọc ra lấy 4 con số phía sau thôi

            // đơn giản là tìm ra số gần nhất lần trước rồi + thêm 1 thôi
            int newIdNumber = filteredIds.Count > 0 ? filteredIds.Max() + 1 : 1; //:1 là bắt đầu từ 1 nếu chưa có cái nào hết
            // nghĩa là 0001 là số bắt đầu.

            // format là 4 số 9999 account là dư xăng rồi nhỉ
            string formattedNumber = newIdNumber.ToString("D4");

            // sau khi lấy hết thì ghép lại rồi trả về 1 user_id hoặc customer_id
            // này sửa lại sau thành hóa đơn tự phát cũng đc nè.
            return $"{prefix}{formattedNumber}";
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_mainPage frm = new frm_mainPage();
            frm.ShowDialog();
            this.Close();
        }
    }
}
