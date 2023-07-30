using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee : BaseEntity<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateOnly Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; } = true;
        public virtual Department Department { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
