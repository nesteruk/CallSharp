using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CallSharp
{
  public class MemberDatabase
  {
    public List<MethodInfo> methods = new List<MethodInfo>();

    public MemberDatabase()
    {
      // get each loaded assembly
      foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
      {
        // we only care about publicly visible types, right?
        foreach (var type in ass.ExportedTypes)
        {
          // since we index properties, ignore getter methods here
          methods.AddRange(type.GetMethods());
        }
      }
    }

    /// <summary>
    /// Locates any non-static method of <code>inputType</code> that yields a
    /// value that is expressly <em>not</em> of <c>ignoreOutputType</c>.
    /// </summary>
    /// <param name="inputType">The type on which to search for member functions.</param>
    /// <param name="ignoreThisOutputType">Optional return type to avoid including in the results.</param>
    /// <returns>
    /// The reason why <c>ignoreOutputType</c> exists is that when making the primary search,
    /// we search explicitly for f:A->B and already have cookies from those searches. We want
    /// to avoid performing the search again, so we search for f:A->Z where Z != B.
    /// </returns>
    public IEnumerable<MethodInfo> FindAnyToOneNonStatic(Type inputType, Type ignoreThisOutputType)
    {
      foreach (var method in methods.Where(m =>
        m.DeclaringType == inputType &&
        m.ReturnType != ignoreThisOutputType &&
        TypeDatabase.CoreTypes.Contains(m.ReturnType) // safety!
        ))
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
        && !m.Name.Equals("Parse") // it throws 
        ))
      {
        if (method.Name.Contains("Delete"))
          throw new Exception("Just in case!");

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