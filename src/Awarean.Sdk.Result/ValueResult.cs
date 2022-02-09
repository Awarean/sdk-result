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

            if(genericType.IsClass)
                Value = (T) genericType.GetField("Empty").GetValue(null) 
                    ?? throw new ArgumentNullException(nameof(Value), $"No implementation of empty object pattern for type {genericType.FullName} found.");
            
            Value = default;
        }

        public static Result<T> Success(T value) => new Result<T>(value);

        public static Result<T> Fail(string errorCode, string errorMessage) => new Result<T>(Error.Create(errorCode, errorMessage));
    }
}