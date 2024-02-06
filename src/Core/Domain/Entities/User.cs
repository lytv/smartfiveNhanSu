using Domain.Entities.Base;

namespace Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool EmailConfirmed { get; set; } = true;
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public string EmailConfirmationCode { get; set; }
        public string EmailConfirmedCode { get; set; }
        public string ResetPasswordCode { get; set; }
        public DateOnly Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public virtual Department Department { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public string EmployeeCode { get; set; }
        public int TenantId { get; set; } = 1;
    }
}