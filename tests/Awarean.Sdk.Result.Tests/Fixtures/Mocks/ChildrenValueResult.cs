using static Awarean.Sdk.Result.Tests.Fixtures.Mocks.MockClasses;

namespace Awarean.Sdk.Result.Tests.Fixtures;

public class ChildrenResult : Result<MockingClassWithEmptyObject>
{
    public ChildrenResult(MockingClassWithEmptyObject value) : base(value)
    {
    }

    public ChildrenResult(Error error) : base(error)
    {
    }
}
