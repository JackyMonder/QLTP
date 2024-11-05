using QLTP.DAL;
//using QLTP.DAL.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTP.BLL
{
    public class Employee_service
    {
        // Method to add a new employee
        public int Employee_add(Employee employee)
        {
            if (employee == null)
                return -1; // Null error

            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Check if the employee already exists based on some unique criteria like Emp_id or Email
                if (db.Employee.Any(n => n.Email.Equals(employee.Email, StringComparison.OrdinalIgnoreCase)))
                    return -2; // Email already exists

                db.Employee.Add(employee);
                db.SaveChanges();
                return 0; // Successful operation
            }
        }

        // Method to update an existing employee
        public int Employee_update(Employee employee)
        {
            if (employee == null) return -1; // Error: null employee object

            using (QLTP_Entities db = new QLTP_Entities())
            {
                // Find the existing employee by some unique field, for example, Emp_id
                var employeeToUpdate = db.Employee.FirstOrDefault(e => e.Emp_id == employee.Emp_id);

                if (employeeToUpdate == null)
                    return -2; // Error: employee not found

                // Update employee fields
                employeeToUpdate.Full_name = employee.Full_name;
                employeeToUpdate.Address = employee.Address;
                employeeToUpdate.Email = employee.Email;
                employeeToUpdate.Phone_number = employee.Phone_number;
                employeeToUpdate.Position = employee.Position;
                employeeToUpdate.Sex = employee.Sex;
                employeeToUpdate.Salary = employee.Salary;
                employeeToUpdate.Username = employee.Username; // Ensure Emp_id matches for relational integrity

                db.Entry(employeeToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return 0; // Successful operation
            }
        }

        // Method to delete a employee by Employee_id
        public int Employee_delete(string employee_id)
        {
            if (String.IsNullOrEmpty(employee_id)) return -1; // Error: empty or null employee ID

            using (QLTP_Entities db = new QLTP_Entities())
            {
                var employeeToDelete = db.Employee.FirstOrDefault(e => e.Emp_id == employee_id);

                if (employeeToDelete == null)
                    return -2; // Error: employee not found

                db.Employee.Remove(employeeToDelete);
                db.SaveChanges();
                return 0; // Successful operation
            }
        }

        // Method to get a list of all employees
        public List<Employee> Employee_search_all()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Employee.ToList();
            }
        }

        // Method to get a single employee by their Employee_id
        public Employee Employee_search_unit(string employee_id)
        {
            if (String.IsNullOrEmpty(employee_id)) return null;

            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Employee.FirstOrDefault(e => e.Emp_id == employee_id);
            }
        }
        public List<Employee> Employee_search_by_full_name(string fullName)
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Employee
                    .Where(c => c.Full_name.Contains(fullName)) // Adjust the logic as necessary
                    .ToList();
            }
        }

        public List<string> Employee_get_all_user_ids()
        {
            using (QLTP_Entities db = new QLTP_Entities())
            {
                return db.Account.Select(a => a.Username).Distinct().ToList(); // Retrieves distinct User_ids
            }
        }
    }
}
