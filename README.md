# sdk-result
Library designed to provide a fluent and universal way to provide results with data and errors.

example:
```CSharp
// Success Result of T.
public async Task<Result<T>> DoSomeWorkWithSuccess<T>() where T: new()  => Result<T>.Success(new T());
//Fail Result of T.
public async Task<Result<T>> DoSomeWorkWithFail<T>() => Result<T>.Fail("OPERATION_FAILED", "The operation Failed with for any reason");
```

And with that we're able to receive responses from methods like this:

```CSharp
var result = DoSomeWorkWithSuccess<MyClass>();

if(result.IsSuccess)
  return DoAnyOtherThing(result.Value);
```

```CSharp
var result = DoSomeWorkWithFail<MyClass>();

if(result.IsFailed)
  return HandleErrorResult(result.Error);
```

### Exceptions

Value Result(AKA. Result<T>) enforces implementing ![NULL OBJECT PATTERN](https://refactoring.guru/introduce-null-object)
so when returning a failed result ex:
```CSharp
  Result<MyClass>.Fail("OPERATION_FAILED", "The operation Failed with for any reason");
```

It is mandatory to implement a public static readonly field called either Empty or Null as follow

````csharp
public class MyClass
{
    // Example of a property that cannot be less than one.
    private int _quantity = 0;
    public int Quantity 
    { 
      get => _quantity; 
      private set => _quantity = value >= 0 ? value : throw new ArgumentException("Value Should Be Greater than zero); 
    }
    
    // Empty object with an invalid state so it is not used by application logic.
    public static readonly MyClass Empty = new MyClass(){ _quantity = -1 };
}
```

