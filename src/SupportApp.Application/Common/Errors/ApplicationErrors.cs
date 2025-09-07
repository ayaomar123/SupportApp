using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static Error CategoryNotFound => Error.NotFound(
           "ApplicationErrors.Category.NotFound",
           "Category does not exist.");

        public static Error InvalidRefreshToken => Error.Validation(
            "RefreshToken.Expiry.Invalid",
            "Expiry must be in the future.");

        public static readonly Error ExpiredAccessTokenInvalid = Error.Conflict(
         code: "Auth.ExpiredAccessToken.Invalid",
         description: "Expired access token is not valid.");

        public static readonly Error UserIdClaimInvalid = Error.Conflict(
            code: "Auth.UserIdClaim.Invalid",
            description: "Invalid userId claim.");

        public static readonly Error RefreshTokenExpired = Error.Conflict(
            code: "Auth.RefreshToken.Expired",
            description: "Refresh token is invalid or has expired.");

        public static readonly Error UserNotFound = Error.NotFound(
            code: "Auth.User.NotFound",
            description: "User not found.");

        public static readonly Error TicketNotFound = Error.NotFound(
            code: "Ticket.id.NotFound",
            description: "Ticket not found.");

        public static readonly Error TokenGenerationFailed = Error.Failure(
            code: "Auth.TokenGeneration.Failed",
            description: "Failed to generate new JWT token.");
    }
}
