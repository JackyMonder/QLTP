using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;

namespace QLTP.BLL
{
    public class ProductItem_service
    {
        // Add a new product item
        public int Product_item_add(Product_Item product_item)
        {
            if (product_item == null)
                return -1; // Error: Null product_item
            using (QLTP_Entities db = new QLTP_Entities())
            {
                db.Product_Item.Add(product_item);
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Update an existing product item
        public int Product_item_update(Product_Item product_item)
        {
            if (product_item == null)
                return -1; // Error: Null product_item

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var existingProductItem = db.Product_Item.FirstOrDefault(p => p.Product_name_id == product_item.Product_name_id);
                if (existingProductItem == null)
                {
                    return -2; // Error: Product item does not exist
                }

                existingProductItem.Product_name = product_item.Product_name;
                existingProductItem.Product_type_id = product_item.Product_type_id;

                db.Entry(existingProductItem).State = EntityState.Modified;
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Delete a product item
        public int Product_item_delete(string product_name_id)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                var productItemToDelete = db.Product_Item.FirstOrDefault(p => p.Product_name_id == product_name_id);
                if (productItemToDelete != null)
                {
                    db.Product_Item.Remove(productItemToDelete);
                    db.SaveChanges();
                    return 0; // Success
                }
                return -2; // Error: Product item not found
            }
        }

        // Retrieve all product items
        public List<Product_Item> GetAllProductItems()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Product_Item.ToList(); // Return all product items
            }
        }

        // Retrieve a product item by ID
        public Product_Item GetProductItemById(string product_name_id)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Product_Item.FirstOrDefault(p => p.Product_name_id == product_name_id); // Find product item by ID
            }
        }
        public string GetProductItemName(string product_name_id)
        {
            if (string.IsNullOrEmpty(product_name_id))
                return null; // Error: Invalid product_name_id

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var productItem = db.Product_Item.FirstOrDefault(p => p.Product_name_id == product_name_id);
                return productItem?.Product_name; // Return product name or null if not found
            }
        }

        public List<Product_Item> GetProductItemsByTypeId(int product_type_id)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Get all product items that match the specified product type ID
                return db.Product_Item
                         .Where(p => p.Product_type_id == product_type_id)
                         .ToList(); // Return the list of product items
            }
        }

        public List<Product_Item> SearchProductsByName(string searchTerm)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Product_Item
                    .Where(c => c.Product_name.Contains(searchTerm)) // Adjust the logic as necessary
                    .ToList();
            }
        }
    }
}
