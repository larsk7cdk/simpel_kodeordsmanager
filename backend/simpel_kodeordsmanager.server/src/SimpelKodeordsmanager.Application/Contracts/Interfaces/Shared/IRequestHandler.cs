using System.Security.Claims;

namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

public interface IRequestHandler<in T> where T : class
{
    Task InvokeAsync(T request, ClaimsPrincipal user, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<in TIn, TOut> where TIn : class where TOut : class
{
    Task<TOut> InvokeAsync(TIn request, ClaimsPrincipal user, CancellationToken cancellationToken = default);
}