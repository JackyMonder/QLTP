using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using QLTP.DAL;

namespace QLTP.BLL
{
    public class Customer_service
    {
        public string Cus_Id { get; set; }
        public int Customer_add(Customer customer)
        {
            if (customer == null)
                return -1; 

            using (QLTP_Entities db = new QLTP_Entities())
            {
                if (db.Customer.Any(n => n.Email.Equals(customer.Email, StringComparison.OrdinalIgnoreCase)))
                    return -2;

                db.Customer.Add(customer);
                db.SaveChanges();
                return 0;
            }
        }
        public int Customer_update(Customer customer)
        {
            if (customer == null) return -1; // Error: null customer object

            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Find the existing customer by some unique field, for example, User_id
                var customerToUpdate = db.Customer.FirstOrDefault(n => n.Cus_id == customer.Cus_id);

                if (customerToUpdate == null)
                    return -2; // Error: customer not found

                // Update customer fields
                customerToUpdate.Full_name = customer.Full_name;
                customerToUpdate.Address = customer.Address;
                customerToUpdate.Email = customer.Email;
                customerToUpdate.Phone_number = customer.Phone_number;
                customerToUpdate.Experience = customer.Experience;
                customerToUpdate.Username = customer.Username; // Ensure User_id matches for relational integrity

                db.Entry(customerToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return 0; // Successful operation
            }
        }

        // Method to delete a customer by Customer_id
        public int Customer_delete(string customer_id)
        {
            if (String.IsNullOrEmpty(customer_id)) return -1; // Error: empty or null customer ID

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var customerToDelete = db.Customer.FirstOrDefault(n => n.Cus_id == customer_id);

                if (customerToDelete == null)
                    return -2; // Error: customer not found

                db.Customer.Remove(customerToDelete);
                db.SaveChanges();
                return 0; // Successful operation
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

        // Method to get a single customer by their Customer_id
        public Customer Customer_search_unit(string customer_id)
        {
            if (String.IsNullOrEmpty(customer_id)) return null;

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Customer.FirstOrDefault(n => n.Cus_id == customer_id);
            }
        }
        public List<string> Customer_get_all_user_ids()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Retrieves distinct Customer_ids from the Customer table
                return db.Customer.Select(c => c.Cus_id).Distinct().ToList();
            }
        }

    }
}
