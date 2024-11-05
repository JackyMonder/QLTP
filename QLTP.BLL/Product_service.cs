using QLTP.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTP.BLL
{
    public class Product_service
    {
        // Add a new product
        public int Product_add(Product product)
        {
            if (product == null)
                return -1; // Error: Null product
            using (QLTP_Entities db = new QLTP_Entities())
            {
                db.Product.Add(product);
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Update an existing product
        public int Product_update(Product product)
        {
            if (product == null)
                return -1; // Error: Null product

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var existingProduct = db.Product.FirstOrDefault(p => p.Product_id == product.Product_id);
                if (existingProduct == null)
                {
                    return -2; // Error: Product does not exist
                }

                // Update fields
                existingProduct.Product_type_id = product.Product_type_id;
                existingProduct.Product_name_id = product.Product_name_id;
                existingProduct.Sell_Price = product.Sell_Price;
                existingProduct.Expired_day = product.Expired_day;
                existingProduct.Quantity = product.Quantity;
                existingProduct.On_discount = product.On_discount;

                db.Entry(existingProduct).State = EntityState.Modified;
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Delete a product
        public int Product_delete(string product_id)
        {
            if (string.IsNullOrEmpty(product_id))
                return -1; // Error: Invalid product_id

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var productToDelete = db.Product.FirstOrDefault(p => p.Product_id == product_id);
                if (productToDelete != null)
                {
                    db.Product.Remove(productToDelete);
                    db.SaveChanges();
                    return 0; // Success
                }
                return -2; // Error: Product not found
            }
        }

        // Retrieve all products
        public List<Product> GetAllProducts()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Sử dụng Include để nạp trước các thuộc tính liên quan
                return db.Product.Include(p => p.Product_Item).ToList();
            }
        }

        // Retrieve a product by ID
        public Product GetProductById(string product_id)
        {
            if (string.IsNullOrEmpty(product_id))
                return null; // Error: Invalid product_id

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Product.FirstOrDefault(p => p.Product_id == product_id); // Find product by product_id
            }
        }

        public List<Product> SearchProductsByName(string searchTerm)
        {
            var allProducts = GetAllProducts(); // Fetch all products

            // Filter products based on the search term
            var filteredProducts = allProducts
                .Where(p => p.Product_name_id.ToString().ToLower().Contains(searchTerm.ToLower())) // Assuming Product_name_id is an int and not a name
                .ToList();

            return filteredProducts;
        }

        public int UpdateOnDiscountState()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                var products = db.Product.ToList(); // Retrieve all products from the database

                foreach (var product in products)
                {
                    bool withinDiscountPeriod = (product.Expired_day - DateTime.Now).TotalDays < 3;

                    // Apply discount logic based on `On_discount` and expiration conditions
                    if (!product.On_discount && withinDiscountPeriod)
                    {
                        product.On_discount = true;
                        product.Sell_Price *= 0.9; // Apply 10% discount
                    }
                    else if (product.On_discount && !withinDiscountPeriod)
                    {
                        product.On_discount = false;
                        product.Sell_Price /= 0.9; // Restore to original price if discount is no longer applicable
                    }

                    db.Entry(product).State = EntityState.Modified; // Mark as modified for update
                }

                return db.SaveChanges(); // Save changes and return the number of affected rows
            }
        }
    }
}
