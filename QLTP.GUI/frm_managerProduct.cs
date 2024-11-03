using QLTP.BLL;
using QLTP.DAL;

//using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
                FillProductTypeCBB(); // Fill ComboBox with Product Types
                FillDataDGV(); // Load Products into DataGridView

                // Initially load product names based on the first product type
                if (cbo_LoaiSP.Items.Count > 0)
                {
                    cbo_LoaiSP.SelectedIndex = 0; // Select first item
                    FillProductNameCBB((int)cbo_LoaiSP.SelectedValue); // Load corresponding product names
                }
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
                dgv_quanLySanPham.Rows[RowNew].Cells["colMaSP"].Value = product.Product_id; // Mã SP
                dgv_quanLySanPham.Rows[RowNew].Cells["colTenSP"].Value = _productItemService.GetProductItemName(product.Product_name_id); // Tên SP
                dgv_quanLySanPham.Rows[RowNew].Cells["colLoaiSP"].Value = _productTypeService.GetProductTypeName(product.Product_type_id); // Loại SP
                dgv_quanLySanPham.Rows[RowNew].Cells["colSL"].Value = product.Quantity; // Số lượng

                // Calculate the display price based on the `On_discount` status
                double displayPrice = product.On_discount ? product.Sell_Price * 0.9 : product.Sell_Price;
                dgv_quanLySanPham.Rows[RowNew].Cells["colGiaBan"].Value = displayPrice; // Update displayed price

                dgv_quanLySanPham.Rows[RowNew].Cells["colHSD"].Value = product.Expired_day.ToString("yyyy-MM-dd"); // Ngày hết hạn

                // Set checkbox based on On_discount state
                dgv_quanLySanPham.Rows[RowNew].Cells["colGiamGia"].Value = product.On_discount;
            }
        }

        private void FillProductTypeCBB()
        {
            var listProductType = _productTypeService.GetAllProductTypes(); // Call service directly
            cbo_LoaiSP.DataSource = listProductType;
            cbo_LoaiSP.DisplayMember = "Product_type_name";
            cbo_LoaiSP.ValueMember = "Product_type_id";
            cbo_LoaiSP.SelectedIndexChanged += cbo_LoaiSP_SelectedIndexChanged; // Add event handler
        }

        private void cbo_LoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_LoaiSP.SelectedValue is int selectedTypeId)
            {
                FillProductNameCBB(selectedTypeId); // Load product names based on selected type
            }
        }

        private void FillProductNameCBB(int productTypeId)
        {
            var productNames = _productItemService.GetProductItemsByTypeId(productTypeId); // Call service directly
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
            txt_MaSP.Clear();
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
                {
                    if (dgv_quanLySanPham.Rows[i].Cells[0].Value.ToString() == IDProduct)
                        return i;
                }
            }
            return -1; // Product not found
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            // Check if the input data is valid
            if (!CheckDataInput())
            {
                return; // Exit if validation fails
            }

            // Generate a unique Product ID
            string productId = $"{cbo_TenSP.SelectedValue}{DateTime.Now:ddMMyyyyHHmmss}";

            // Check if the product already exists
            if (CheckIdProduct(productId) != -1)
            {
                MessageBox.Show("Sản phẩm đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Exit if the product already exists
            }

            // Validate the selected product from the dropdown
            if (cbo_TenSP.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if no product is selected
            }

            // Retrieve selected product item
            var selectedItemName = _productItemService.GetProductItemById(cbo_TenSP.SelectedValue.ToString());
            if (selectedItemName == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if the selected item is not found
            }

            // Validate input fields
            if (!int.TryParse(txt_SL.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if quantity is invalid
            }

            if (!double.TryParse(txt_GiaBan.Text, out double sellPrice) || sellPrice < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if sell price is invalid
            }

            // Validate selected product type
            if (cbo_LoaiSP.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if no product type is selected
            }

            // Try to convert the selected value to an integer
            if (!int.TryParse(cbo_LoaiSP.SelectedValue.ToString(), out int productTypeId))
            {
                MessageBox.Show("Loại sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if the selected value cannot be converted to an integer
            }

            // Create a new product object
            Product productToAdd = new Product
            {
                Product_id = productId,
                Product_name_id = selectedItemName.Product_name_id,
                Product_type_id = productTypeId,
                Quantity = quantity,
                Sell_Price = sellPrice,
                Expired_day = dtpHSD.Value,
                Import_day = DateTime.Now, // Set Import day to current time
                On_discount = false // Default value, set based on business logic
            };

            try
            {
                // Call the service method to add the product
                int result = _productService.Product_add(productToAdd);

                // Check if the product was added successfully
                if (result == 0)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDataDGV(); // Reload the DataGridView
                    loadForm(); // Reset input fields
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            // Get the product ID from txt_MaSP
            string selectedProductId = txt_MaSP.Text.Trim();

            // Validate the product ID
            if (string.IsNullOrEmpty(selectedProductId))
            {
                MessageBox.Show("Mã sản phẩm không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if product ID is empty
            }

            // Check if the input data is valid
            if (!CheckDataInput())
            {
                return; // Exit if validation fails
            }

            // Retrieve selected product item and validate
            var selectedItemName = _productItemService.GetProductItemById(cbo_TenSP.SelectedValue.ToString());
            if (selectedItemName == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if the selected item is not found
            }

            try
            {
                // Validate quantity and price inputs
                if (!int.TryParse(txt_SL.Text, out int quantity) || quantity < 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit if quantity is invalid
                }

                if (!double.TryParse(txt_GiaBan.Text, out double sellPrice) || sellPrice < 0)
                {
                    MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit if sell price is invalid
                }

                // Validate selected product type
                if (cbo_LoaiSP.SelectedValue == null)
                {
                    MessageBox.Show("Bạn chưa chọn loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit if no product type is selected
                }

                // Try to convert the selected value to an integer
                if (!int.TryParse(cbo_LoaiSP.SelectedValue.ToString(), out int productTypeId))
                {
                    MessageBox.Show("Loại sản phẩm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit if the selected value cannot be converted to an integer
                }

                // Create a product object for updating
                Product productToUpdate = new Product
                {
                    Product_id = selectedProductId,
                    Product_name_id = selectedItemName.Product_name_id,
                    Product_type_id = productTypeId,
                    Quantity = quantity,
                    Sell_Price = sellPrice,
                    Expired_day = dtpHSD.Value,
                    Import_day = DateTime.Now // Set Import day to current time
                };

                // Call the service method to update the product
                int result = _productService.Product_update(productToUpdate);

                // Check if the product was updated successfully
                if (result == 0)
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDataDGV(); // Reload the DataGridView
                    loadForm(); // Reset input fields
                }
                else
                {
                    MessageBox.Show("Cập nhật sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            // Get the product ID from txt_MaSP
            string selectedProductId = txt_MaSP.Text.Trim();

            // Validate the product ID
            if (string.IsNullOrEmpty(selectedProductId))
            {
                MessageBox.Show("Mã sản phẩm không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit if product ID is empty
            }

            // Confirm deletion
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult != DialogResult.Yes)
            {
                return; // Exit if not confirmed
            }

            try
            {
                // Call the service method to delete the product
                int result = _productService.Product_delete(selectedProductId);

                // Check if the product was deleted successfully
                if (result == 0)
                {
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDataDGV(); // Reload the DataGridView
                    loadForm(); // Reset input fields
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm thất bại! Sản phẩm không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_quanLySanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Only proceed if a valid row is clicked
            if (e.RowIndex >= 0)
            {
                // Check if the clicked column is colTenSP, colMaSP, or colLoaiSP
                if (dgv_quanLySanPham.Columns[e.ColumnIndex].Name == "colTenSP" ||
                    dgv_quanLySanPham.Columns[e.ColumnIndex].Name == "colMaSP" ||
                    dgv_quanLySanPham.Columns[e.ColumnIndex].Name == "colLoaiSP")
                {
                    // Set the ID of the selected product
                    txt_MaSP.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells["colMaSP"].Value.ToString();

                    // Get the selected product from the service
                    Product sp = _productService.GetProductById(txt_MaSP.Text);

                    if (sp != null)
                    {
                        // Set the product type in the cbo_LoaiSP ComboBox
                        cbo_LoaiSP.SelectedValue = sp.Product_type_id; // Set Product Type based on ID

                        // Set the product name in the cbo_TenSP ComboBox
                        cbo_TenSP.SelectedValue = sp.Product_name_id; // Set Product Name based on ID

                        // Set other details
                        txt_SL.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells["colSL"].Value.ToString(); // Set Quantity
                        txt_GiaBan.Text = dgv_quanLySanPham.Rows[e.RowIndex].Cells["colGiaBan"].Value.ToString(); // Set Sell Price
                        dtpHSD.Value = DateTime.Parse(dgv_quanLySanPham.Rows[e.RowIndex].Cells["colHSD"].Value.ToString()); // Set Expiration Date
                    }
                }
            }
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_manager_mainPage frm = new frm_manager_mainPage();
            frm.ShowDialog();
            this.Close();
        }

        private void thêmLoạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thêmMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_managerAddItems frm = new frm_managerAddItems();
            frm.ShowDialog();
            frm.Close();
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
                    dgv_quanLySanPham.Rows[rowNew].Cells["colMaSP"].Value = product.Product_id;
                    dgv_quanLySanPham.Rows[rowNew].Cells["colTenSP"].Value = productItem.Product_name; // Set the product name from Product_Item
                    dgv_quanLySanPham.Rows[rowNew].Cells["colLoaiSP"].Value = _productTypeService.GetProductTypeName(product.Product_type_id);
                    dgv_quanLySanPham.Rows[rowNew].Cells["colSL"].Value = product.Quantity;
                    dgv_quanLySanPham.Rows[rowNew].Cells["colGiaBan"].Value = product.Sell_Price;
                    dgv_quanLySanPham.Rows[rowNew].Cells["colHSD"].Value = product.Expired_day.ToString("dd/MM/yyyy");

                    // Set the checkbox state based on the On_discount property
                    dgv_quanLySanPham.Rows[rowNew].Cells["colGiamGia"].Value = product.On_discount; // true for checked, false for unchecked
                }
            }
        }
    }
}
