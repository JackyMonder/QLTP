using QLTP.BLL; // Assuming you have a BLL layer for services
using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QLTP.GUI
{
    public partial class frm_managerProduct : Form
    {
        private readonly Product_service _productService; // Service for product operations
        private readonly ProductItem_service _productItemService; // Service for product item operations
        private readonly Product_type_service _productTypeService; // Service for product type operations

        public frm_managerProduct()
        {
            InitializeComponent();
            _productService = new Product_service();
            _productItemService = new ProductItem_service();
            _productTypeService = new Product_type_service();
        }

        private void frm_managerProduct_Load(object sender, EventArgs e)
        {
            try
            {
                // Load Product Types and Products using services
                List<Product_type> listProductType = _productTypeService.GetAllProductTypes();
                List<Product> listProduct = _productService.GetAllProducts();

                FillDataCBB(listProductType); // Fill ComboBox with Product Types
                FillDataDGV(listProduct);      // Fill DataGridView with Products

                // Initially load product names based on the first product type
                if (listProductType.Count > 0)
                {
                    cbo_LoaiSP.SelectedIndex = 0; // Select first item
                    LoadProductNames((int)cbo_LoaiSP.SelectedValue); // Load corresponding product names
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillDataDGV(List<Product> listProduct)
        {
            dgv_quanLySanPham.Rows.Clear(); // Clear existing rows in DataGridView

            foreach (var product in listProduct)
            {
                int RowNew = dgv_quanLySanPham.Rows.Add();
                dgv_quanLySanPham.Rows[RowNew].Cells[0].Value = product.Product_id; // Mã SP
                dgv_quanLySanPham.Rows[RowNew].Cells[1].Value = _productItemService.GetProductItemName(product.Product_name_id); // Tên SP
                dgv_quanLySanPham.Rows[RowNew].Cells[2].Value = _productTypeService.GetProductTypeName(product.Product_type_id); // Loại SP
                dgv_quanLySanPham.Rows[RowNew].Cells[3].Value = product.Quantity; // Số lượng

                // Determine if the product is on discount based on expiration
                product.On_discount = (product.Expired_day - DateTime.Now).TotalDays < 3;

                // Update price if discount is applicable
                double displayPrice = product.Sell_Price;

                if (product.On_discount)
                {
                    displayPrice *= 0.9; // Apply 10% discount
                }

                dgv_quanLySanPham.Rows[RowNew].Cells[4].Value = displayPrice; // Update displayed price
                dgv_quanLySanPham.Rows[RowNew].Cells[5].Value = product.Expired_day.ToString("yyyy-MM-dd"); // Ngày hết hạn
                dgv_quanLySanPham.Rows[RowNew].Cells[6].Value = product.On_discount; // Set checkbox based on On_discount property
            }
        }

        private void FillDataCBB(List<Product_type> listProductType)
        {
            cbo_LoaiSP.DataSource = listProductType;
            cbo_LoaiSP.DisplayMember = "Product_type_name";
            cbo_LoaiSP.ValueMember = "Product_type_id";
            cbo_LoaiSP.SelectedIndexChanged += cbo_LoaiSP_SelectedIndexChanged; // Add event handler
        }

        private void cbo_LoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_LoaiSP.SelectedValue is int selectedTypeId)
            {
                LoadProductNames(selectedTypeId); // Load product names based on selected type
            }
        }

        private void LoadProductNames(int productTypeId)
        {
            List<Product_Item> productNames = _productItemService.GetProductItemsByTypeId(productTypeId);
            cbo_TenSP.DataSource = productNames;
            cbo_TenSP.DisplayMember = "Product_name";
            cbo_TenSP.ValueMember = "Product_name_id";
        }

        private void loadForm()
        {
            txt_SL.Clear();
            txt_GiaBan.Clear();
            cbo_LoaiSP.SelectedIndex = -1; // Reset combo box
            cbo_TenSP.SelectedIndex = -1; // Reset product name combo box
        }

        private void loadDGV()
        {
            List<Product> newList = _productService.GetAllProducts();
            FillDataDGV(newList);
        }

        private bool CheckDataInput()
        {
            if (string.IsNullOrWhiteSpace(cbo_LoaiSP.Text) ||
                string.IsNullOrWhiteSpace(cbo_TenSP.Text) ||
                string.IsNullOrWhiteSpace(txt_SL.Text) ||
                string.IsNullOrWhiteSpace(txt_GiaBan.Text))
            {
                MessageBox.Show("Bạn chưa nhập đúng thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(txt_SL.Text, out _))
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

        private int CheckIdProduct(string IDProduct)
        {
            int length = dgv_quanLySanPham.Rows.Count; // Count rows in DataGridView
            for (int i = 0; i < length; i++)
            {
                if (dgv_quanLySanPham.Rows[i].Cells[0].Value != null)
                    if (dgv_quanLySanPham.Rows[i].Cells[0].Value.ToString() == IDProduct)
                        return i;
            }
            return -1; // Product not found
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (CheckDataInput())
            {
                string productId = $"{cbo_TenSP.SelectedValue}{DateTime.Now.ToString("ddMMyyyyHHmmss")}"; // Keep this line as it is

                if (CheckIdProduct(productId) == -1) // New product
                {
                    try
                    {
                        Product sp = new Product(); // Create new product
                        sp.Product_id = productId; // Use generated Product_id

                        if (cbo_TenSP.SelectedItem is Product_Item selectedItem)
                        {
                            sp.Product_name_id = selectedItem.Product_name_id; // Extract Product_name_id from the Product_Item
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        sp.Quantity = Convert.ToInt32(txt_SL.Text);
                        sp.Sell_Price = Convert.ToDouble(txt_GiaBan.Text);
                        sp.Expired_day = dtpHSD.Value;
                        sp.Import_day = DateTime.Now; // Set Import_day to current time
                        sp.On_discount = false; // Default value

                        if (cbo_LoaiSP.SelectedValue is int productTypeId)
                        {
                            sp.Product_type_id = productTypeId; // Assign Product_type_id
                        }
                        else
                        {
                            MessageBox.Show("Bạn chưa chọn loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        _productService.Product_add(sp); // Use service to add product
                        loadDGV();
                        loadForm();
                        MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sản phẩm đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (CheckDataInput())
            {
                Product sp = _productService.GetProductById(txt_MaSP.Text);
                if (sp != null)
                {
                    if (cbo_TenSP.SelectedItem is Product_Item selectedItem)
                    {
                        sp.Product_name_id = selectedItem.Product_name_id; // Extract Product_name_id from the Product_Item
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    sp.Quantity = Convert.ToInt32(txt_SL.Text);
                    sp.Sell_Price = Convert.ToDouble(txt_GiaBan.Text);
                    sp.Expired_day = dtpHSD.Value;
                    sp.On_discount = false; // Default value

                    if (cbo_LoaiSP.SelectedValue is int productTypeId)
                    {
                        sp.Product_type_id = productTypeId; // Assign Product_type_id
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _productService.Product_update(sp); // Use service to update product
                    loadDGV();
                    loadForm();
                    MessageBox.Show("Sửa đổi dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgv_quanLySanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_MaSP.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells[0].Value.ToString(); // Set the ID
                cbo_TenSP.SelectedValue = _productService.GetProductById(txt_MaSP.Text)?.Product_name_id; // Set Product name based on ID
                txt_SL.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells[3].Value.ToString(); // Set Quantity
                txt_GiaBan.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells[4].Value.ToString(); // Set Sell Price
                dtpHSD.Value = DateTime.Parse(dgv_quanLySanPham.Rows[e.RowIndex].Cells[5].Value.ToString()); // Set Expiration Date
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            // Assuming you have a TextBox named txt_ProductID to input the product ID to delete
            string productId = txt_MaSP.Text.Trim(); // Get the product ID from the input

            if (string.IsNullOrEmpty(productId))
            {
                MessageBox.Show("Please enter a valid Product ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product_service productService = new Product_service();
            int result = productService.Product_delete(productId);

            switch (result)
            {
                case 0:
                    MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_MaSP.Text = string.Empty;
                    loadDGV(); 
                    break;
                case -1:
                    MessageBox.Show("Invalid Product ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -2:
                    MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("An unexpected error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void dgv_quanLySanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Set the ID of the selected product
                txt_MaSP.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Get the selected product
                Product sp = _productService.GetProductById(txt_MaSP.Text);

                if (sp != null)
                {
                    // Set the product type in the cbo_LoaiSP ComboBox
                    cbo_LoaiSP.SelectedValue = sp.Product_type_id; // Set Product Type based on ID

                    // Set the product name in the cbo_TenSP ComboBox
                    cbo_TenSP.SelectedValue = sp.Product_name_id; // Set Product name based on ID

                    // Set other details
                    txt_SL.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells[3].Value.ToString(); // Set Quantity
                    txt_GiaBan.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells[4].Value.ToString(); // Set Sell Price
                    dtpHSD.Value = DateTime.Parse(dgv_quanLySanPham.Rows[e.RowIndex].Cells[5].Value.ToString()); // Set Expiration Date
                }
            }
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thêmLoạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thêmMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void txtTimTenSP_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FilterProducts()
        {

        }
    }
}
