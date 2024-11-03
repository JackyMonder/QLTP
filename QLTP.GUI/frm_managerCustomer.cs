using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLTP.BLL;
using QLTP.DAL;

namespace QLTP.GUI
{
    public partial class frm_managerCustomer : Form
    {
        private readonly Customer_service _customerService;
        private readonly Rank_service _rankService;

        public frm_managerCustomer()
        {
            InitializeComponent();
            _customerService = new Customer_service();
            _rankService = new Rank_service();
        }

        private void frm_managerCustomer_Load(object sender, EventArgs e)
        {
            try
            {

                // Fill DataGridView with Customers
                FillDataDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillDataDGV()
        {
            // Clear existing rows
            dgv_quanLyKhachHang.Rows.Clear();

            // Get all customers using the service
            var listCustomer = _customerService.Customer_search_all();

            foreach (var customer in listCustomer)
            {
                int rowNew = dgv_quanLyKhachHang.Rows.Add();

                // Assign values to the specific columns by name
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colMaKH"].Value = customer.Cus_id;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colTenKH"].Value = customer.Full_name;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colGT"].Value = customer.Sex;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colSDT"].Value = customer.Phone_number;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colDiaChi"].Value = customer.Address;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colEmail"].Value = customer.Email; // Cấp bậc
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colCapBac"].Value = _rankService.GetRankName(customer.Rank_id); // Tích lũy
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colDiemTichLuy"].Value = customer.Experience;
            }
        }

        private void loadForm()
        {
            txt_TenKH.Clear();
            txt_SDT.Clear();
            txt_DiaChi.Clear();
            txt_Email.Clear();
            txt_TichLuy.Clear();
            rdo_Nam.Checked = true;
        }

        private bool CheckDataInput()
        {
            if (string.IsNullOrWhiteSpace(txt_TenKH.Text) ||
                string.IsNullOrWhiteSpace(txt_SDT.Text) ||
                string.IsNullOrWhiteSpace(txt_DiaChi.Text) ||
                string.IsNullOrWhiteSpace(txt_Email.Text) ||
                string.IsNullOrWhiteSpace(txt_CapBac.Text))
            {
                MessageBox.Show("Bạn chưa nhập đúng thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(txt_SDT.Text, out _))
            {
                MessageBox.Show("Số điện thoại không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (CheckDataInput())
            {
                // Validate Experience input if it's required, otherwise you can skip this validation
                if (!float.TryParse(txt_TichLuy.Text, out float experience))
                {
                    MessageBox.Show("Tích lũy không hợp lệ. Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if parsing fails
                }

                // Create a Customer object and populate it directly from the form inputs
                var customerToUpdate = new Customer
                {
                    Cus_id = txt_MaKH.Text.Trim(),
                    Full_name = txt_TenKH.Text.Trim(),
                    Sex = rdo_Nam.Checked ? "Nam" : "Nữ",
                    Phone_number = txt_SDT.Text.Trim(),
                    Address = txt_DiaChi.Text.Trim(),
                    Email = txt_Email.Text.Trim(),
                    Rank_id = txt_CapBac.Text.Trim() // Safely get the rank ID
                                                     // Remove Experience field as it's not supposed to be updated
                };

                // Call the service to update the customer
                var result = _customerService.Customer_update(customerToUpdate);

                // Check the result of the update operation
                switch (result)
                {
                    case 0:
                        // Success
                        FillDataDGV();
                        loadForm();
                        MessageBox.Show("Sửa đổi dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case -1:
                        // Error: null customer object
                        MessageBox.Show("Thông tin khách hàng không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case -2:
                        // Error: customer not found
                        MessageBox.Show("Khách hàng không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        // Handle any other cases (if you decide to define more)
                        MessageBox.Show("Có lỗi xảy ra khi cập nhật khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin nhập vào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string customerId = txt_MaKH.Text.Trim();
            if (string.IsNullOrEmpty(customerId))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int result = _customerService.Customer_delete(customerId);
            switch (result)
            {
                case 0:
                    MessageBox.Show("Xóa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_MaKH.Clear(); // Use Clear for better readability
                    FillDataDGV();
                    break;
                case -1:
                    MessageBox.Show("Mã khách hàng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -2:
                    MessageBox.Show("Không tìm thấy khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("Đã xảy ra lỗi bất ngờ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (frm_manager_mainPage frm = new frm_manager_mainPage())
            {
                frm.ShowDialog();
            }
        }

        private void dgv_quanLyKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked cell is valid
                if (e.RowIndex >= 0 && dgv_quanLyKhachHang.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewRow selectedRow = dgv_quanLyKhachHang.Rows[e.RowIndex];

                    // Assign values from the selected row to the input fields
                    txt_MaKH.Text = selectedRow.Cells["colMaKH"].FormattedValue.ToString();
                    txt_TenKH.Text = selectedRow.Cells["colTenKH"].FormattedValue.ToString();
                    rdo_Nam.Checked = selectedRow.Cells["colGT"].FormattedValue.ToString() == "Nam";
                    rdo_Nu.Checked = !rdo_Nam.Checked;
                    txt_SDT.Text = selectedRow.Cells["colSDT"].FormattedValue.ToString();
                    txt_DiaChi.Text = selectedRow.Cells["colDiaChi"].FormattedValue.ToString();
                    txt_Email.Text = selectedRow.Cells["colEmail"].FormattedValue.ToString();
                    txt_CapBac.Text = selectedRow.Cells["colCapBac"].FormattedValue.ToString();
                    txt_TichLuy.Text = selectedRow.Cells["colDiemTichLuy"].FormattedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hãy thử lại! " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtTimTenKH_TextChanged(object sender, EventArgs e)
        {
            // Get the search term from the text box
            string searchTerm = txtTimTenKH.Text.Trim();

            // Call the customer service to search for customers by full name
            var filteredCustomers = _customerService.Customer_search_by_full_name(searchTerm);

            // Clear existing rows
            dgv_quanLyKhachHang.Rows.Clear();

            // Populate the DataGridView with the filtered customer data
            foreach (var customer in filteredCustomers)
            {
                int rowNew = dgv_quanLyKhachHang.Rows.Add();
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colMaKH"].Value = customer.Cus_id;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colTenKH"].Value = customer.Full_name;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colGT"].Value = customer.Sex;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colSDT"].Value = customer.Phone_number;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colDiaChi"].Value = customer.Address;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colEmail"].Value = customer.Email;
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colCapBac"].Value = _rankService.GetRankName(customer.Rank_id);
                dgv_quanLyKhachHang.Rows[rowNew].Cells["colDiemTichLuy"].Value = customer.Experience;
            }
        }
    }
}
