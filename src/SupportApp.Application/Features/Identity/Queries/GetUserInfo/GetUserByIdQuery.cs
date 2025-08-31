using MediatR;
using SupportApp.Application.Features.Identity.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Identity.Queries.GetUserInfo;

public sealed record GetUserByIdQuery(string? UserId) : IRequest<Result<AppUserDto>>;