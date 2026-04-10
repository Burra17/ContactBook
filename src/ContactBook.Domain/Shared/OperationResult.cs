namespace ContactBook.Domain.Shared;

// Represents the outcome of an operation without returning data
public class OperationResult
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public bool IsFailure => !IsSuccess;
    public bool IsNotFound { get; }

    protected OperationResult(bool isSuccess, string? errorMessage, bool isNotFound = false)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        IsNotFound = isNotFound;
    }

    public static OperationResult Success() => new(true, null);
    public static OperationResult Failure(string errorMessage) => new(false, errorMessage);
    public static OperationResult NotFound(string errorMessage) => new(false, errorMessage, isNotFound: true);
}

// Represents the outcome of an operation that returns data
public class OperationResult<T> : OperationResult
{
    public T? Data { get; }

    private OperationResult(bool isSuccess, T? data, string? errorMessage, bool isNotFound = false)
        : base(isSuccess, errorMessage, isNotFound)
    {
        Data = data;
    }

    public static OperationResult<T> Success(T data) => new(true, data, null);
    public new static OperationResult<T> Failure(string errorMessage) => new(false, default, errorMessage);
    public new static OperationResult<T> NotFound(string errorMessage) => new(false, default, errorMessage, isNotFound: true);
}
