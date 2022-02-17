using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using static Awarean.Sdk.Result.Tests.Fixtures.Mocks.MockClasses;

namespace Awarean.Sdk.Result.Tests
{
    public class ValueResultTests
    {
        [Theory]
        [MemberData(nameof(EmptyObjectGenerator))]
        public void Failed_Result_Should_Have_Empty_Value_If_Available(object obj)
        {
            var result = Result<Error>.Fail("test_code", "test_message");

            result.Value.Should().Be(Error.Empty);
        }

        [Theory]
        [MemberData(nameof(NoEmptyObjectGenerator))]
        public void Not_Implemented_Empty_Object_Patterns_Should_ThrowException(object obj)
        {
            var throwAction = new Action(() => Result<MockingClass>.Fail("test_code", "test_message"));

            throwAction.Should().Throw<NotImplementedException>();
        }

        public static IEnumerable<object[]> NoEmptyObjectGenerator()
        {
            yield return new object[] { new MockingClass() as object };
        }

        public static IEnumerable<object[]> EmptyObjectGenerator()
        {
            yield return new object[] { Error.Empty as object };
        }
    }
}