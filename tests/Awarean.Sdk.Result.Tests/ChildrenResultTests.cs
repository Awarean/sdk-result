using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using static Awarean.Sdk.Result.Tests.Fixtures.Mocks.MockClasses;
using Awarean.Sdk.Result.Tests.Fixtures;

namespace Awarean.Sdk.Result.Tests;

public class ChildrenResultTests
{
    [Fact]
    public void Inheriting_Value_Result_Should_Allow_Custom_Typed_Result()
    {
        var @class = new MockingClassWithEmptyObject();
        var sut = new ChildrenResult(@class);

        sut.Value.Should().Be(@class);
        sut.Should().BeOfType<ChildrenResult>();
    }

    [Fact]
    public void Inheriting_Value_Result_Should_Allow_Error()
    {
        var error = Error.Create("TYPED_CHILDREN_RESULT_ERROR", "Typed_Children_Result_Error");
        var sut = new ChildrenResult(error);

        sut.Error.Should().Be(error);
        sut.IsFailed.Should().BeTrue();
    }
}
