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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            const string DepartmentId = "43db034a-98cc-42ee-8fff-c57016484fdd";
            builder.Property(u => u.DepartmentCode)
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(u => u.Description)
                .IsRequired();
            builder.Property(u => u.TenantId)
                .IsRequired();

            builder.HasData(
                new Department
                {
                    Id = Guid.Parse(DepartmentId),
                    DepartmentCode = "A000",
                    Description = "Testing",
                    TenantId = 1
                }
            );
        }
    }
}
