namespace SimpelKodeordsmanager.Application.Contracts.Interfaces.Shared;

public interface IRequestHandler<in T> where T : class
{
    Task InvokeAsync(T request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<in TIn, TOut> where TIn : class where TOut : class
{
    Task<TOut> InvokeAsync(TIn request, CancellationToken cancellationToken = default);
}

public interface IResponseHandler<T> where T : class
{
    Task<T> InvokeAsync(CancellationToken cancellationToken = default);
}