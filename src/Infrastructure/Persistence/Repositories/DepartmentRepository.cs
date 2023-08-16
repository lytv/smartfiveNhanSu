using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class DepartmentRepository : EfEntityRepository<Department, CAContext, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(CAContext context) : base(context)
        {
        }

        public async Task<Department> GetDepartmentByIdAsync(Guid id)
        {
            return await _context.Departments.SingleOrDefaultAsync(d => d.Id == id);
        }
    }
}
