using EmployeeAppWpf.Models.Configurations;
using EmployeeAppWpf.Models.Domains;
using EmployeeAppWpf.Properties;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmployeeAppWpf
{
    public class ApplicationDbContext : DbContext
    {
        private static string _connectionString = $@"Server={Settings.Default.ServerAdress}\{Settings.Default.ServerName};
                                                    Database={Settings.Default.DbName};
                                                    User Id={Settings.Default.Login};
                                                    Password={Settings.Default.Password}";

        public ApplicationDbContext()
            : base(_connectionString)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfigurations());
            modelBuilder.Configurations.Add(new UsersConfigurations());
        }
    }

}