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
            // Lấy dữ liệu từ các textbox
            string username = txt_username.Text;
            string password = txt_password.Text;
            string passwordConfirm = txt_confirmPassword.Text;
            string gmail = txt_gmail.Text;
            string fullName = txt_fullName.Text;
            string address = txt_address.Text;
            string phoneNumber = txt_phoneNumber.Text;

            // Khởi tạo biến sex với giá trị mặc định hoặc kiểm tra nếu không được chọn
            string sex = "";
            if (rdo_nam.Checked)
            {
                sex = "Nam";
            }
            else if (rdo_nu.Checked)
            {
                sex = "Nữ";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return; // Kết thúc hàm, không tiếp tục thực hiện
            }

            // Mặc định giá trị role của khách hàng là 2 (có thể là quyền hạn thấp nhất, ví dụ khách hàng)
            byte role = 2;

            // Prefix để sinh ID cho Account và Customer
            string customerIdPrefix = "CTM_";      // Ví dụ: CTM_ là prefix cho Customer ID

            // Lấy danh sách các ID hiện có để tránh trùng lặp
            List<string> existingCustomerIds = GetExistingCustomerIds();

            Account_service accountService = new Account_service();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(passwordConfirm) ||
                string.IsNullOrWhiteSpace(gmail) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Không được để trống thông tin!"); // Thông báo lỗi
                return; // Kết thúc hàm, không tiếp tục thực hiện
            }

            // Kiểm tra nếu username đã tồn tại
            if (accountService.Account_search_unit(username) != null)
            {
                // Nếu username đã tồn tại, hiển thị thông báo và dừng xử lý
                MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn Username khác.");
                return; // Kết thúc hàm, không tiếp tục thực hiện
            }

            if (password != passwordConfirm)
            {
                // Hiển thị thông báo nếu mật khẩu không khớp
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp! Vui lòng nhập lại.");
                return; // Kết thúc hàm, không tiếp tục thực hiện
            }

            // Tạo thực thể mới cho Account, chuẩn bị lưu vào cơ sở dữ liệu
            Account Account_new = new Account
            {
                Username = username,
                Password = password,
                Role = role // Role mặc định là 2
            };

            try
            {
                // Thêm tài khoản mới vào bảng Account
                int accountResult = accountService.Account_add(Account_new);

                // Nếu việc thêm tài khoản thành công (accountResult == 0)
                if (accountResult == 0)
                {
                    // Sinh ra customer_id mới từ prefix và danh sách ID hiện có
                    string customerId = GenerateNextId(customerIdPrefix, existingCustomerIds);

                    // Tạo thực thể mới cho Customer, chuẩn bị lưu vào cơ sở dữ liệu
                    Customer Customer_new = new Customer
                    {
                        Cus_id = customerId,
                        Full_name = fullName,
                        Sex = sex,
                        Address = address,
                        Email = gmail,
                        Phone_number = phoneNumber,
                        Experience = 0,   // Điểm kinh nghiệm mặc định = 0
                        Username = username,
                        Rank_id = "CP"          // Mặc định rank là CP (có thể là Customer thường)
                    };

                    Customer_service customerService = new Customer_service();
                    // Thêm khách hàng mới vào bảng Customer
                    int customerResult = customerService.Customer_add(Customer_new);

                    // Kiểm tra kết quả thêm customer
                    if (customerResult == 0)
                    {
                        // Nếu thành công, thông báo đăng ký tài khoản thành công
                        MessageBox.Show("Đăng ký tài khoản thành công!");

                        // Ẩn form đăng ký và hiển thị form chính
                        this.Hide();
                        frm_mainPage frm = new frm_mainPage();
                        frm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        // Nếu có lỗi khi thêm khách hàng, hiển thị thông báo lỗi
                        MessageBox.Show("Lỗi trong lúc lưu trữ dữ liệu khách hàng!");
                    }
                }
                else
                {
                    // Kiểm tra nếu tài khoản bị trùng username
                    if (accountResult == -2)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng thay đổi Username.");
                    }
                    else
                    {
                        // Nếu có lỗi khác trong quá trình lưu tài khoản
                        MessageBox.Show("Lỗi trong lúc lưu trữ dữ liệu tài khoản!");
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Bắt lỗi và hiển thị thông báo chi tiết về lỗi validation trong cơ sở dữ liệu
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }


        // Phương thức lấy danh sách ID đã tồn tại của Customer
        private List<string> GetExistingCustomerIds()
        {
            Customer_service customerService = new Customer_service();
            return customerService.Customer_get_all_user_ids(); // Trả về danh sách các Customer_id hiện có
        }

        // Phương thức tự động sinh ID mới không trùng lặp
        private string GenerateNextId(string prefix, List<string> existingIds)
        {
            // Lọc ra các ID có cùng prefix và lấy phần số phía sau
            var filteredIds = existingIds
                .Where(id => id.StartsWith(prefix))
                .Select(id => int.Parse(id.Substring(prefix.Length))) // Chỉ lấy phần số sau prefix
                .ToList();

            // Tìm số ID lớn nhất hiện tại và tăng thêm 1, nếu chưa có ID thì bắt đầu từ 1
            int newIdNumber = filteredIds.Count > 0 ? filteredIds.Max() + 1 : 1;

            // Format số ID thành 4 chữ số (VD: 0001, 0002)
            string formattedNumber = newIdNumber.ToString("D4");

            // Trả về ID mới với prefix và số vừa sinh ra
            return $"{prefix}{formattedNumber}";
        }

        // Event khi nhấn nút hủy
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form đăng ký
            frm_mainPage frm = new frm_mainPage(); // Mở form chính
            frm.ShowDialog();
            this.Close(); // Đóng form đăng ký
        }
    }
}
