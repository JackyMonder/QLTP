using Microsoft.Data.SqlClient;
using QLTP.BLL;
using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace QLTP.GUI
{
    public partial class frm_managerAccount : Form
    {
        private readonly Customer_service _customerService;
        private readonly Employee_service _employeeService;
        private readonly Rank_service _rankService;
        //private readonly Account_service _accountService;
        private BindingSource _bs; // Khai báo BindingSource ở đây

        public frm_managerAccount()
        {
            InitializeComponent();
            _customerService = new Customer_service();
            _employeeService = new Employee_service();
            _rankService = new Rank_service();
            //_accountService = new Account_service();  // Khởi tạo _accountService

            // Thêm sự kiện SelectedIndexChanged một lần
            cbo_Accounts.SelectedIndexChanged += cbo_Accounts_SelectedIndexChanged;
            FillRoleNameCBB();
        }

        public class RoleItem
        {
            public string Value { get; set; }
            public byte Id { get; set; } // Thêm thuộc tính Id cho mỗi RoleItem
        }

        private void frm_managerAccount_Load(object sender, EventArgs e)
        {
            try
            {
                FillRoleNameCBB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbo_Accounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_Accounts.SelectedItem is RoleItem selectedRole)
            {
                FillDataDGV(selectedRole.Id); // Lấy Id từ RoleItem
            }
        }

        private void FillRoleNameCBB()
        {
            // Tạo list các object RoleItem
            List<RoleItem> roles = new List<RoleItem>()
            {
                new RoleItem { Value = "Customers", Id = 1 },
                new RoleItem { Value = "Employees", Id = 2 }
            };

            // Sử dụng DataSource để hiển thị các phần tử
            cbo_Accounts.DataSource = roles;
            cbo_Accounts.DisplayMember = "Value";

            // Thiết lập giá trị mặc định cho ComboBox
            cbo_Accounts.SelectedIndex = 0; // Chọn "Customer" làm mặc định
        }


        private void FillDataDGV(byte role)
        {
            GetCustomersOrEmployeesByRole(role);
        }

        private void GetCustomersOrEmployeesByRole(byte role)
        {
            try
            {
                _bs = new BindingSource();

                using (QLTP_Entities db = new QLTP_Entities())
                {
                    if (role == 1) // Lấy dữ liệu cho Customer
                    {
                        // Lấy dữ liệu từ service
                        var listCustomer = db.Customer.ToList();

                        // Gán danh sách dữ liệu cho BindingSource
                        _bs.DataSource = listCustomer;

                        // Hiển thị các cột cho Customer
                        dgv_quanLyTaiKhoan.Columns["colMaKH"].Visible = true;
                        dgv_quanLyTaiKhoan.Columns["colTenKH"].Visible = true;
                        dgv_quanLyTaiKhoan.Columns["colCapBac"].Visible = true;
                        dgv_quanLyTaiKhoan.Columns["colDiemTichLuy"].Visible = true;
                        dgv_quanLyTaiKhoan.Columns["colMaNV"].Visible = false;
                        dgv_quanLyTaiKhoan.Columns["colTenNV"].Visible = false;
                        dgv_quanLyTaiKhoan.Columns["colSal"].Visible = false;
                        dgv_quanLyTaiKhoan.Columns["colPos"].Visible = false;

                        foreach (var customer in listCustomer)
                        {
                            int rowNew = dgv_quanLyTaiKhoan.Rows.Add();

                            // Assign values to the specific columns by name
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colMaKH"].Value = customer.Cus_id;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colTenKH"].Value = customer.Full_name;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colGT"].Value = customer.Sex;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colSDT"].Value = customer.Phone_number;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colDiaChi"].Value = customer.Address;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colEmail"].Value = customer.Email; // Cấp bậc
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colCapBac"].Value = _rankService.GetRankName(customer.Rank_id); // Tích lũy
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colDiemTichLuy"].Value = customer.Experience;
                        }
                    }
                    else if (role == 2) // Lấy dữ liệu cho Employee
                    {
                        // Lấy dữ liệu từ service
                        var listEmployee = db.Employee.ToList();

                        // Gán danh sách dữ liệu cho BindingSource
                        _bs.DataSource = listEmployee;

                        // Hiển thị các cột cho Employee
                        dgv_quanLyTaiKhoan.Columns["colMaKH"].Visible = false;
                        dgv_quanLyTaiKhoan.Columns["colTenKH"].Visible = false;
                        dgv_quanLyTaiKhoan.Columns["colCapBac"].Visible = false;
                        dgv_quanLyTaiKhoan.Columns["colDiemTichLuy"].Visible = false;
                        dgv_quanLyTaiKhoan.Columns["colMaNV"].Visible = true;
                        dgv_quanLyTaiKhoan.Columns["colTenNV"].Visible = true;
                        dgv_quanLyTaiKhoan.Columns["colSal"].Visible = true;
                        dgv_quanLyTaiKhoan.Columns["colPos"].Visible = true;

                        foreach (var employee in listEmployee)
                        {
                            int rowNew = dgv_quanLyTaiKhoan.Rows.Add();

                            // Assign values to the specific columns by name
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colMaNV"].Value = employee.Emp_id;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colTenNV"].Value = employee.Full_name;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colGT"].Value = employee.Sex;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colSDT"].Value = employee.Phone_number;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colDiaChi"].Value = employee.Address;
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colEmail"].Value = employee.Email; // Cấp bậc
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colPos"].Value = employee.Position; // Tích lũy
                            dgv_quanLyTaiKhoan.Rows[rowNew].Cells["colSal"].Value = employee.Salary;
                        }

                    }
                    dgv_quanLyTaiKhoan.DataSource = _bs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}