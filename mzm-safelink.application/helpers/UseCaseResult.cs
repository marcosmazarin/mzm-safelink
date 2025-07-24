namespace mzm_safelink.application.helpers
{
    public class UseCaseResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string? ErrorCode { get; private set; }

        private UseCaseResult(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        private UseCaseResult(string message, T data)
        {
            IsSuccess = true;
            Data = data;
            Message = message;
        }

        private UseCaseResult(string errorMessage, string? errorCode = null)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public static UseCaseResult<T> Success(T data) => new UseCaseResult<T>(data);
        public static UseCaseResult<T> Success(string message, T data) => new UseCaseResult<T>(message, data);
        public static UseCaseResult<T> Failure(string errorMessage, string? errorCode = null) => new UseCaseResult<T>(errorMessage, errorCode);
    }    
}