namespace Awarean.Sdk.Result
{
    public class Result
    {
        private readonly bool isSuccess;
        protected Result(bool isSuccess, Error error) => (this.isSuccess, Error) = (isSuccess, error);

        public bool IsSuccess { get => isSuccess; }

        public bool IsFailed { get => isSuccess is false; }

        public virtual Error Error { get; protected set; }

    #region Base Result Creation Methods
        public static Result Success() => new Result(isSuccess: true, error: Error.Empty);
        public static Result Fail(string code, string reason) => new Result(isSuccess: false, Error.Create(code, reason));
        public static Result Fail(Error error) => new Result(isSuccess: false, error);
    #endregion

    #region Typed Result Creation Methods
        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
        public static Result<T> Fail<T>(Error error) => Result<T>.Fail(error);
        public static Result<T> Fail<T>(string code, string reason) => Result<T>.Fail(code, reason);
    #endregion
    }
}
