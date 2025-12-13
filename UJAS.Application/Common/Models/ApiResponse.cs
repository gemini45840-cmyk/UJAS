namespace UJAS.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; } = 200;

        public static ApiResponse<T> SuccessResponse(T data, string message = "")
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message };
        }

        public static ApiResponse<T> FailureResponse(string message, List<string> errors = null, int statusCode = 400)
        {
            return new ApiResponse<T> { Success = false, Message = message, Errors = errors ?? new List<string>(), StatusCode = statusCode };
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
        public static new ApiResponse SuccessResponse(object data = null, string message = "")
        {
            return new ApiResponse { Success = true, Data = data, Message = message };
        }

        public static new ApiResponse FailureResponse(string message, List<string> errors = null, int statusCode = 400)
        {
            return new ApiResponse { Success = false, Message = message, Errors = errors ?? new List<string>(), StatusCode = statusCode };
        }
    }

    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedResponse(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
