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
    public class EmployeeTypeConfiguration : IEntityTypeConfiguration<EmployeeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeType> builder)
        {
            const string EmployeeTypeId = "43db034a-98cc-42ee-8fff-c5701648dddd";
            builder.Property(u => u.EmployeeTypeCode)
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(u => u.Description)
                .IsRequired();
            builder.Property(u => u.TenantId)
                .IsRequired();

            builder.HasData(
                new EmployeeType
                {
                    Id = Guid.Parse(EmployeeTypeId),
                    EmployeeTypeCode = "A0000",
                    Description = "Testing",
                    TenantId = 1
                }
            );
        }
    }
}
