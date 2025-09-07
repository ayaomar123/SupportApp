using SupportApp.Domain.Entities.Identity.User;

namespace SupportApp.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }

    UserType? UserType { get; }
}