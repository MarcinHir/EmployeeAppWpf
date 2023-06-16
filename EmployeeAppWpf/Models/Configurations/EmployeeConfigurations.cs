using EmployeeAppWpf.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppWpf.Models.Configurations
{
    public class EmployeeConfigurations : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfigurations()
        {
            ToTable("dbo.Employees");
            HasKey(x => x.Id);
            Property(x => x.FirstName)
                .HasMaxLength(80)
                .IsRequired();
            Property(x => x.LastName)
                .HasMaxLength(80)
                .IsRequired();
            Property(x => x.Salary)
                .IsRequired();
            Property(x => x.StartWorkingDate)
                .IsRequired();

        }
    }
}
