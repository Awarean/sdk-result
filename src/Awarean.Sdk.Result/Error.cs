using System;

namespace Awarean.Sdk.Result
{
    public class Error
    {
        public const string DefaultErrorCode = "NO_ERROR_OCURRED";
        public const string DefaultErrorMessage = "Empty error.";

        private Error() { }

        private Error(string code, string message) => 
            (Code, Message) = (code ?? throw new ArgumentNullException(nameof(code)), message ?? throw new ArgumentNullException(nameof(Message)));

        public string Code { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;

        public static Error Create(string code, string reason) => new Error(code, reason);

        public static readonly Error Empty = Create(DefaultErrorCode, DefaultErrorMessage);
    }
}
