using System;
using System.Collections.Generic;
using System.Linq;

namespace CallSharp
{
  static class TypeDatabase
  {
    private static HashSet<Type> logicalTypes = new HashSet<Type>
    {
      typeof(bool)
    };

    private static HashSet<Type> integralTypes = new HashSet<Type>
    {
      typeof(long),
      typeof(ulong),
      typeof(int),
      typeof(uint),
      typeof(short),
      typeof(ushort),
      typeof(byte),
      typeof(sbyte),
    };

    private static HashSet<Type> floatingPointTypes = new HashSet<Type>
    {
      typeof(decimal),
      typeof(double),
      typeof(float),
    };

    private static HashSet<Type> dateTimeTypes = new HashSet<Type>
    {
      typeof(DateTime),
      typeof(TimeSpan),
    };

    // todo: simply search for those types which have Parse()
    public static IEnumerable<Type> ParseableTypes => logicalTypes.Concat(integralTypes)
      .Concat(floatingPointTypes)
      .Concat(dateTimeTypes);

    public static IEnumerable<Type> CoreTypes => ParseableTypes;
  }
}
