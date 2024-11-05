using System;
using System.Collections.Generic;
using System.Linq;
using QLTP.DAL;

namespace QLTP.BLL
{
    public class Customer_service
    {
        public int Customer_add(Customer customer)
        {
            if (customer == null)
                return -1; // Error: null customer object

            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Check if a customer with the same email already exists
                if (db.Customer.Any(c => c.Email.Equals(customer.Email, StringComparison.OrdinalIgnoreCase)))
                    return -2; // Error: duplicate email

                // Generate new customer ID
                customer.Cus_id = GenerateNewCustomerId(); // Pass db context to ID generator

                db.Customer.Add(customer);
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Method to update an existing customer
        public int Customer_update(Customer customer)
        {
            if (customer == null)
                return -1; // Error: null customer object

            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Find the customer to update
                var customerToUpdate = db.Customer.FirstOrDefault(c => c.Cus_id == customer.Cus_id);
                if (customerToUpdate == null)
                    return -2; // Error: customer not found

                // Only update the specified fields
                customerToUpdate.Full_name = customer.Full_name; // Update Full_name
                customerToUpdate.Sex = customer.Sex; // Update Sex
                customerToUpdate.Email = customer.Email; // Update Email
                customerToUpdate.Address = customer.Address; // Update Address
                customerToUpdate.Phone_number = customer.Phone_number; // Update Phone_number

                // The Experience and Username fields will not be modified

                // Set the state of the entity to Modified
                db.Entry(customerToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Method to delete a customer by ID
        public int Customer_delete(string customer_id)
        {
            if (string.IsNullOrEmpty(customer_id))
                return -1; // Error: empty or null customer ID

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var customerToDelete = db.Customer.FirstOrDefault(c => c.Cus_id == customer_id);
                if (customerToDelete == null)
                    return -2; // Error: customer not found

                db.Customer.Remove(customerToDelete);
                db.SaveChanges();
                return 0; // Success
            }
        }

        // Method to get a list of all customers
        public List<Customer> Customer_search_all()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Customer.ToList();
            }
        }

        // Method to get a single customer by ID
        public Customer Customer_search_unit(string customer_id)
        {
            if (string.IsNullOrEmpty(customer_id))
                return null;

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Customer.FirstOrDefault(c => c.Cus_id == customer_id);
            }
        }

        // Method to get all unique customer IDs
        public List<string> Customer_get_all_user_ids()
        {
            using (QLTP_Entities db = new   QLTP_Entities())
            {
                return db.Customer.Select(c => c.Cus_id).Distinct().ToList();
            }
        }

        // Updated to take the database context as an argument
        public string GenerateNewCustomerId()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Get the latest customer ID from the database
                var lastCustomer = db.Customer
                    .OrderByDescending(c => c.Cus_id)
                    .FirstOrDefault();

                int newId;

                if (lastCustomer != null)
                {
                    // Extract the numerical part of the last customer ID (after "CTM_")
                    string lastIdString = lastCustomer.Cus_id;
                    string numberPart = lastIdString.Substring(4); // Remove "CTM_"
                    newId = int.Parse(numberPart) + 1; // Increment the number
                }
                else
                {
                    newId = 1; // Start from 1 if no customer exists
                }

                // Format the new ID with leading zeros and prefix it with "CTM_"
                return $"CTM_{newId:D4}"; // Format as "CTM_xxxx"
            }
        }
        public List<Customer> Customer_search_by_full_name(string fullName)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Customer
                    .Where(c => c.Full_name.Contains(fullName)) // Adjust the logic as necessary
                    .ToList();
            }
        }

        public Customer GetCustomerByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null; // Error: Invalid product_id

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Customer.FirstOrDefault(p => p.Username == username); // Find product by product_id
            }
        }
    }
}
