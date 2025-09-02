using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Identity.User
{
    public class UserErrors
    {
        public static readonly Error NameRequired =
        Error.Validation("User.Name.Required", "User Name is required.");

        public static readonly Error EmailRequired =
        Error.Validation("User.Email.Required", "User Email is required.");

        public static Error EmailInvalid =>
          Error.Validation("Client_Email_Invalid", "Email is invalid");

        public static readonly Error EmailExists =
        Error.Validation("User.Email.Exists", "User Email is Exists.");

        public static readonly Error PhoneRequired =
        Error.Validation("User.Phone.Required", "User Phone is required.");

        public static readonly Error PhoneExists =
        Error.Validation("User.Phone.Exists", "User Phone is Exists.");

        public static readonly Error InvalidPhoneNumber =
            Error.Conflict("User.Phone.InvalidPhoneNumber", "Phone number must be 7–15 digits and may start with '+'.");

        public static Error PasswordRequired =>
           Error.Validation("Client_Password_Required", "Client password is required");

        public static readonly Error CannotDeleteClientWithTickets =
            Error.Conflict("Client.CannotDelete", "Client cannot be deleted due to existing tickets.");

        public static readonly Error PasswordOrEmailError =
        Error.Validation("User.Name.Required", "PasswordOrEmailError");
    }
}
