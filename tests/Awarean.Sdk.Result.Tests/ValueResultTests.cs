using FluentAssertions;
using System;
using Xunit;
using static Awarean.Sdk.Result.Tests.Fixtures.Mocks.MockClasses;

namespace Awarean.Sdk.Result.Tests
{
    public class ValueResultTests
    {
        [Fact]
        public void Failed_Result_Should_Have_Empty_Value_If_Available()
        {
            var result = Result<Error>.Fail("test_code", "test_message");

            result.Value.Should().Be(Error.Empty);
        }

        [Fact]
        public void Success_Result_Should_Correct_Value()
        {
            var expected = new MockingClass();
            var result = Result<MockingClass>.Success(expected);

            result.Value.Should().Be(expected);
            result.Error.Should().Be(Error.Empty);
        }

        [Fact]
        public void Success_Result_Should_NotBe_Empty_Object()
        {
            var expected = new MockingClassWithEmptyObject();
            var result = Result<MockingClassWithEmptyObject>.Success(expected);

            result.Value.Should().NotBe(MockingClassWithEmptyObject.Empty);
        }

        [Fact]
        public void Success_Result_Should_NotBe_Null_Object()
        {
            var expected = new MockingClassWithNullObject();
            var result = Result<MockingClassWithNullObject>.Success(expected);

            result.Value.Should().NotBe(MockingClassWithNullObject.Null);
        }

        [Fact]
        public void Not_Implemented_Null_Object_Patterns_Should_Require_Implementation()
        {
            var throwAction = new Action(() => Result<MockingClass>.Fail("test_code", "test_message"));

            throwAction.Should().Throw<NotImplementedException>()
                .WithMessage($"No implementation of null object pattern for type {nameof(MockingClass)} found." +
                    $"*Please implement a public static readonly field named \"Null\" or * \"Empty\" for type {nameof(MockingClass)}*");
        }

        [Fact]
        public void Failed_Result_Should_Be_Of_Correct_Type()
        {
            var expected = Error.Create("Test_Failed_Code", "Test meant for failing");
            var result = Result<Error>.Fail(expected);

            result.Error.Should().Be(expected);
            result.Should().BeOfType<Result<Error>>();
        }
    }
}