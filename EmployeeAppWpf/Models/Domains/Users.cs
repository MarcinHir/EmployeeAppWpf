﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppWpf.Models.Domains
{
    public class Users
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        //public int AccesLevelId { get; set; }
    }
}
