# sdk-result
Library designed to provide a fluent and universal way to provide results with data and errors.

example:
```CSharp
// Success Result of T.
public async Task<Result<T>> DoSomeWorkWithSuccess<T>() where T: new()  => Result<T>.Success(new T("this is a demonstration"));
//Fail Result of T.
public async Task<Result<T>> DoSomeWorkWithFail<T>() where T: new()  => Result<T>.Fail("OPERATION_FAILED", "The operation Failed with for any reason");
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

