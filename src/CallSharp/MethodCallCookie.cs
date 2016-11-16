namespace CallSharp
{
  using System;
  using System.Linq;
  using System.Reflection;
  using System.Text;

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
      return "x";
    }

    /// <summary>
    /// Renders the function call on <c>subject</c>. The rendering is different depending
    /// on whether or not the function is static.
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public string ToString(string subject)
    {
      var sb = new StringBuilder();
      var methodParams = MethodCalled.GetParameters();

      // we either called it on a member . or on static X.
      if (MethodCalled.IsStatic)
        sb.Append(MethodCalled.DeclaringType.GetFriendlyName());
      else
        sb.Append(subject);
      sb.Append(".");

      if (MethodCalled.Name.StartsWith("get_"))
      {
        // just a property
        sb.Append(MethodCalled.Name.Substring(4));
      }
      else
      {
        sb.Append(MethodCalled.Name).Append("(");

        int start = MethodCalled.IsStatic ? 1 : 0;
        for (int i = start; i < Arguments.Length; i++)
        {
          var arg = Arguments[i];
          bool isParams = methodParams[i].IsParams();
          
          // caveat: calling a params[] really passes in a single
          // 0-sized array :( need special handling
          var arr = arg as Array;
          if (arr != null && arr.Length == 0)
            break;

          // todo: literalize argument into code
          if (arg is string)
          {
            string s = (string) arg;
            if (s.Length == 0)
              sb.Append("string.Empty");
            else
              sb.AppendFormat("\"{0}\"", arg);
          }
          else if (arg is char)
          {
            sb.AppendFormat("\'{0}'", arg);
          }
          else if (arg is char[])
          {
            if (!isParams) sb.Append("new char[]{");
            sb.Append(string.Join(",", ((char[]) arg).Select(c => "'" + c + "'")));
            if (!isParams) sb.Append("}");
          }
          else
          {
            sb.Append(arg);
          }

          if (i+1 != Arguments.Length)
            sb.Append(", ");
        }

        // on the other hand, a static call has NO arguments, so...
        if (MethodCalled.IsStatic)
          sb.Append(subject);

        sb.Append(")");
      }
      return sb.ToString();
    }
  }
}