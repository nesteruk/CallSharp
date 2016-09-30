using System;
using System.Reflection;

namespace CallSharp
{
  /// <summary>
  /// This class contains all the information about an attempt to call
  /// a particular function on an input object. It contains information
  /// about the function called, the arguments that were applied and
  /// the resulting return value.
  /// </summary>
  public class MethodCallCookie
  {
    public MethodInfo MethodCalled;
    public object[] Arguments;
    public object ReturnValue;

    public MethodCallCookie(MethodInfo methodCalled, object[] arguments, object returnValue)
    {
      MethodCalled = methodCalled;
      Arguments = arguments;
      ReturnValue = returnValue;
    }

    /// <summary>
    /// The type of the return value.
    /// </summary>
    public Type ReturnType => ReturnValue.GetType();
  }
}