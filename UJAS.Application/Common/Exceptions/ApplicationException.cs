using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Common.Exceptions
{
    public class ApplicationException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

        public ApplicationException(string message, string errorCode = null, int statusCode = 400)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        public ApplicationException(string message, Exception innerException, string errorCode = null, int statusCode = 400)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }

    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            : base(message, "NOT_FOUND", 404)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.", "NOT_FOUND", 404)
        {
        }
    }

    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.", "VALIDATION_ERROR", 400)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : this()
        {
            Errors = errors;
        }

        public ValidationException(IEnumerable<ValidationError> errors)
            : this()
        {
            Errors = errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());
        }
    }

    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException(string message = "Unauthorized")
            : base(message, "UNAUTHORIZED", 401)
        {
        }
    }

    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException(string message = "Forbidden")
            : base(message, "FORBIDDEN", 403)
        {
        }
    }
}