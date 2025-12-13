namespace UJAS.Application.Common.DTOs
{
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponseDto<T> SuccessResult(T data, string message = null)
            => new() { Success = true, Data = data, Message = message };

        public static ApiResponseDto<T> FailureResult(string message, List<string> errors = null)
            => new() { Success = false, Message = message, Errors = errors ?? new List<string>() };
    }

    public class ApiResponseDto : ApiResponseDto<object>
    {
        public static ApiResponseDto SuccessResult(string message = null)
            => new() { Success = true, Message = message };

        public static ApiResponseDto FailureResult(string message, List<string> errors = null)
            => new() { Success = false, Message = message, Errors = errors ?? new List<string>() };
    }
}