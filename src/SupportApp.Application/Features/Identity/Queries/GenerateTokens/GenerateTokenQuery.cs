using MediatR;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Identity.Queries.GenerateTokens;

public record GenerateTokenQuery(
    string Email,
    string Password) : IRequest<Result<TokenResponse>>;