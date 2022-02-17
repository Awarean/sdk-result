using System;

namespace Awarean.Sdk.Result
{
    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(T value) : base(true, Error.Empty) => 
            Value = value ?? throw new ArgumentNullException(nameof(value));


        private Result(Error error) : base(false, error)
        {
            var genericType = typeof(T);

            if (genericType is null)
                return;

            if (genericType.IsClass is false)
                Value = default;

            else
                Value = (T) genericType.GetField("Empty")?.GetValue(null)
                    ?? throw new NotImplementedException($"No implementation of null object pattern for type {genericType.FullName} found.");

        }

        public static Result<T> Success(T value) => new Result<T>(value);

        public static new Result<T> Fail(string errorCode, string errorMessage) => new Result<T>(Error.Create(errorCode, errorMessage));
    }
}