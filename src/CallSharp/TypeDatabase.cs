using System;
using System.Collections.Generic;
using System.Linq;

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

    public static IEnumerable<Type> integralTypes =>
      signedIntegralTypes.Concat(unsignedIntegralTypes);

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

    // todo: simply search for those types which have Parse()
    public static IEnumerable<Type> ParseableTypes => integralTypes
      .Prepend(typeof(bool))
      .Concat(floatingPointTypes)
      .Concat(dateTimeTypes)
      .Append(typeof(char))
      .Append(typeof(string));

    public static IEnumerable<Type> CoreTypes => ParseableTypes;
  }
}
