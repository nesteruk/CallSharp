using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace CallSharp
{
  public class MemberDatabase
  {
    private readonly List<MethodInfo> methods = new List<MethodInfo>();
    private readonly List<ConstructorInfo> constructors = new List<ConstructorInfo>();
    private readonly FragmentationEngine fragEngine = new FragmentationEngine();

    public MemberDatabase()
    {
      // get each loaded assembly
      foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
      {
        // we only care about publicly visible types, right?
        foreach (var type in ass.ExportedTypes)
        {
          Trace.WriteLine(type.FullName + " from " + ass.FullName);
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
      foreach (var method in methods.AsParallel().Where(m =>
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
      foreach (var method in methods.AsParallel().Where(m =>
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
    /// Locate any non-static method of <c>inputType</c> that takes a single parameter or
    /// a <c>params[]</c>.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="outputType"></param>
    /// <returns></returns>
    public IEnumerable<MethodInfo> FindOneToTwoNonStatic(Type inputType, Type outputType)
    {
      foreach (var method in methods.AsParallel().Where(m =>
        !m.IsStatic
        && m.DeclaringType == inputType
        && m.ReturnType.IsConvertibleTo(outputType)))
      {
        var pars = method.GetParameters();
        if (pars.Length == 1)
          yield return method;
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
      foreach (var method in methods.AsParallel().Where(m =>
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

    [Pure]
    public IEnumerable<string> FindCandidates(object input, object output, int depth, string callChain = "input")
    {
      Trace.WriteLine(callChain);

      bool foundSomething = false;

      // contains all calls that didn't yield the right result
      var failCookies = new List<MethodCallCookie>();

      foreach (
        var ctor in FindConstructorFor(input.GetType(), output.GetType()))
      {
        // construct exactly this type of object
        object instance = ctor.Invoke(new[] { input });
        if (instance.Equals(output))
        {
          yield return $"new {ctor.ReflectedType.GetFriendlyName()}({callChain})";
          foundSomething = true;
        }
      }

      foreach (var m in FindOneToOneNonStatic(input.GetType(), output.GetType()))
      {
        var cookie = m.InvokeWithNoArgument(input);
        if (cookie != null && output.Equals(cookie?.ReturnValue))
        {
          yield return cookie.ToString(callChain);
          foundSomething = true;
        }
        else
        {
          failCookies.Add(cookie);
        }
      }


      foreach (var m in FindOneToOneStatic(input.GetType(), output.GetType()))
      {
        var cookie = m.InvokeStaticWithSingleArgument(input);
        if (output.Equals(cookie?.ReturnValue))
        {
          if (cookie != null && !Equals(cookie.ReturnValue, input))
          {
            yield return cookie.ToString(callChain);
            foundSomething = true;
          }
        }
        else
        {
          failCookies.Add(cookie);
        }
      }

      foreach (var m in FindOneToTwoNonStatic(input.GetType(), output.GetType()))
      {
        // generate a set of values to invoke on
        foreach (var arg in fragEngine.Frag(input, m.GetParameters()[0].ParameterType))
        {
          var cookie = m.InvokeWithSingleArgument(input, arg);
          if (output.Equals(cookie?.ReturnValue))
          {
            if (cookie != null && !Equals(cookie.ReturnValue, input))
            {
              yield return cookie.ToString(callChain);
              foundSomething = true;
            }
          }
          else
          {
            failCookies.Add(cookie);
          }
        }
      }

      // assuming we haven't found things and not in too deep
      if (!foundSomething && depth < 3)
      {
        // if we found nothing of worth, try a chain
        foreach (var m in FindAnyToOneNonStatic(input.GetType(), output.GetType()))
        {
          // get the cookie for this invocation
          var cookie = m.InvokeWithNoArgument(input);

          // pass it on
          if (cookie != null && !Equals(cookie.ReturnValue, input))
          {
            foreach (var c in
              FindCandidates(cookie.ReturnValue, output, depth + 1, cookie.ToString(callChain)))
            {
              yield return c;
              foundSomething = true;
            }
          }
        }

        // could be a static call of some arbitrary type
        foreach (
          var m in FindAnyToOneStatic(input.GetType(), output.GetType()))
        {
          var cookie = m.InvokeStaticWithSingleArgument(input);
          if (cookie != null && !Equals(cookie.ReturnValue, input))
          {
            foreach (var c in FindCandidates(cookie.ReturnValue, output, depth + 1, cookie.ToString(callChain)))
            {
              yield return c;
              foundSomething = true;
            }
          }
        }

        // we already have call results for some invocation chains, why not try those?
        foreach (var fc in failCookies.Where(fc => fc != null && !Equals(fc.ReturnValue, input)))
        {
          foreach (var с in FindCandidates(fc.ReturnValue, output, depth + 1, fc.ToString(callChain)))
          {
            yield return с;
            foundSomething = true;
          }
        }
      }
    }
  }
}