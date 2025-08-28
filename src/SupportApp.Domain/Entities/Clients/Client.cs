using System.Net.Mail;
using System.Text.RegularExpressions;

using SupportApp.Domain.Common;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Clients
{
    public class Client : Entity
    {
        public string? Name { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }

        private Client()
        {
        }

        private Client(Guid id,string? name, string? phoneNumber, string? email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public static Result<Client> Create(Guid id, string name, string phoneNumber, string email)
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

            return new Client(id, name, phoneNumber, email);
        }

        public Result<Updated> Update(string name, string email, string phoneNumber)
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

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;

            return Result.Updated;
        }

    }
}
