using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Data.Configrations
{
    internal class EmployeeConfigrations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Fluent APIs For Employee Domain

            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired(true);
            builder.Property(E => E.Address).IsRequired(true);
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
           
            builder.Property(E => E.Gender)
               .HasConversion<string>();
        }
    }
}
