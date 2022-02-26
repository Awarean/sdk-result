using System;
namespace Awarean.Sdk.Result.Tests.Fixtures.Mocks
{
    public class MockClasses
    {
        public class MockingClass
        {
            public string MockProperty1 { get; set; } = String.Empty;
            public string MockProperty2 { get; set; } = String.Empty;
        }

        public class MockingClassWithEmptyObject
        {
            public static readonly MockingClassWithEmptyObject Empty = new MockingClassWithEmptyObject();
        }

        public class MockingClassWithNullObject
        {
            public static readonly MockingClassWithEmptyObject Null = new MockingClassWithEmptyObject();
        }
    }
}
