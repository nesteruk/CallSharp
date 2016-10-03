using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace CallSharp
{
  public static class ExtensionMethods
  {
    public static string RemoveMarkers(this string s)
    {
      return s.Replace(MagicTextBox.SpaceChar, ' ');
    }

    public static bool AllAreOptional(this ParameterInfo[] ps)
    {
      return ps.All(p => p.IsOptional);
    }

    public static bool IsParams(this ParameterInfo pi)
    {
      return pi.GetCustomAttribute<ParamArrayAttribute>() != null;
    }

    [CanBeNull]
    public static MethodCallCookie InvokeStaticWithSingleArgument<T>(this MethodInfo mi, T arg)
    {
      MethodCallCookie result = null;
      try
      {
        var args = new object[] {arg};
        var retval = mi.Invoke(null /*static*/, args);
        result = new MethodCallCookie(mi, args, retval);
      }
      catch
      {
        // we cannot reasonably catch this
      }
      return result;
    }

    [CanBeNull]
    public static MethodCallCookie InvokeWithNoArgument<T>(this MethodInfo mi, T subject)
    {
      var pars = mi.GetParameters();
      MethodCallCookie result = null;
      try
      {
        if (pars.IsSingleParamsArgument())
        {
          var args = new[]
          {
            Activator.CreateInstance(pars[0].ParameterType.UnderlyingSystemType, 0)
          };
          var retval = mi.Invoke(subject, args);
          result = new MethodCallCookie(mi, args, retval);
        }
        else
        {
          var retval = mi.Invoke(subject, Empty.ObjectArray);
          result = new MethodCallCookie(mi, Empty.ObjectArray, retval);
        }
      } catch { }
      return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIn<T>(this T obj, IEnumerable<T> collection)
    {
      return collection.Contains(obj);
    }

    public static bool IsSingleParamsArgument(this ParameterInfo[] ps)
    {
      return ps.Length == 1 && ps[0].IsParams();
    }

    public static IList<object> InferTypes(this string text)
    {
      var result = new List<object>();

      foreach (var type in TypeDatabase.ParseableTypes)
      {
        foreach (var m in type.GetMethods().Where(
          x => x.Name.Equals("TryParse") 
          && x.GetParameters().Length == 2))
        {
          // see http://stackoverflow.com/questions/569249/methodinfo-invoke-with-out-parameter
          object[] pars = {text, null};
          bool ok = (bool) m.Invoke(null, pars);
          if (ok)
          {
            result.Add(pars[1]);
          }
        }
      }
      return result;
    }

    /// <summary>Adds a single element to the end of an IEnumerable.</summary>
    /// <typeparam name="T">Type of enumerable to return.</typeparam>
    /// <returns>IEnumerable containing all the input elements, followed by the
    /// specified additional element.</returns>
    public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      return concatIterator(element, source, false);
    }

    /// <summary>Adds a single element to the start of an IEnumerable.</summary>
    /// <typeparam name="T">Type of enumerable to return.</typeparam>
    /// <returns>IEnumerable containing the specified additional element, followed by
    /// all the input elements.</returns>
    public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head)
    {
      if (tail == null)
        throw new ArgumentNullException("tail");
      return concatIterator(head, tail, true);
    }

    private static IEnumerable<T> concatIterator<T>(T extraElement,
        IEnumerable<T> source, bool insertAtStart)
    {
      if (insertAtStart)
        yield return extraElement;
      foreach (var e in source)
        yield return e;
      if (!insertAtStart)
        yield return extraElement;
    }

    public static string GetFriendlyName(this Type type)
    {
      var codeDomProvider = CodeDomProvider.CreateProvider("C#");
      var typeReferenceExpression = new CodeTypeReferenceExpression(new CodeTypeReference(type));
      using (var writer = new StringWriter())
      {
        codeDomProvider.GenerateCodeFromExpression(typeReferenceExpression, writer, new CodeGeneratorOptions());
        return writer.GetStringBuilder().Replace("System.", string.Empty).ToString();
      }
    }

    static Dictionary<Type, List<Type>> conversionMap = new Dictionary<Type, List<Type>>
    {
        { typeof(decimal), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(char) } },
        { typeof(double), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(char), typeof(float) } },
        { typeof(float), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(char), typeof(float) } },
        { typeof(ulong), new List<Type> { typeof(byte), typeof(ushort), typeof(uint), typeof(char) } },
        { typeof(long), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(char) } },
        { typeof(uint), new List<Type> { typeof(byte), typeof(ushort), typeof(char) } },
        { typeof(int), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(char) } },
        { typeof(ushort), new List<Type> { typeof(byte), typeof(char) } },
        { typeof(short), new List<Type> { typeof(byte) } }
    };

    public static bool IsConvertibleTo(this Type from, Type to)
    {
      if (from == to || to.IsAssignableFrom(from))
      {
        return true;
      }
      if (conversionMap.ContainsKey(to) && conversionMap[to].Contains(from))
      {
        return true;
      }
      bool castable = from.GetMethods(BindingFlags.Public | BindingFlags.Static)
                      .Any(
                          m => m.ReturnType == to &&
                          (m.Name == "op_Implicit" ||
                          m.Name == "op_Explicit")
                      );
      return castable;
    }
  }
}