using EmployeeAppWpf.Models.Domains;
using EmployeeAppWpf.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeAppWpf.Models.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeWrapper ToWrapper(this Employee model)
        {
            return new EmployeeWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Description = model.Description,
                Salary = model.Salary,
                StartWorkingDate = model.StartWorkingDate,
                EndWorkingDate = model.EndWorkingDate,
                FireOrNot = model.FireOrNot
            };
        }

        public static Employee ToDao(this EmployeeWrapper model)
        {
            return new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Description = model.Description,
                Salary = model.Salary,
                StartWorkingDate = model.StartWorkingDate,
                EndWorkingDate = model.EndWorkingDate,
                FireOrNot = model.FireOrNot
            };
        }
    }
}

