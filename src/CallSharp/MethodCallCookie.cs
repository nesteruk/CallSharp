using System;
using System.Reflection;
using System.Text;

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

    public override string ToString()
    {
      var sb = new StringBuilder();

      // we either called it on a member . or on static X.
      if (MethodCalled.IsStatic)
        sb.Append(MethodCalled.DeclaringType.GetFriendlyName());
      sb.Append(".");

      if (MethodCalled.Name.StartsWith("get_"))
        sb.Append(MethodCalled.Name.Substring(4));
      else
      {
        sb.Append(MethodCalled.Name).Append("(");

        for (int i = 0; i < Arguments.Length; i++)
        {
          var arg = Arguments[i];
          
          // caveat: calling a params[] really passes in a single
          // 0-sized array :( need special handling
          var arr = arg as Array;
          if (arr != null && arr.Length == 0)
            break;

          // todo: literalize argument into code
          sb.Append(arg);

          if (i+1 != Arguments.Length)
            sb.Append(", ");
        }

        sb.Append(")");
      }
      return sb.ToString();
    }
  }
}