using System.Net.Mail;
using System.Text.RegularExpressions;

using SupportApp.Domain.Common;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Tickets;

namespace SupportApp.Domain.Entities.Clients
{
    public class Client : AuditableEntity
    {
        public string? Name { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string? PasswordHash { get; private set; }

        private readonly List<Ticket> _tickets = [];
        public IEnumerable<Ticket> Tickets => _tickets.AsReadOnly();
        private Client()
        {
        }

        private Client(Guid id, string name, string phoneNumber, string email, string passwordHash)
            : base(id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static Result<Client> Create(Guid id, string name, string phoneNumber, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return ClientErrors.NameRequired;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$"))
            {
                return ClientErrors.InvalidPhoneNumber;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return ClientErrors.EmailRequired;
            }

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                return ClientErrors.EmailInvalid;
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                return ClientErrors.PasswordRequired;
            }

            return new Client(id, name, phoneNumber, email,passwordHash);
        }

        public Result<Updated> Update(string name, string email, string phoneNumber, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return ClientErrors.NameRequired;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$"))
            {
                return ClientErrors.InvalidPhoneNumber;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return ClientErrors.EmailRequired;
            }

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                return ClientErrors.EmailInvalid;
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                return ClientErrors.PasswordRequired;
            }

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;

            return Result.Updated;
        }

    }
}
