using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(u => u.EmployeeId)
                .IsRequired();
            builder.Property(u => u.UserName)
                .HasMaxLength(50);
            builder.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(u => u.Email)
                .IsRequired();
        }
    }
}
