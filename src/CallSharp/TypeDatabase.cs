using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CallSharp
{
  static class TypeDatabase
  {
    private static readonly HashSet<Type> signedIntegralTypes = new HashSet<Type>
    {
      typeof(int),
      typeof(long),
      typeof(short),
      typeof(sbyte)
    };

    private static readonly HashSet<Type> unsignedIntegralTypes = new HashSet<Type>
    {
      typeof(uint),
      typeof(ulong),
      typeof(ushort),
      typeof(byte)
    };

    public static HashSet<Type> integralTypes = new HashSet<Type>(signedIntegralTypes.Concat(unsignedIntegralTypes));

    private static readonly HashSet<Type> floatingPointTypes = new HashSet<Type>
    {
      typeof(float),
      typeof(double),
      typeof(decimal)
    };

    private static readonly HashSet<Type> dateTimeTypes = new HashSet<Type>
    {
      typeof(DateTime),
      typeof(TimeSpan)
    };

    // finds all types from `mscorlib` that contains TryParse<T>(string, out T) method
    /*public static Dictionary<Type, MethodInfo> ParseableTypes = Assembly.Load("mscorlib").GetTypes()
                                                                        .ToDictionary(m => m, t => t.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                                                                    .FirstOrDefault(m => m.Name == "TryParse" && m.GetParameters().Length == 2))
                                                                        .Where(pair => pair.Value != null && pair.Key.FullName != "System.Enum")
                                                                        .ToDictionary(pair => pair.Key, pair => pair.Value);*/

    
    //all types from `mscorlib` that contains TryParse<T>(string, out T) method
    public static readonly Dictionary<Type, MethodInfo> ParseableTypesDict = integralTypes
     .Prepend(typeof(bool))
     .Concat(floatingPointTypes)
     .Concat(dateTimeTypes).Append(typeof(char))
     /*.Append(typeof(string))*/.ToDictionary(t => t,
       t => t.GetMethods(BindingFlags.Static | BindingFlags.Public)
             .FirstOrDefault(m => m.Name == "TryParse" && 
                                  m.GetParameters().Length == 2));

    public static HashSet<Type> SequenceTypes = new HashSet<Type>
    {
      typeof(Array),
      typeof(List<>)
    };

    public static readonly HashSet<Type> CoreTypes = new HashSet<Type>( 
      ParseableTypesDict.Keys
      .Append(typeof(Enumerable))
      .Append(typeof(Math)));
  }
}
