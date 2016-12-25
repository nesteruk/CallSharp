using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace CallSharp
{
  [Obsolete("This is far too slow, use StaticMemberDatabase", true)]
  public class DynamicMemberDatabase : IMemberDatabase
  {
    private readonly HashSet<MethodInfo> staticMethods = new HashSet<MethodInfo>();
    private readonly HashSet<MethodInfo> instanceMethods = new HashSet<MethodInfo>();
    private readonly HashSet<ConstructorInfo> constructors = new HashSet<ConstructorInfo>();
    private readonly IFragmentationEngine fragEngine = new FragmentationEngine();

    public DynamicMemberDatabase()
    {
      // get each loaded assembly
      foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
      {
        // we only care about publicly visible types, right?
        foreach (var type in ass.ExportedTypes)
        {
          Trace.WriteLine(type.FullName + " from " + ass.FullName);
          var methods = type.GetMethods();
          foreach (var m in methods)
            m.AddTo(m.IsStatic ? staticMethods : instanceMethods);

          // we index single-argument constructors only
          type.GetConstructors().Where(c =>
            !c.ContainsGenericParameters // nor this
            && c.GetParameters().Length == 1 // the argument type is unconstrained
          ).AddTo(constructors);
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
    public IEnumerable<MethodInfo> FindAnyToOneInstance(Type inputType, Type ignoreThisOutputType)
    {
      foreach (var method in instanceMethods.AsParallel().Where(m =>
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
      foreach (var method in staticMethods.Where(m =>
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
    public IEnumerable<MethodInfo> FindOneToOneInstance(Type inputType, Type outputType)
    {
      foreach (var method in instanceMethods.AsParallel().Where(m =>
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

    private IEnumerable<MethodInfo> FindOneToXInstance(Type inputType, Type outputType,
      int numberOfNonOptionalArguments)
    {
      foreach (var method in instanceMethods.AsParallel().Where(m =>
        !m.IsStatic
        && m.DeclaringType == inputType
        && m.ReturnType.IsConvertibleTo(outputType)))
      {
        var pars = method.GetParameters();
        if (pars.Count(p => !p.ProvisionOfThisArgumentIsOptional()) ==
            numberOfNonOptionalArguments)
        {
          yield return method;
        }
      }
    }

    public IEnumerable<MethodInfo> FindOneToThreeInstance(Type inputType, Type outputType)
    {
      return FindOneToXInstance(inputType, outputType, 2);
    }

    /// <summary>
    /// Locate any non-static method of <c>inputType</c> that takes a single parameter or
    /// a <c>params[]</c>.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="outputType"></param>
    /// <returns></returns>
    public IEnumerable<MethodInfo> FindOneToTwoInstance(Type inputType, Type outputType)
    {
      return FindOneToXInstance(inputType, outputType, 1);
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
      // search in ALL core types :)
      // warning: allowing other types is NOT SAFE because you might call File.Delete or something
      foreach (var method in staticMethods.AsParallel().Where(m =>
        m.ReturnType.IsConvertibleTo(outputType)
        && TypeDatabase.CoreTypes.Contains(m.DeclaringType) // a core type
        && !m.Name.Equals("Parse") // it throws 
        ))
      {
        if (method.Name.Contains("Delete"))
          throw new Exception("Just in case!");

        if (method.ToString().Contains("System.Math") && method.Name.Contains("Sqrt"))
        {
          Debugger.Break();
        }

        var pars = method.GetParameters();

        if (method.IsStatic &&
            pars.Length == 1 &&
            inputType.IsConvertibleTo(pars[0].ParameterType)
            //pars[0].ParameterType == inputType
           )
        {
          yield return method;
        }
      }
    }

    

    [Pure]
    public IEnumerable<string> FindCandidates(object origin, object input, object output, int depth, string callChain = "input")
    {
      Trace.WriteLine(callChain);

      // if inputs are completely identical, we have no further work to do
      if (input.Equals(output))
      {
        yield return callChain;
        yield break;
      }

      // here we try to brute-force conversion of input to output
      // if it succeeds, we break as before
      object newValue = null;
      try
      {
        newValue = Convert.ChangeType(output, input.GetType());
      } catch (Exception) { }
      if (input.Equals(newValue))
      {
        yield return callChain;
        yield break;
      }

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

      foreach (var m in FindOneToOneInstance(input.GetType(), output.GetType()))
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

      // look for single-argument fragmentation
      if (!foundSomething)
      {
        foreach (var m in FindOneToTwoInstance(input.GetType(), output.GetType()))
        {
          // generate a set of values to invoke on
          foreach (var arg in fragEngine.Frag(input, m.GetParameters()[0].ParameterType))
          {
            var cookie = m.InvokeWithArguments(input, arg);
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
      }

      // look for two-argument fragmentation; this is costly
      if (!foundSomething)
      {
        foreach (var m in FindOneToThreeInstance(input.GetType(), output.GetType()))
        {
          // generate a set of first and second arguments
          foreach (var arg1 in fragEngine.Frag(input, m.GetParameters()[0].ParameterType))
          foreach (var arg2 in fragEngine.Frag(input, m.GetParameters()[1].ParameterType))
          {
            var cookie = m.InvokeWithArguments(input, arg1, arg2);
            if (output.Equals(cookie?.ReturnValue))
            {
              if (cookie != null && !Equals(cookie.ReturnValue, input))
              {
                yield return cookie.ToString(callChain);
                //foundSomething = true;
              }
            }
            else
            {
              failCookies.Add(cookie);
            }
          }
        }
      }

      // assuming we haven't found things and not in too deep
      if (!foundSomething && depth < 2)
      {
        // if we found nothing of worth, try a chain
        foreach (var m in FindAnyToOneInstance(input.GetType(), output.GetType()))
        {
          // get the cookie for this invocation
          var cookie = m.InvokeWithNoArgument(input);

          // pass it on
          if (cookie != null && !Equals(cookie.ReturnValue, input))
          {
            foreach (var c in
              FindCandidates(origin, cookie.ReturnValue, output, depth + 1, cookie.ToString(callChain)))
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
            foreach (var c in FindCandidates(origin, cookie.ReturnValue, output, depth + 1, cookie.ToString(callChain)))
            {
              yield return c;
              foundSomething = true;
            }
          }
        }

        // we already have call results for some invocation chains, why not try those?
        foreach (var fc in failCookies.Where(fc => fc != null && !Equals(fc.ReturnValue, input)))
        {
          foreach (var с in FindCandidates(origin, fc.ReturnValue, output, depth + 1, fc.ToString(callChain)))
          {
            yield return с;
            foundSomething = true;
          }
        }
      }
    }
  }
}