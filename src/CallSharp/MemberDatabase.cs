using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CallSharp
{
  public class MemberDatabase
  {
    public List<MethodInfo> methods = new List<MethodInfo>();
    public List<PropertyInfo> properties = new List<PropertyInfo>();

    public MemberDatabase()
    {
      // get each loaded assembly
      foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
      {
        // we only care about publicly visible types, right?
        foreach (var type in ass.ExportedTypes)
        {
          // since we index properties, ignore getter methods here
          methods.AddRange(type.GetMethods().Where(
            m => !m.Name.StartsWith("get_")));
          properties.AddRange(type.GetProperties());
        }
      }
    }

    public IEnumerable<PropertyInfo> FindOneToOnePropertyGet(Type inputType,
      Type outputType)
    {
      var x = properties.Where(p =>
            p.DeclaringType == typeof(string))
        .ToArray();

      return properties.Where(p => // get properties where
        p.DeclaringType == inputType // a property returns the right type
        && p.GetMethod != null // it has a getter
        && p.GetMethod.ReturnType == outputType // that returns the right type (duh!)
        && !p.GetMethod.GetParameters().Any()
      ); // and the getter takes no arguments
    }

    public IEnumerable<MethodInfo> FindOneToOneNonStatic(Type inputType, Type outputType)
    {
      foreach (var method in methods.Where(m => 
        m.DeclaringType == inputType &&
        m.ReturnType == outputType))
      {
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

    public IEnumerable<MethodInfo> FindOneToOneStatic(Type inputType, Type outputType)
    {
      // search in ALL core types types :)
      // warning: allowing other types is NOT SAFE because you might call File.Delete or something
      foreach (var method in methods.Where(m =>
        m.ReturnType == outputType 
        && TypeDatabase.CoreTypes.Contains(m.DeclaringType)
        && !m.Name.Equals("Parse") // it throws :(
        ))
      {
        if (method.Name.Equals("IsNullOrWhitespace"))
          Debugger.Break();

        var pars = method.GetParameters();
        
        if (method.IsStatic &&
            pars.Length == 1 &&
            pars[0].ParameterType == inputType)
        {
          yield return method;
        }
      }
    }
  }
}