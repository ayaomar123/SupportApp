using System.Net.Mail;
using System.Text.RegularExpressions;

using SupportApp.Domain.Common;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Tickets;

namespace SupportApp.Domain.Entities.Identity.User
{
    public enum UserType
    {
        Employee,
        Client
    }

    public class User : AuditableEntity
    {
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? PasswordHash { get; private set; }
        public string? PhoneNumber { get; private set; }
        public UserType UserType { get; private set; } = UserType.Client;

        private readonly List<Ticket> _tickets = [];
        public IEnumerable<Ticket> Tickets => _tickets.AsReadOnly();

        public User()
        {
        }

        public User(Guid id, string? name, string? email, string? passwordHash, string? phoneNumber)
            : base(id)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
        }

        public static Result<User> Create(Guid id, string name, string email, string passwordHash, string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return UserErrors.NameRequired;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$"))
            {
                return UserErrors.InvalidPhoneNumber;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return UserErrors.EmailRequired;
            }

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                return UserErrors.EmailInvalid;
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                return UserErrors.PasswordRequired;
            }

            return new User(id, name, email, passwordHash, phoneNumber);
        }
    }
}
