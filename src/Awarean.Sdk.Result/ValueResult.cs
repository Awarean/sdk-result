using System;

namespace Awarean.Sdk.Result
{
    public class Result<T> : Result
    {
        public const string EMPTY_OBJECT_NAME = "Empty";
        public const string NULL_OBJECT_NAME = "Null";

        private const string NOT_IMPLEMENTED_NULL_OBJECT_MESSAGE = "No implementation of null object pattern for type {0} found. " +
            "Please implement a public static readonly field named \"Null\" or named \"Empty\" for type {1}.";

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
            {
                Value = (T)genericType.GetField(EMPTY_OBJECT_NAME)?.GetValue(null)
                    ?? (T)genericType.GetField(NULL_OBJECT_NAME)?.GetValue(null)
                    ?? throw new NotImplementedException(string.Format(NOT_IMPLEMENTED_NULL_OBJECT_MESSAGE, genericType.Name, genericType.Name));
            }
        }

        public static Result<T> Success(T value) => new Result<T>(value);

        public static new Result<T> Fail(string errorCode, string errorMessage) => new Result<T>(Error.Create(errorCode, errorMessage));
    }
}
