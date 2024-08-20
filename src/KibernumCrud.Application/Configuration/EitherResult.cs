namespace KibernumCrud.Application.Configuration;

public sealed class EitherResult<TSuccess, TError>
{
    private readonly TSuccess? _success;
    private readonly TError? _error;
    
    public TSuccess? Value => _success;
    public TError? Error => _error;
    public bool IsSuccess => _error is null;
    
    private EitherResult(TSuccess success)
    {
        _success = success;
    }

    private EitherResult(TError error)
    {
        _error = error;
    }
    
    public T Match<T>(Func<TSuccess, T> success, Func<TError, T> error)
    {
        return IsSuccess ? success(_success!) : error(_error!); 
    }
    
    public static implicit operator EitherResult<TSuccess, TError>(TSuccess value)
    {
        return new EitherResult<TSuccess, TError>(value);
    }
    
    public static implicit operator EitherResult<TSuccess, TError>(TError error)
    {
        return new EitherResult<TSuccess, TError>(error);
    }
}