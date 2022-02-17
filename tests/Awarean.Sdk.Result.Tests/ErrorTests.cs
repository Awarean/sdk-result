using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Awarean.Sdk.Result.Tests
{
    public class ErrorTests
    {
        [Fact]
        public void Creating_Error_Should_Return_New_Error()
        {
            var error = Error.Create(code: "NEW_TEST_ERROR", reason: "Test reason for error");

            error.Should().BeAssignableTo<Error>();
        }

        [Fact]
        public void Error_Empty_Should_Have_Empty_Properties()
        {
            var error = Error.Empty;

            error.Code.Should().Be(Error.DefaultErrorCode);
            error.Message.Should().Be(Error.DefaultErrorMessage);
        }
    }
}
