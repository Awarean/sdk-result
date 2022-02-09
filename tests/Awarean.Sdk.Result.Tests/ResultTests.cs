using Xunit;
using FluentAssertions;

namespace Awarean.Sdk.Result.Tests;

public class ResultTests
{
    [Fact]
    public void Sucess_Method_Should_Create_Sucess_Result_With_Empty_Error()
    {
        var result = Result.Success();

        result.Error.Should().Be(Error.Empty);
    }

    [Fact]
    public void Create_Sucess_Result_Should_Have_Truthy_IsSuccess()
    {
        var result = Result.Success();

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Create_Sucess_Result_Should_Have_Falsy_IsFailed()
    {
        var result = Result.Success();

        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public void Fail_Method_With_Error_Should_Create_Fail_Result_With_Defined_Error()
    {
        var expectedError = Error.Create("My_Test_Error_Code", "This error is built for test reasons");

        var result = Result.Fail(expectedError);

        result.Error.Should().Be(expectedError);
    }

    [Fact]
    public void Fail_Method_With_Error_Message_Should_Create_Fail_Result_With_Defined_Error()
    {
        var (expectedCode, expectedMessage) = ("My_Test_Error_Code", "This error is built for test reasons");

        var result = Result.Fail(expectedCode, expectedMessage);

        result.Error.Should().NotBeNull();
        result.Error.Should().NotBe(Error.Empty);

        result.Error.Code.Should().Be(expectedCode);
        result.Error.Message.Should().Be(expectedMessage);
    }

    [Fact]
    public void Create_Fail_Result_Should_Have_Falsy_IsSuccess()
    {
        var result = Result.Fail(string.Empty, string.Empty);

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Create_Fail_Result_Should_Have_Truthy_IsFailed()
    {
        var result = Result.Fail(string.Empty, string.Empty);

        result.IsFailed.Should().BeTrue();
    }
}
