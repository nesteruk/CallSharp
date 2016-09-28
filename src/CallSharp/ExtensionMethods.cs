using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CallSharp
{
  public static class ExtensionMethods
  {
    private static Type[] parseableTypes = new[]
    {
      typeof(DateTime),
      typeof(TimeSpan),
      typeof(double),
      typeof(float),
      typeof(decimal),
      typeof(double),
      typeof(float),
      typeof(long),
      typeof(ulong),
      typeof(int),
      typeof(uint),
      typeof(short),
      typeof(ushort),
      typeof(byte),
      typeof(sbyte),
    };

    private static IEnumerable<Type> AllTypes => parseableTypes;

    public static bool AllAreOptional(this ParameterInfo[] ps)
    {
      return ps.All(p => p.IsOptional);
    }

    public static bool IsParams(this ParameterInfo pi)
    {
      return pi.GetCustomAttribute<ParamArrayAttribute>()
             != null;
    }

    public static object InvokeWithSingleArgument<T>(this MethodInfo mi, T subject)
    {
      var pars = mi.GetParameters();
      if (pars.IsSingleParamsArgument())
        return mi.Invoke(subject, new[]
        {
          Activator.CreateInstance(pars[0].ParameterType.UnderlyingSystemType, 0)
        });
      else
      {
        return mi.Invoke(subject, new object[] {});
      }
    }

    public static bool IsSingleParamsArgument(this ParameterInfo[] ps)
    {
      return ps.Length == 1 && ps[0].IsParams();
    }

    public static IReadOnlyList<Type> InferTypes(this string text)
    {
      var result = new List<Type>();

      foreach (var type in parseableTypes)
      {
        foreach (var m in type.GetMethods().Where(
          x => x.Name.Equals("TryParse") 
          && x.GetParameters().Length == 2))
        {
          var _ = Activator.CreateInstance(type);
          bool ok = (bool) m.Invoke(null, new[] {text, _});
          if (ok)
          {
            result.Add(type);
          }
        }
      }
      return result;
    }
  }
}