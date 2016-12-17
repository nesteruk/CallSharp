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

    // todo: simply search for those types which have TryParse()
    public static HashSet<Type> ParseableTypes = new HashSet<Type>(integralTypes
      .Prepend(typeof(bool))
      .Concat(floatingPointTypes)
      .Concat(dateTimeTypes)
      .Append(typeof(char))
      .Append(typeof(string)));

    public static HashSet<Type> SequenceTypes = new HashSet<Type>
    {
      typeof(Array),
      typeof(List<>)
    };

    public static HashSet<Type> CoreTypes = new HashSet<Type>( 
      ParseableTypes
      .Append(typeof(Enumerable))
      .Append(typeof(Math))
	    .Append(typeof(ExtensionMethods))
    );
  }
}
