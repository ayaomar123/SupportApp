using MediatR;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Identity.Queries.RefreshTokens;

public record RefreshTokenQuery(string RefreshToken, string ExpiredAccessToken) : IRequest<Result<TokenResponse>>;