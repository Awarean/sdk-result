using Xunit;
using FluentAssertions;
using static Awarean.Sdk.Result.Tests.Fixtures.Mocks.MockClasses;

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

    [Fact]
    public void Creating_Typed_Failed_Result_Should_Work()
    {
        var expected = new MockingClass() { MockProperty1 = "Success", MockProperty2 = "Created from Result Class" };
        var result = Result.Success(expected);

        result.Should().BeOfType(typeof(Result<MockingClass>));
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expected);
    }

    [Fact]
    public void Creating_Typed_Error_Result_With_Using_Instance_Should_Pass()
    {
        var expected = MockingClassWithEmptyObject.Empty;
        var expectedError = Error.Create("TEST_ERROR", "Test error for typed result");
        var result = Result.Fail<MockingClassWithEmptyObject>(expectedError);

        result.Should().BeOfType(typeof(Result<MockingClassWithEmptyObject>));
        result.IsFailed.Should().BeTrue();
        result.Value.Should().Be(expected);
    }

    [Fact]
    public void Creating_Typed_Error_Result_Using_Messages_Should_Pass()
    {
        var expected = MockingClassWithEmptyObject.Empty;
        var result = Result.Fail<MockingClassWithEmptyObject>("TEST_ERROR", "Test error for typed result");

        result.Should().BeOfType(typeof(Result<MockingClassWithEmptyObject>));
        result.IsFailed.Should().BeTrue();
        result.Value.Should().Be(expected);
    }
}
