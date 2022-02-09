using System;

namespace Awarean.Sdk.Result
{
    public class Error
    {
        private Error() { }

        private Error(string code, string message) => (Code, Message) = (code, message);

        public string Code { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;

        public static readonly Error Empty = Create("NO_ERROR", "No error ocurred.");

        public static Error Create(string code, string reason) => new Error(code, reason);
    }
}