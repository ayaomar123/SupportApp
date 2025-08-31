using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Clients
{
    public class ClientErrors
    {
        public static Error NameRequired =>
           Error.Validation("Client_Name_Required", "Client name is required");

        public static Error EmailRequired =>
            Error.Validation("Client_Email_Required", "Email is required");

        public static Error EmailInvalid =>
          Error.Validation("Client_Email_Invalid", "Email is invalid");

        public static Error ClientEmailExists =>
            Error.Conflict("Client_Email_Exists", "A client with this email already exists.");

        public static Error PhoneNumberRequired =>
            Error.Validation("Client_Number_Required", "Phone number is required");
        public static Error ClientPhoneNumberExists =>
            Error.Conflict("Client_PhoneNumber_Exists", "A client with this Phone number already exists.");

        public static readonly Error InvalidPhoneNumber =
            Error.Conflict("Client.InvalidPhoneNumber", "Phone number must be 7–15 digits and may start with '+'.");

        public static readonly Error CannotDeleteClientWithTickets =
            Error.Conflict("Client.CannotDelete", "Client cannot be deleted due to existing tickets.");

        public static Error PasswordRequired =>
           Error.Validation("Client_Password_Required", "Client password is required");
    }
}
