using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Common
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }

        public static Result Success()
        {
            return new Result { Succeeded = true };
        }

        public static Result Success(string message)
        {
            return new Result { Succeeded = true, Message = message };
        }

        public static Result Failure(params string[] errors)
        {
            return new Result { Succeeded = false, Errors = errors };
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }

        public static Result<T> Success(T data, string message)
        {
            return new Result<T> { Succeeded = true, Data = data, Message = message };
        }

        public static new Result<T> Failure(params string[] errors)
        {
            return new Result<T> { Succeeded = false, Errors = errors };
        }
    }
}