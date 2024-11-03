using QLTP.BLL;
using QLTP.DAL;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Linq;

namespace QLTP.GUI
{
    public partial class frm_managerAddItems : Form
    {
        private readonly ProductItem_service _productItemService; // Service for product item operations
        private readonly Product_type_service _productTypeService;

        public frm_managerAddItems()
        {
            InitializeComponent();
            _productItemService = new ProductItem_service();
            _productTypeService = new Product_type_service();
        }

        private void frm_managerAddItems_Load(object sender, EventArgs e)
        {
            try
            {
                // Load product types
                FillDataCBB();
                LoadProductItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillDataCBB()
        {
            cbo_ProductTypeName.DataSource = _productTypeService.GetAllProductTypes();
            cbo_ProductTypeName.DisplayMember = "Product_type_name";
            cbo_ProductTypeName.ValueMember = "Product_type_id";
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrEmpty(txt_ProductNameID.Text) || string.IsNullOrEmpty(txt_ProductName.Text) || cbo_ProductTypeName.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected Product Type ID from ComboBox
            int productTypeId = (int)cbo_ProductTypeName.SelectedValue; // Assuming the ValueMember is set to Product_type_id
            string productNameID = txt_ProductNameID.Text;
            string productName = txt_ProductName.Text;

            // Create a new Product_Item object
            Product_Item newProductItem = new Product_Item
            {
                Product_name_id = productNameID,
                Product_name = productName,
                Product_type_id = productTypeId
            };

            // Add the product item to the database
            int result = _productItemService.Product_item_add(newProductItem);

            // Check the result of the add operation
            if (result == 0)
            {
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the DataGridView to show the new item
                LoadProductItems();
            }
            else if (result == -1)
            {
                MessageBox.Show("Failed to add product. Product data is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("An unknown error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrEmpty(txt_ProductNameID.Text) || string.IsNullOrEmpty(txt_ProductName.Text) || cbo_ProductTypeName.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get product details from controls
            int productTypeId = (int)cbo_ProductTypeName.SelectedValue;
            string productNameID = txt_ProductNameID.Text;
            string productName = txt_ProductName.Text;

            // Retrieve existing Product_Item by product_name_id
            var existingProductItem = _productItemService.GetProductItemById(productNameID);
            if (existingProductItem == null)
            {
                MessageBox.Show("Product not found for the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update product item details
            existingProductItem.Product_type_id = productTypeId;
            existingProductItem.Product_name = productName;

            // Perform the update operation
            int result = _productItemService.Product_item_update(existingProductItem);
            if (result == 0)
            {
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh DataGridView
                LoadProductItems();
            }
            else
            {
                MessageBox.Show("Failed to update product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            // Check if product_name_id is provided
            string productNameID = txt_ProductNameID.Text;
            if (string.IsNullOrEmpty(productNameID))
            {
                MessageBox.Show("Please enter a Product ID to delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm deletion
            var confirmResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                // Perform the delete operation
                int result = _productItemService.Product_item_delete(productNameID);

                if (result == 0)
                {
                    MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh DataGridView
                    LoadProductItems();
                }
                else
                {
                    MessageBox.Show("Failed to delete product. Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadProductItems()
        {
            // Clear existing rows to prevent duplication
            dgv_productItem.Rows.Clear();

            // Retrieve all product items using the service
            var productItems = _productItemService.GetAllProductItems();

            foreach (var productItem in productItems)
            {
                // Get the product type name based on Product_type_id
                string productTypeName = _productTypeService.GetProductTypeName(productItem.Product_type_id);

                // Create a new row and add it to the DataGridView
                int rowNew = dgv_productItem.Rows.Add();

                // Assign values to the specific columns by name
                dgv_productItem.Rows[rowNew].Cells["colLoaiSP"].Value = productTypeName; // Product type name
                dgv_productItem.Rows[rowNew].Cells["colMaSP"].Value = productItem.Product_name_id; // Product ID
                dgv_productItem.Rows[rowNew].Cells["colTenSP"].Value = productItem.Product_name; // Product name
            }
        }


        private void dgv_productItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the row is valid
            {
                // Get the selected row
                DataGridViewRow row = dgv_productItem.Rows[e.RowIndex];

                // Get values from the row
                string productTypeName = row.Cells["colLoaiSP"].Value.ToString();
                string productNameID = row.Cells["colMaSP"].Value.ToString();
                string productName = row.Cells["colTenSP"].Value.ToString();

                // Set values in ComboBox and TextBoxes
                cbo_ProductTypeName.Text = productTypeName; // Set Product Type Name in ComboBox
                txt_ProductNameID.Text = productNameID;     // Set Product Name ID in TextBox
                txt_ProductName.Text = productName;         // Set Product Name in TextBox
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_managerProduct frm = new frm_managerProduct();
            frm.ShowDialog();
            this.Close();
        }

        private void txt_searchAny_TextChanged(object sender, EventArgs e)
        {
            // Get the search term from the text box
            string searchTerm = txt_searchAny.Text.Trim();

            // Call the service method to search for products
            var searchResults = _productItemService.SearchProductsByName(searchTerm);

            // Clear existing rows in the DataGridView
            dgv_productItem.Rows.Clear();

            // Populate the DataGridView with the search results
            foreach (var product in searchResults)
            {
                int rowNew = dgv_productItem.Rows.Add();
                dgv_productItem.Rows[rowNew].Cells["colLoaiSP"].Value = product.Product_name_id; // Product ID
                dgv_productItem.Rows[rowNew].Cells["colMaSP"].Value = product.Product_name; // Product name
                dgv_productItem.Rows[rowNew].Cells["colTenSP"].Value = _productTypeService.GetProductTypeName(product.Product_type_id); // Product type name
                                                                                                                                              // Add any additional product fields here as needed
            }
        }
    }
}
