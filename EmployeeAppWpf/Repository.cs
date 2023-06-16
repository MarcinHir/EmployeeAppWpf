using EmployeeAppWpf.Models.Converters;
using EmployeeAppWpf.Models.Domains;
using EmployeeAppWpf.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeAppWpf
{
    public class Repository
    {
        //public List<Group> GetGroups()
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        return context.Group.ToList();
        //    }
        //}

        public List<EmployeeWrapper> GetEmployee(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var employees = context.Employees
                    .AsQueryable();

                //if (groupId != 0)
                //    employees = employees.Where(x => x.StatusId == groupId);
                return employees
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();

            }
        }

        public void FireEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                //var studentToDelete = context.Students.Find(id);
                //context.Students.Remove(studentToDelete);
                //context.SaveChanges();
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

        private void UpdateEmployeeProperties(ApplicationDbContext context, Employee employee)
        {
            var employeeToUpdate = context.Employees.Find(employee.Id);
            employeeToUpdate.Description = employee.Description;
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Salary = employee.Salary;
            employeeToUpdate.StartWorkingDate = employee.StartWorkingDate;
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

    }
}

