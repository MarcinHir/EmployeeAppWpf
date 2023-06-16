using EmployeeAppWpf.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppWpf.Models.Configurations
{
    public class UsersConfigurations : EntityTypeConfiguration<Users>
    {
        public UsersConfigurations()
        {
            ToTable("dbo.Users");
            HasKey(x => x.Id);
            Property(x => x.Name)
                .IsRequired();
        }
    }
}
