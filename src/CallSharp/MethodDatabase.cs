using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CallSharp
{
  public class MethodDatabase
  {
    public List<MethodInfo> methods = new List<MethodInfo>();

    public MethodDatabase()
    {
      // get each loaded assembly
      foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
      {
        // we only care about publicly visible types, right?
        foreach (var type in ass.ExportedTypes)
        {
          methods.AddRange(type.GetMethods());
        }
      }
    }

    public IEnumerable<MethodInfo> FindOneToOneNonStatic(Type inputType, Type outputType)
    {
      foreach (var method in methods.Where(m => 
        m.DeclaringType == inputType &&
        m.ReturnType == outputType))
      {
        if (method.Name.Equals("ToString"))
          Debugger.Break();
        var pars = method.GetParameters();

        if (!method.IsStatic &&
            (pars.Length == 0
             || pars.AllAreOptional()
             || pars.IsSingleParamsArgument()))
        {
          yield return method;
        }
      }
    }
  }
}