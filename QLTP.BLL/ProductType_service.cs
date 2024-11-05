using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTP.BLL
{
    public class Product_type_service
    {
        // Add a new product type
        public int Product_type_add(Product_type product_type)
        {
            if (product_type == null)
                return -1; // Error: Null product_type
            using (QLTP_Entities db = new QLTP_Entities())
            {
                db.Product_type.Add(product_type);
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Update an existing product type
        public int Product_type_update(Product_type product_type)
        {
            if (product_type == null)
                return -1; // Error: Null product_type

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var existingProductType = db.Product_type.FirstOrDefault(p => p.Product_type_id == product_type.Product_type_id);
                if (existingProductType == null)
                {
                    return -2; // Error: Product type does not exist
                }

                existingProductType.Product_type_name = product_type.Product_type_name;
                db.Entry(existingProductType).State = EntityState.Modified;
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Delete a product type
        public int Product_type_delete(int product_type_id)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                var productTypeToDelete = db.Product_type.FirstOrDefault(p => p.Product_type_id == product_type_id);
                if (productTypeToDelete != null)
                {
                    db.Product_type.Remove(productTypeToDelete);
                    db.SaveChanges();
                    return 0; // Success
                }
                return -2; // Error: Product type not found
            }
        }

        // Retrieve all product types
        public List<Product_type> GetAllProductTypes()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Product_type.ToList(); // Return all product types
            }
        }

        // Retrieve a product type by ID
        public Product_type GetProductTypeById(int product_type_id)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Product_type.FirstOrDefault(p => p.Product_type_id == product_type_id); // Find product type by ID
            }
        }
        public string GetProductTypeName(int product_type_id)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Find the product type by its ID
                var productType = db.Product_type.FirstOrDefault(p => p.Product_type_id == product_type_id);
                return productType?.Product_type_name; // Return product type name or null if not found
            }
        }
    }
}