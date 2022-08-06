using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using System.Collections;
using static Awarean.Sdk.Result.Tests.Fixtures.Mocks.MockClasses;
using System.Linq;

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
        public void Failed_String_Results_Should_Be_Empty()
        {
            var result = Result.Fail<string>("test_code", "test_message");

            result.Value.Should().Be(string.Empty);
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

        [Theory]
        [MemberData(nameof(CollectionsGenerator))]
        public void Failed_Result_For_Collection_Should_Have_Empty_Collection(Type collectionType, Type elementType)
        {
            var collectionGenericType = collectionType.MakeGenericType(elementType);
            var testInfo = typeof(ValueResultTests).GetMethod(nameof(TestFailedResultsForCollections));

            testInfo.MakeGenericMethod(collectionGenericType, elementType).Invoke(this, null);
        }

        [Fact]
        private void Primitive_Typed_Failed_Results_Should_Be_Default_Value()
        {
            Execute_Primitive_TypedTests<decimal>();
            Execute_Primitive_TypedTests<long>();
            Execute_Primitive_TypedTests<int>();
            Execute_Primitive_TypedTests<char>();
        }

        public void Execute_Primitive_TypedTests<T>()
        {
            var result = Result.Fail<T>($"{typeof(T).Name}_Failed", $"Error when creating failed result for primitive type {typeof(T)}");

            result.Value.Should().Be(default(T));
        }

        public void TestFailedResultsForCollections<T, V>() where T : IEnumerable<V>
        {
            var error = Error.Create("Test_Failed_Code_IEnumerable", "Test meant for failing result with collection");
            var result = Result<T>.Fail(error);

            var expected = Enumerable.Empty<V>();
            result.Value.Should().BeEmpty();
            result.Value.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> CollectionsGenerator()
        {
            yield return new object[] { typeof(List<>), typeof(MockingClass)};
            yield return new object[] { typeof(HashSet<>), typeof(MockingClass)};
            yield return new object[] { typeof(LinkedList<>), typeof(MockingClass)};
            yield return new object[] { typeof(Queue<>), typeof(MockingClass)};
            yield return new object[] { typeof(Stack<>), typeof(MockingClass)};
        }
    }
}