using System.Text;

namespace CallSharp
{
  using System;
  using System.CodeDom;
  using System.CodeDom.Compiler;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Reflection;
  using System.Runtime.CompilerServices;
  using JetBrains.Annotations;

  public static class ExtensionMethods
  {
    public static string ToLiteral(this object o)
    {
      if (o is Array)
      {
        var a = o as Array;
        var sb = new StringBuilder();
        sb.Append("new [] { ");
        for (int i = 0; i < a.Length; i++)
        {
          sb.Append(a.GetValue(i).ToLiteral());
          if (i + 1 != a.Length)
            sb.Append(", ");
        }
        sb.Append(" }");
        return sb.ToString();
      }
      if (o is string)
      {
        if ((o as string).Length == 0)
          return "string.Empty";
        return $"\"{o}\"";
      }
      if (o is char)
      {
        return $"'{o}'";
      }
      return o.ToString();
    }

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

    public static bool ProvisionOfThisArgumentIsOptional(this ParameterInfo pi)
    {
      return pi.IsParams() || pi.HasDefaultValue;
    }

    [CanBeNull]
    public static MethodCallCookie InvokeStaticWithSingleArgument<T>(this MethodInfo mi,
      T arg)
    {
      MethodCallCookie result = null;
      try
      {
        var args = new object[] { arg };
        var retval = mi.Invoke(null /*static*/, args);
        result = new MethodCallCookie(mi, args, retval);
      }
      catch
      {
        // we cannot reasonably catch this
      }
      return result;
    }

    public static IEnumerable<T[]> Combinations<T>(this T[] values, int k)
    {
      if (k < 0 || values.Length < k)
        yield break; // invalid parameters, no combinations possible

      // generate the initial combination indices
      var combIndices = new int[k];
      for (var i = 0; i < k; i++)
      {
        combIndices[i] = i;
      }

      while (true)
      {
        // return next combination
        var combination = new T[k];
        for (var i = 0; i < k; i++)
        {
          combination[i] = values[combIndices[i]];
        }
        yield return combination;

        // find first index to update
        var indexToUpdate = k - 1;
        while (indexToUpdate >= 0 && combIndices[indexToUpdate] >= values.Length - k + indexToUpdate)
        {
          indexToUpdate--;
        }

        if (indexToUpdate < 0)
          yield break; // done

        // update combination indices
        for (var combIndex = combIndices[indexToUpdate] + 1; indexToUpdate < k; indexToUpdate++, combIndex++)
        {
          combIndices[indexToUpdate] = combIndex;
        }
      }
    }

    [CanBeNull]
    public static MethodCallCookie InvokeWithArguments(this MethodInfo mi, object self,
      params object[] args)
    {
      MethodCallCookie result = null;
      try
      {
        var retval = mi.Invoke(self, args);
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
      }
      catch
      {
      }
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
          object[] pars = { text, null };
          bool ok = (bool)m.Invoke(null, pars);
          if (ok)
          {
            result.Add(pars[1]);
          }
        }
      }

      // try to decompose and see if it makes sense
      var commaSeparated = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
      if (commaSeparated.Length > 1)
      {
        // does every separated part parse under this model
        foreach (var type in TypeDatabase.ParseableTypes)
        {
          foreach (var m in type.GetMethods().Where(
            x => x.Name.Equals("TryParse")
                 && x.GetParameters().Length == 2))
          {
            // see http://stackoverflow.com/questions/569249/methodinfo-invoke-with-out-parameter

            // parse on every argument
            var ok = commaSeparated.Select(part => (bool) m.Invoke(null,
              new object[] {part, null}));

            if (ok.All(x=>x))
            {
              //result.Add(pars[1]);
              result.Add(typeof(List<>).MakeGenericType(type));
            }
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
        throw new ArgumentNullException(nameof(source));
      return concatIterator(element, source, false);
    }

    /// <summary>Adds a single element to the start of an IEnumerable.</summary>
    /// <typeparam name="T">Type of enumerable to return.</typeparam>
    /// <returns>IEnumerable containing the specified additional element, followed by
    /// all the input elements.</returns>
    public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head)
    {
      if (tail == null)
        throw new ArgumentNullException(nameof(tail));
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
      var typeReferenceExpression =
        new CodeTypeReferenceExpression(new CodeTypeReference(
          type.Name.Contains("RuntimeType") ? type.UnderlyingSystemType : type
        ));
      using (var writer = new StringWriter())
      {
        codeDomProvider.GenerateCodeFromExpression(typeReferenceExpression, writer,
          new CodeGeneratorOptions());
        return writer.GetStringBuilder().Replace("System.", string.Empty).ToString();
      }
    }

    static readonly Dictionary<Type, HashSet<Type>> conversionMap = new Dictionary
      <Type, HashSet<Type>>
      {
        {
          typeof(decimal), new HashSet<Type>
          {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(char)
          }
        },
        {
          typeof(double), new HashSet<Type>
          {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(char),
            typeof(float)
          }
        },
        {
          typeof(float), new HashSet<Type>
          {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(char),
            typeof(float)
          }
        },
        {
          typeof(ulong),
          new HashSet<Type> {typeof(byte), typeof(ushort), typeof(uint), typeof(char)}
        },
        {
          typeof(long), new HashSet<Type>
          {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(char)
          }
        },
        {typeof(uint), new HashSet<Type> {typeof(byte), typeof(ushort), typeof(char)}},
        {
          typeof(int),
          new HashSet<Type>
          {
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(char)
          }
        },
        {typeof(ushort), new HashSet<Type> {typeof(byte), typeof(char)}},
        {typeof(short), new HashSet<Type> {typeof(byte)}}
      };

    public static void AddTo<T>(this T item, ICollection<T> coll)
    {
      coll.Add(item);
    }

    public static void AddTo<T>(this IEnumerable<T> items, ICollection<T> coll)
    {
      foreach (var item in items)
      {
        coll.Add(item);
      }
    }

    public static bool IsConvertibleTo(this Type from, Type to)
    {
      return from == to || to.IsAssignableFrom(from) ||
             (conversionMap.ContainsKey(to) && conversionMap[to].Contains(from));

      //if (from == to || to.IsAssignableFrom(from))
      //{
      //  return true;
      //}
      //if (conversionMap.ContainsKey(to) && conversionMap[to].Contains(from))
      //{
      //  return true;
      //}
      //bool castable = from.GetMethods(BindingFlags.Public | BindingFlags.Static)
      //                .Any(
      //                    m => m.ReturnType == to &&
      //                    (m.Name == "op_Implicit" ||
      //                    m.Name == "op_Explicit")
      //                );
      //return castable;
    }
  }
}