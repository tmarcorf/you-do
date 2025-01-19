using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDo.Application.Results
{
    public class Result<T>
    {
        private Result(T data, bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            Data = data;
            IsSuccess = isSuccess;
            Error = error;
        }

        private Result(Error error)
        {
            Data = default;
            IsSuccess = false;
            Error = error;
        }

        public T Data { get; }

        public bool IsSuccess { get; }

        public Error Error { get; }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data, true, Error.None);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(error);
        }
    }
}
