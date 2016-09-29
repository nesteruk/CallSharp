using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CallSharp
{
  public static class ExtensionMethods
  {
    public static bool AllAreOptional(this ParameterInfo[] ps)
    {
      return ps.All(p => p.IsOptional);
    }

    public static bool IsParams(this ParameterInfo pi)
    {
      return pi.GetCustomAttribute<ParamArrayAttribute>()
             != null;
    }

    public static object InvokeStaticWithSingleArgument<T>(this MethodInfo mi, T arg)
    {
      return mi.Invoke(null /*static*/, new object[] {arg});
    }

    public static object InvokeWithNoArgument<T>(this MethodInfo mi, T subject)
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

    public static IReadOnlyList<object> InferTypes(this string text)
    {
      var result = new List<object>();

      foreach (var type in TypeDatabase.ParseableTypes)
      {
        foreach (var m in type.GetMethods().Where(
          x => x.Name.Equals("TryParse") 
          && x.GetParameters().Length == 2))
        {
          var instance = Activator.CreateInstance(type);
          bool ok = (bool) m.Invoke(null, new[] {text, instance});
          if (ok)
          {
            result.Add(instance);
          }
        }
      }
      return result;
    }
  }
}