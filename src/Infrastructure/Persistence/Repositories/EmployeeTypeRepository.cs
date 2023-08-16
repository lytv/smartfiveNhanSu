using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class EmployeeTypeRepository : EfEntityRepository<EmployeeType, CAContext, Guid>, IEmployeeTypeRepository
    {
        public EmployeeTypeRepository(CAContext context) : base(context)
        {
        }

        public async Task<EmployeeType> GetEmployeeTypeByIdAsync(Guid id)
        {
            return await _context.EmployeeTypes.SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
