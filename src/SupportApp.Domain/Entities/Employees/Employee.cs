using SupportApp.Domain.Common;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Employees;
using SupportApp.Domain.Entities.Identity;

namespace SupportApp.Domain.Entities.Employees
{
    public class Employee : AuditableEntity
    {
        public string? FirstName { get; }
        public string? LastName { get; }
        public Role Role { get; }
        public string FullName => $"{FirstName} {LastName}";

        private Employee() { }

        private Employee(Guid id, string firstName, string lastName, Role role)
        : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public static Result<Employee> Create(Guid id, string firstName, string lastName, Role role)
        {
            if (id == Guid.Empty)
            {
                return EmployeeErrors.IdRequired;
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                return EmployeeErrors.FirstNameRequired;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return EmployeeErrors.LastNameRequired;
            }

            if (!Enum.IsDefined(role))
            {
                return EmployeeErrors.RoleInvalid;
            }

            return new Employee(id, firstName.Trim(), lastName.Trim(), role);
        }
    }
}
