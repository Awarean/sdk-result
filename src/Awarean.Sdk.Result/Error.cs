using System;

namespace Awarean.Sdk.Result
{
    public class Error : IEquatable<Error>
    {
        public const string DefaultErrorCode = "NO_ERROR_OCURRED";
        public const string DefaultErrorMessage = "Empty error.";

        private Error() { }

        protected Error(string code, string reason) => 
            (Code, Message) = (code ?? throw new ArgumentNullException(nameof(code)), reason ?? throw new ArgumentNullException(nameof(reason)));

        public string Code { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;

        public static Error Create(string code, string reason) => new Error(code, reason);

        public bool Equals(Error other)
        {
            if (other is null) return false;

            return other.Code.Equals(Code) && other.Message.Equals(Message);
        }

        public static readonly Error Empty = Create(DefaultErrorCode, DefaultErrorMessage);
    }
}
