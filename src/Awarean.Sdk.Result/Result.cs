namespace Awarean.Sdk.Result
{
    public class Result
    {
        private readonly bool isSuccess;
        protected Result(bool isSuccess, Error error) => (this.isSuccess, Error) = (isSuccess, error);

        public bool IsSuccess { get => isSuccess; }

        public bool IsFailed { get => isSuccess is false; }

        public Error Error { get; private set; }

        public static Result Success() => new Result(isSuccess: true, error: Error.Empty);

        public static Result Fail(string code, string reason) => new Result(isSuccess: false, Error.Create(code, reason));
        public static Result Fail(Error error) => new Result(isSuccess: false, error);
    }
}
