using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CallSharp
{
  public class MemberDatabase
  {
    public List<MethodInfo> methods = new List<MethodInfo>();
    public List<ConstructorInfo> constructors = new List<ConstructorInfo>();

    public MemberDatabase()
    {
      // get each loaded assembly
      foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
      {
        // we only care about publicly visible types, right?
        foreach (var type in ass.ExportedTypes)
        {
          methods.AddRange(type.GetMethods());

          // we index single-argument constructors only
          constructors.AddRange(type.GetConstructors().Where(c =>
            !c.ContainsGenericParameters // nor this
            && c.GetParameters().Length == 1 // the argument type is unconstrained
          ));
        }
      }
    }

    /// <summary>
    /// Locates any non-static method of <code>inputType</code> that yields a
    /// value that is expressly <em>not</em> of <c>ignoreOutputType</c>.
    /// </summary>
    /// <param name="inputType">The type on which to search for member functions.</param>
    /// <param name="ignoreThisOutputType">Optional return type to avoid including in the results.</param>
    /// <remarks>
    /// The reason why <c>ignoreOutputType</c> exists is that when making the primary search,
    /// we search explicitly for f:A->B and already have cookies from those searches. We want
    /// to avoid performing the search again, so we search for f:A->Z where Z != B.
    /// </remarks>
    public IEnumerable<MethodInfo> FindAnyToOneNonStatic(Type inputType, Type ignoreThisOutputType)
    {
      foreach (var method in methods.Where(m =>
        m.DeclaringType == inputType &&
        m.ReturnType != ignoreThisOutputType &&
        m.ReturnType != typeof(void)
        // warn: return value can be of any type
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

    /// <summary>
    /// This search ought to locate non-void extension methods available in extraneous types, such as
    /// <c>IEnumerable</c> extension methods.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="ignoreThisOutputType"></param>
    /// <returns></returns>
    public IEnumerable<MethodInfo> FindAnyToOneStatic(Type inputType,
      Type ignoreThisOutputType)
    {
      foreach (var method in methods.Where(m =>
        m.DeclaringType != null 
        && (
          m.DeclaringType.IsIn(TypeDatabase.CoreTypes)
          ||
          m.DeclaringType.Name.Contains("IEnumerable") // hack ;(
        )
        && m.ReturnType != ignoreThisOutputType
        && m.IsStatic))
      {
        var pars = method.GetParameters();

        // accept just 1 argument of required type
        if (pars.Length == 1 && pars[0].ParameterType == inputType)
          yield return method;
      }
    }

    public IEnumerable<ConstructorInfo> FindConstructorFor(Type inputType, Type outputType)
    {
      return constructors.Where(c =>
          c.DeclaringType == outputType
          && c.GetParameters()[0].ParameterType == inputType
      );
    }

    /// <summary>
    /// Locate any non-static method of <c>inputType</c> that takes no parameters and
    /// returns a value of <c>outputType</c>.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="outputType"></param>
    /// <returns></returns>
    public IEnumerable<MethodInfo> FindOneToOneNonStatic(Type inputType, Type outputType)
    {
      foreach (var method in methods.Where(m =>
        m.DeclaringType == inputType &&
        m.ReturnType.IsConvertibleTo(outputType)))
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

    /// <summary>
    /// Locates all static methods of any type that is in <see cref="TypeDatabase.CoreTypes"/>
    /// that takes an argument of <c>inputType</c> and returns a value of <c>outputType</c>.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="outputType"></param>
    /// <returns></returns>
    public IEnumerable<MethodInfo> FindOneToOneStatic(Type inputType, Type outputType)
    {
      // search in ALL core types types :)
      // warning: allowing other types is NOT SAFE because you might call File.Delete or something
      foreach (var method in methods.Where(m =>
        m.ReturnType.IsConvertibleTo(outputType)
        && TypeDatabase.CoreTypes.Contains(m.DeclaringType) // a core type
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