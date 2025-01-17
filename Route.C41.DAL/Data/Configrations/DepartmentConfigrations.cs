﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.DAL.Data.Configrations
{
    internal class DepartmentConfigrations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Fluent APIs For "Department" Model | Domain | Class
            builder.Property(D=>D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired(true);
            builder.Property(D => D.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired(true);

            builder.HasMany(D => D.Employees)
                .WithOne(E=>E.Department)
                .OnDelete(DeleteBehavior.Cascade);
      
        }
    }
}
