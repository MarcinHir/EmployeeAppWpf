using EmployeeAppWpf.Models.Converters;
using EmployeeAppWpf.Models.Domains;
using EmployeeAppWpf.Models.Wrappers;
using EmployeeAppWpf.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeAppWpf
{
    public class Repository
    {
        public List<EmployeeWrapper> GetEmployee(int IsWorkingFromEnum)
        {
            using (var context = new ApplicationDbContext())
            {
                var employees = context.Employees   
                    .AsQueryable();

                if (IsWorkingFromEnum == 2)
                    employees = employees.Where(x => x.FireOrNot == false);
                else if(IsWorkingFromEnum == 3)
                    employees = employees.Where(x => x.FireOrNot == true);                    
                return employees
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();
            }
        }

        public void UpdateEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();
            using (var context = new ApplicationDbContext())
            {
                UpdateEmployeeProperties(context, employee);
                context.SaveChanges();
            }
        }

        public void FireEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();
            using (var context = new ApplicationDbContext())
            {
                FireEmployeeProperties(context, employee);
                context.SaveChanges();
            }
        }

        private void UpdateEmployeeProperties(ApplicationDbContext context, Employee employee)
        {
            var employeeToUpdate = context.Employees.Find(employee.Id);
            employeeToUpdate.Description = employee.Description;
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Salary = employee.Salary;
            employeeToUpdate.StartWorkingDate = employee.StartWorkingDate;
            employeeToUpdate.EndWorkingDate = employee.EndWorkingDate;
            employeeToUpdate.FireOrNot = employee.FireOrNot;
        }

        private void FireEmployeeProperties(ApplicationDbContext context, Employee employee)
        {
            var employeeToFire = context.Employees.Find(employee.Id);
            employeeToFire.Description = employee.Description;
            employeeToFire.FirstName = employee.FirstName;
            employeeToFire.LastName = employee.LastName;
            employeeToFire.Salary = employee.Salary;
            employeeToFire.StartWorkingDate = employee.StartWorkingDate;
            employeeToFire.EndWorkingDate = employee.EndWorkingDate;
            employeeToFire.FireOrNot = true;
        }

        public void AddEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();
            using (var context = new ApplicationDbContext())
            {
                var dbEmployee = context.Employees.Add(employee);
                context.SaveChanges();
            }

        }

        public bool LogUser(UserWrapper userWrapper)
        {
            
            var user = userWrapper.ToDao();
            var context = new ApplicationDbContext();
            return CheckUser(context, user);
                //    Settings.Default.Authorization = true;
                //else
                //    Settings.Default.Authorization = false;
            
        }
        public bool CheckUser(ApplicationDbContext context, Users user)
        {
            var userToLogin = context.Users.Where(c => c.UserLogin == user.UserLogin).FirstOrDefault();
            if (userToLogin != null)
            {
                if (userToLogin.UserPassword == user.UserPassword)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }     
    }
}

