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
    public static class UserConverter
    {
        public static Users ToDao(this UserWrapper model)
        {
            return new Users
            {
                Id = model.Id,
                UserLogin = model.UserLogin,
                UserPassword = model.UserPassword
            };
        }
    }
}
