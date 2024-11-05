using QLTP.BLL;
using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTP.GUI
{
    public partial class frm_customer_BuyingGoods : Form
    {
        private readonly Product_service _productService;
        private readonly ProductItem_service _productItemService;
        private readonly Product_type_service _productTypeService;
        private readonly Customer_service _customerService;
        private readonly Bill_service _billService;
        private readonly Bill_detail_service _billDetailService;
        private Customer customer;
        private readonly Product product;

        public frm_customer_BuyingGoods(string User)
        {
            InitializeComponent();
            _productService = new Product_service();
            _productItemService = new ProductItem_service();
            _productTypeService = new Product_type_service();
            _customerService = new Customer_service();
            _billService = new Bill_service();
            _billDetailService = new Bill_detail_service();

            loadForm(User);
        }

        private void frm_customer_BuyingGoods_Load(object sender, EventArgs e)
        {
            try
            {
                FillDataDGV(); // Load Products into DataGridView

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FillDataDGV()
        {
            // Update the discount state in the database before displaying
            _productService.UpdateOnDiscountState();

            dgv_quanLySanPham.Rows.Clear(); // Clear existing rows in DataGridView

            var listProduct = _productService.GetAllProducts();

            foreach (var product in listProduct)
            {
                int RowNew = dgv_quanLySanPham.Rows.Add();
                dgv_quanLySanPham.Rows[RowNew].Cells["colTenSP"].Value = _productItemService.GetProductItemName(product.Product_name_id); // Tên SP
                dgv_quanLySanPham.Rows[RowNew].Cells["colLoaiSP"].Value = _productTypeService.GetProductTypeName(product.Product_type_id); // Loại SP
                dgv_quanLySanPham.Rows[RowNew].Cells["colSLTon"].Value = product.Quantity; // Số lượng

                // Calculate the display price based on the `On_discount` status
                double displayPrice = product.On_discount ? product.Sell_Price * 0.9 : product.Sell_Price;
                dgv_quanLySanPham.Rows[RowNew].Cells["colGiaBan"].Value = displayPrice; // Update displayed price

                dgv_quanLySanPham.Rows[RowNew].Cells["colHSD"].Value = product.Expired_day.ToString("yyyy-MM-dd"); // Ngày hết hạn
            }
        }
        private void loadForm(string User)
        {
            txt_SLTon.Clear();
            txt_GiaBan.Clear();
            txt_LoaiSP.Clear();
            txt_TenSP.Clear();
            // Fetch and update customer data
            customer = _customerService.GetCustomerByUsername(User);
            txt_TenKH.Text = customer.Full_name;
            txt_DiaChi.Text = customer.Address;
            txt_SDT.Text = customer.Phone_number;
        }

        private bool CheckDataInput()
        {
            if (string.IsNullOrWhiteSpace(txt_LoaiSP.Text) ||
                string.IsNullOrWhiteSpace(txt_TenSP.Text) ||
                string.IsNullOrWhiteSpace(txt_SLTon.Text) ||
                string.IsNullOrWhiteSpace(txt_GiaBan.Text))
            {
                MessageBox.Show("Bạn chưa nhập đúng thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(txt_SLTon.Text, out _))
            {
                MessageBox.Show("Số lượng sản phẩm không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!double.TryParse(txt_GiaBan.Text, out _))
            {
                MessageBox.Show("Giá bán không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void txtTimTenSP_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtTimTenSP.Text.Trim();

            // Call the product item service to get product items matching the search term
            var filteredProductItems = _productItemService.SearchProductsByName(searchTerm);

            // Clear existing rows
            dgv_quanLySanPham.Rows.Clear();

            // Loop through the filtered product items to get corresponding products
            foreach (var productItem in filteredProductItems)
            {
                // Find the product associated with the product item
                var product = _productService.GetAllProducts().FirstOrDefault(p => p.Product_name_id == productItem.Product_name_id);

                if (product != null) // Ensure the product exists
                {
                    int rowNew = dgv_quanLySanPham.Rows.Add();
                    dgv_quanLySanPham.Rows[rowNew].Cells["colTenSP"].Value = productItem.Product_name; // Set the product name from Product_Item
                    dgv_quanLySanPham.Rows[rowNew].Cells["colLoaiSP"].Value = _productTypeService.GetProductTypeName(product.Product_type_id);
                    dgv_quanLySanPham.Rows[rowNew].Cells["colSLTon"].Value = product.Quantity;
                    dgv_quanLySanPham.Rows[rowNew].Cells["colGiaBan"].Value = product.Sell_Price;
                    dgv_quanLySanPham.Rows[rowNew].Cells["colHSD"].Value = product.Expired_day.ToString("dd/MM/yyyy");
                }
            }
        }

        private void dgv_quanLySanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GetGoods(e);
        }

        private void dgv_quanlySanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GetGoods(e);
            Cart_Add();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            CheckDataInput();
            Cart_Add();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            dgv_chiTietHD.Rows.Clear();
            txt_SLDat.Clear();
            txt_TongTien.Clear();
        }

        private void Cart_Add()
        {
            // Lấy thông tin sản phẩm từ các textbox
            string productName = txt_TenSP.Text;
            int quantityInStock = int.Parse(txt_SLTon.Text);
            double sellPrice = double.Parse(txt_GiaBan.Text);

            // Kiểm tra xem đã nhập số lượng đặt hàng hay chưa
            if (!int.TryParse(txt_SLDat.Text, out int quantityOrdered) || quantityOrdered <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng đặt hàng!", "Thông báo");
                return;
            }

            // Kiểm tra số lượng đặt hàng có vượt quá số lượng tồn kho hay không
            if (quantityOrdered > quantityInStock)
            {
                MessageBox.Show("Số lượng đặt hàng vượt quá số lượng tồn kho!", "Thông báo");
                return;
            }

            // Tìm kiếm sản phẩm trong dgv_chiTietHD
            int rowIndex = -1;
            for (int i = 0; i < dgv_chiTietHD.Rows.Count; i++)
            {
                if (dgv_chiTietHD.Rows[i].Cells["col_TenSP"].Value != null &&
                    dgv_chiTietHD.Rows[i].Cells["col_TenSP"].Value.ToString() == productName)
                {
                    rowIndex = i;
                    break;
                } 
                else if (product.Product_id != )
            }

            // Nếu sản phẩm đã có trong dgv_chiTietHD, cập nhật số lượng mua
            if (rowIndex >= 0)
            {
                int currentQuantity = int.Parse(dgv_chiTietHD.Rows[rowIndex].Cells["col_SLMua"].Value.ToString());

                if ((currentQuantity + quantityOrdered) > quantityInStock)
                {
                    MessageBox.Show("Số lượng đặt hàng đã vượt quá số lượng tồn kho!", "Thông báo");
                    return;
                }
                else
                    dgv_chiTietHD.Rows[rowIndex].Cells["col_SLMua"].Value = currentQuantity + quantityOrdered;
            }
            else // Nếu sản phẩm chưa có trong dgv_chiTietHD, thêm mới
            {
                // Tạo dòng mới trong dgv_chiTietHD
                int rowNew = dgv_chiTietHD.Rows.Add();
                // Thêm thông tin sản phẩm vào dòng mới
                dgv_chiTietHD.Rows[rowNew].Cells["col_TenSP"].Value = productName;
                dgv_chiTietHD.Rows[rowNew].Cells["col_SLMua"].Value = quantityOrdered;
                dgv_chiTietHD.Rows[rowNew].Cells["col_DonGia"].Value = sellPrice;

                // Kiểm tra hạn sử dụng
                if ((product.Expired_day - DateTime.Now).TotalDays < 3)
                {
                    dgv_chiTietHD.Rows[rowNew].Cells["col_GiamGia"].Value = "10%";
                }
                else
                {
                    dgv_chiTietHD.Rows[rowNew].Cells["col_GiamGia"].Value = "0%";
                }

                // Các cột còn lại (Giảm giá, Thành tiền) có thể được tính toán dựa trên số lượng mua và đơn giá
                dgv_chiTietHD.Rows[rowNew].Cells["col_ThanhTien"].Value = sellPrice * quantityOrdered;

                // Cập nhật tổng tiền
                UpdateTotalMoney();
            }
            // Xóa dữ liệu trong textbox SL đặt
            txt_SLDat.Clear();
        }

        private void GetGoods(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy ID sản phẩm từ cột "colTenSP" (giả sử ID sản phẩm được lưu trữ trong cột này)
                string productName = dgv_quanLySanPham.Rows[e.RowIndex].Cells["colTenSP"].Value.ToString();

                // Tìm sản phẩm tương ứng
                Product product = _productService.GetProductByName(productName);

                // Kiểm tra sản phẩm có tồn tại hay không
                if (product != null)
                {
                    // Cập nhật thông tin sản phẩm vào các textbox
                    txt_TenSP.Text = productName;
                    txt_LoaiSP.Text = _productTypeService.GetProductTypeName(product.Product_type_id);
                    txt_SLTon.Text = product.Quantity.ToString();
                    txt_GiaBan.Text = product.Sell_Price.ToString();
                    dtpHSD.Value = product.Expired_day;
                }
            }
        }

        private void UpdateTotalMoney()
        {
            double totalMoney = 0;

            // Duyệt qua từng dòng trong dgv_chiTietHD
            for (int i = 0; i < dgv_chiTietHD.Rows.Count; i++)
            {
                // Tính tổng tiền cho mỗi dòng
                if (dgv_chiTietHD.Rows[i].Cells["col_DonGia"].Value != null)
                {
                    double price = double.Parse(dgv_chiTietHD.Rows[i].Cells["col_ThanhTien"].Value.ToString());
                    totalMoney += price;
                }
            }

            // Cập nhật tổng tiền vào textbox
            txt_TongTien.Text = totalMoney.ToString();
        }
    }
}
