

 // cannot get exported types of Anonymously Hosted DynamicMethods Assembly 
namespace CallSharp
{
  using System;
  using System.Diagnostics;
  using System.Collections.Generic;

  class StaticMemberDatabase : IMemberDatabase
  {
    private readonly IFragmentationEngine fragEngine = new FragmentationEngine();

    public IEnumerable<string> FindCandidates(object input, object output, int depth,
      string callChain = "input")
    {
      Trace.WriteLine(callChain);

      var inputType = input.GetType();
      var outputType = output.GetType();

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
        newValue = Convert.ChangeType(output, inputType);
      } catch (Exception) { }
      if (input.Equals(newValue))
      {
        yield return callChain;
        yield break;
      }

	    bool foundSomething = false;

	    // contains all calls that didn't yield the right result
      var failCookies = new List<MethodCallCookie>();

      // note: technically, we collect fail cookies. or used to.

	    // 1) Look for all matching constructors.
      if (output is System.String && input is System.Char[])
    {
      var instance = new System.String((System.Char[])input);
      if (instance == (System.String)output)
      {
        yield return $"new string({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.DateTime && input is System.Int64)
    {
      var instance = new System.DateTime((System.Int64)input);
      if (instance == (System.DateTime)output)
      {
        yield return $"new DateTime({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.Decimal && input is System.Int32)
    {
      var instance = new System.Decimal((System.Int32)input);
      if (instance == (System.Decimal)output)
      {
        yield return $"new decimal({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.Decimal && input is System.UInt32)
    {
      var instance = new System.Decimal((System.UInt32)input);
      if (instance == (System.Decimal)output)
      {
        yield return $"new decimal({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.Decimal && input is System.Int64)
    {
      var instance = new System.Decimal((System.Int64)input);
      if (instance == (System.Decimal)output)
      {
        yield return $"new decimal({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.Decimal && input is System.UInt64)
    {
      var instance = new System.Decimal((System.UInt64)input);
      if (instance == (System.Decimal)output)
      {
        yield return $"new decimal({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.Decimal && input is System.Single)
    {
      var instance = new System.Decimal((System.Single)input);
      if (instance == (System.Decimal)output)
      {
        yield return $"new decimal({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.Decimal && input is System.Double)
    {
      var instance = new System.Decimal((System.Double)input);
      if (instance == (System.Decimal)output)
      {
        yield return $"new decimal({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.Decimal && input is System.Int32[])
    {
      var instance = new System.Decimal((System.Int32[])input);
      if (instance == (System.Decimal)output)
      {
        yield return $"new decimal({callChain})";
        foundSomething = true;
      }
    }
    if (output is System.TimeSpan && input is System.Int64)
    {
      var instance = new System.TimeSpan((System.Int64)input);
      if (instance == (System.TimeSpan)output)
      {
        yield return $"new TimeSpan({callChain})";
        foundSomething = true;
      }
    }
        // 2) 1-to-1 instance functions.
        if (input is String && typeof(System.Char[]).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).ToCharArray();
            if (result == (System.Char[])output)
      {
                yield return string.Format("{0}.ToCharArray()", callChain);
      }
    }
        if (input is String && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is String && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).Length;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Length", callChain);
      }
    }
        if (input is String && typeof(System.String[]).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).Split();
            if (result == (System.String[])output)
      {
                yield return string.Format("{0}.Split()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).Trim();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.Trim()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).TrimStart();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.TrimStart()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).TrimEnd();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.TrimEnd()", callChain);
      }
    }
        if (input is String && typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).IsNormalized();
            if (result == (System.Boolean)output)
      {
                yield return string.Format("{0}.IsNormalized()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).Normalize();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.Normalize()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).ToLower();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToLower()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).ToLowerInvariant();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToLowerInvariant()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).ToUpper();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToUpper()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).ToUpperInvariant();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToUpperInvariant()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is String && typeof(System.Object).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).Clone();
            if (result == (System.Object)output)
      {
                yield return string.Format("{0}.Clone()", callChain);
      }
    }
        if (input is String && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).Trim();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.Trim()", callChain);
      }
    }
        if (input is String && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is String && typeof(System.CharEnumerator).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.String)input).GetEnumerator();
            if (result == (System.CharEnumerator)output)
      {
                yield return string.Format("{0}.GetEnumerator()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Day;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Day", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Hour;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Hour", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Month;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Month", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Minute;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Minute", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Second;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Second", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Year;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Year", callChain);
      }
    }
        if (input is DateTime && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is DateTime && typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).IsDaylightSavingTime();
            if (result == (System.Boolean)output)
      {
                yield return string.Format("{0}.IsDaylightSavingTime()", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int64).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToBinary();
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.ToBinary()", callChain);
      }
    }
        if (input is DateTime && typeof(System.DateTime).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Date;
            if (result == (System.DateTime)output)
      {
                yield return string.Format("{0}.Date", callChain);
      }
    }
        if (input is DateTime && typeof(System.DayOfWeek).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).DayOfWeek;
            if (result == (System.DayOfWeek)output)
      {
                yield return string.Format("{0}.DayOfWeek", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).DayOfYear;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.DayOfYear", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is DateTime && typeof(System.DateTimeKind).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Kind;
            if (result == (System.DateTimeKind)output)
      {
                yield return string.Format("{0}.Kind", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Millisecond;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Millisecond", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int64).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).Ticks;
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.Ticks", callChain);
      }
    }
        if (input is DateTime && typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).TimeOfDay;
            if (result == (System.TimeSpan)output)
      {
                yield return string.Format("{0}.TimeOfDay", callChain);
      }
    }
        if (input is DateTime && typeof(System.Double).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToOADate();
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.ToOADate()", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int64).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToFileTime();
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.ToFileTime()", callChain);
      }
    }
        if (input is DateTime && typeof(System.Int64).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToFileTimeUtc();
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.ToFileTimeUtc()", callChain);
      }
    }
        if (input is DateTime && typeof(System.DateTime).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToLocalTime();
            if (result == (System.DateTime)output)
      {
                yield return string.Format("{0}.ToLocalTime()", callChain);
      }
    }
        if (input is DateTime && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToLongDateString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToLongDateString()", callChain);
      }
    }
        if (input is DateTime && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToLongTimeString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToLongTimeString()", callChain);
      }
    }
        if (input is DateTime && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToShortDateString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToShortDateString()", callChain);
      }
    }
        if (input is DateTime && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToShortTimeString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToShortTimeString()", callChain);
      }
    }
        if (input is DateTime && typeof(System.DateTime).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).ToUniversalTime();
            if (result == (System.DateTime)output)
      {
                yield return string.Format("{0}.ToUniversalTime()", callChain);
      }
    }
        if (input is DateTime && typeof(System.String[]).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).GetDateTimeFormats();
            if (result == (System.String[])output)
      {
                yield return string.Format("{0}.GetDateTimeFormats()", callChain);
      }
    }
        if (input is DateTime && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.DateTime)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Boolean && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Boolean)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Boolean && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Boolean)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Boolean && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Boolean)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Byte && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Byte)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Byte && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Byte)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Byte && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Byte)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Char && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Char)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Char && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Char)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Char && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Char)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Decimal && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Decimal)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Decimal && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Decimal)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Decimal && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Decimal)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Double && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Double)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Double && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Double)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Double && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Double)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Int16 && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int16)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Int16 && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int16)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Int16 && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int16)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Int32 && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int32)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Int32 && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int32)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Int32 && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int32)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Int64 && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int64)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Int64 && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int64)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Int64 && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Int64)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Object && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Object && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is SByte && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.SByte)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is SByte && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.SByte)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is SByte && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.SByte)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Single && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Single)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Single && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Single)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Single && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Single)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Int64).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Ticks;
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.Ticks", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Days;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Days", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Hours;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Hours", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Milliseconds;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Milliseconds", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Minutes;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Minutes", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Seconds;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Seconds", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Double).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalDays;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalDays", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Double).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalHours;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalHours", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Double).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalMilliseconds;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalMilliseconds", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Double).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalMinutes;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalMinutes", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Double).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalSeconds;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalSeconds", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Duration();
            if (result == (System.TimeSpan)output)
      {
                yield return string.Format("{0}.Duration()", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Negate();
            if (result == (System.TimeSpan)output)
      {
                yield return string.Format("{0}.Negate()", callChain);
      }
    }
        if (input is TimeSpan && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.TimeSpan)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is UInt16 && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt16)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is UInt16 && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt16)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is UInt16 && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt16)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is UInt32 && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt32)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is UInt32 && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt32)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is UInt32 && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt32)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is UInt64 && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt64)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is UInt64 && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt64)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is UInt64 && typeof(System.TypeCode).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.UInt64)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Object && typeof(System.String).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Object && typeof(System.Int32).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Object && typeof(System.Type).IsConvertibleTo(outputType))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
      // 3. Search static members
      if (inputType.IsConvertibleTo(typeof(System.String)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.String.IsNullOrEmpty((System.String)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("string.IsNullOrEmpty({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.String)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.String.IsNullOrWhiteSpace((System.String)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("string.IsNullOrWhiteSpace({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.String)) &&
        typeof(System.String).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.String.Copy((System.String)input);
        if (result == (System.String)output)
                  retVal = string.Format("string.Copy({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Object)) &&
        typeof(System.String).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.String.Concat((System.Object)input);
        if (result == (System.String)output)
                  retVal = string.Format("string.Concat({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Object[])) &&
        typeof(System.String).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.String.Concat((System.Object[])input);
        if (result == (System.String)output)
                  retVal = string.Format("string.Concat({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.String[])) &&
        typeof(System.String).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.String.Concat((System.String[])input);
        if (result == (System.String)output)
                  retVal = string.Format("string.Concat({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.String)) &&
        typeof(System.String).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.String.Intern((System.String)input);
        if (result == (System.String)output)
                  retVal = string.Format("string.Intern({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.String)) &&
        typeof(System.String).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.String.IsInterned((System.String)input);
        if (result == (System.String)output)
                  retVal = string.Format("string.IsInterned({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int64)) &&
        typeof(System.DateTime).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.DateTime.FromBinary((System.Int64)input);
        if (result == (System.DateTime)output)
                  retVal = string.Format("DateTime.FromBinary({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int64)) &&
        typeof(System.DateTime).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.DateTime.FromFileTime((System.Int64)input);
        if (result == (System.DateTime)output)
                  retVal = string.Format("DateTime.FromFileTime({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int64)) &&
        typeof(System.DateTime).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.DateTime.FromFileTimeUtc((System.Int64)input);
        if (result == (System.DateTime)output)
                  retVal = string.Format("DateTime.FromFileTimeUtc({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.DateTime).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.DateTime.FromOADate((System.Double)input);
        if (result == (System.DateTime)output)
                  retVal = string.Format("DateTime.FromOADate({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int32)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.DateTime.IsLeapYear((System.Int32)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("DateTime.IsLeapYear({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Char).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.ToLower((System.Char)input);
        if (result == (System.Char)output)
                  retVal = string.Format("char.ToLower({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.String).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.ToString((System.Char)input);
        if (result == (System.String)output)
                  retVal = string.Format("char.ToString({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsDigit((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsDigit({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsLetter((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsLetter({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsWhiteSpace((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsWhiteSpace({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsUpper((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsUpper({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsLower((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsLower({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsPunctuation((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsPunctuation({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsLetterOrDigit((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsLetterOrDigit({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Char).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.ToUpper((System.Char)input);
        if (result == (System.Char)output)
                  retVal = string.Format("char.ToUpper({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Char).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.ToUpperInvariant((System.Char)input);
        if (result == (System.Char)output)
                  retVal = string.Format("char.ToUpperInvariant({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Char).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.ToLowerInvariant((System.Char)input);
        if (result == (System.Char)output)
                  retVal = string.Format("char.ToLowerInvariant({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsControl((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsControl({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsNumber((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsNumber({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsSeparator((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsSeparator({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsSurrogate((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsSurrogate({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsSymbol((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsSymbol({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Globalization.UnicodeCategory).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.GetUnicodeCategory((System.Char)input);
        if (result == (System.Globalization.UnicodeCategory)output)
                  retVal = string.Format("char.GetUnicodeCategory({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.GetNumericValue((System.Char)input);
        if (result == (System.Double)output)
                  retVal = string.Format("char.GetNumericValue({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsHighSurrogate((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsHighSurrogate({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Char)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.IsLowSurrogate((System.Char)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("char.IsLowSurrogate({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int32)) &&
        typeof(System.String).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Char.ConvertFromUtf32((System.Int32)input);
        if (result == (System.String)output)
                  retVal = string.Format("char.ConvertFromUtf32({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Int64).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToOACurrency((System.Decimal)input);
        if (result == (System.Int64)output)
                  retVal = string.Format("decimal.ToOACurrency({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int64)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.FromOACurrency((System.Int64)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("decimal.FromOACurrency({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.Ceiling((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("decimal.Ceiling({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.Floor((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("decimal.Floor({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Int32[]).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.GetBits((System.Decimal)input);
        if (result == (System.Int32[])output)
                  retVal = string.Format("decimal.GetBits({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.Negate((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("decimal.Negate({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.Round((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("decimal.Round({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Byte).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToByte((System.Decimal)input);
        if (result == (System.Byte)output)
                  retVal = string.Format("decimal.ToByte({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.SByte).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToSByte((System.Decimal)input);
        if (result == (System.SByte)output)
                  retVal = string.Format("decimal.ToSByte({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Int16).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToInt16((System.Decimal)input);
        if (result == (System.Int16)output)
                  retVal = string.Format("decimal.ToInt16({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToDouble((System.Decimal)input);
        if (result == (System.Double)output)
                  retVal = string.Format("decimal.ToDouble({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToInt32((System.Decimal)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("decimal.ToInt32({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Int64).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToInt64((System.Decimal)input);
        if (result == (System.Int64)output)
                  retVal = string.Format("decimal.ToInt64({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.UInt16).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToUInt16((System.Decimal)input);
        if (result == (System.UInt16)output)
                  retVal = string.Format("decimal.ToUInt16({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.UInt32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToUInt32((System.Decimal)input);
        if (result == (System.UInt32)output)
                  retVal = string.Format("decimal.ToUInt32({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.UInt64).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToUInt64((System.Decimal)input);
        if (result == (System.UInt64)output)
                  retVal = string.Format("decimal.ToUInt64({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Single).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.ToSingle((System.Decimal)input);
        if (result == (System.Single)output)
                  retVal = string.Format("decimal.ToSingle({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Decimal.Truncate((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("decimal.Truncate({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Double.IsInfinity((System.Double)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("double.IsInfinity({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Double.IsPositiveInfinity((System.Double)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("double.IsPositiveInfinity({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Double.IsNegativeInfinity((System.Double)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("double.IsNegativeInfinity({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Double.IsNaN((System.Double)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("double.IsNaN({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Round((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Round({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Round((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("Math.Round({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Floor((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("Math.Floor({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Floor((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Floor({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Ceiling((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("Math.Ceiling({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Ceiling((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Ceiling({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Acos((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Acos({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Asin((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Asin({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Atan((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Atan({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Cos((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Cos({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Cosh((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Cosh({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sin((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Sin({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Tan((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Tan({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sinh((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Sinh({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Tanh((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Tanh({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Truncate((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("Math.Truncate({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Truncate((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Truncate({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sqrt((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Sqrt({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Log((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Log({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Log10((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Log10({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Exp((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Exp({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.SByte)) &&
        typeof(System.SByte).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Abs((System.SByte)input);
        if (result == (System.SByte)output)
                  retVal = string.Format("Math.Abs({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int16)) &&
        typeof(System.Int16).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Abs((System.Int16)input);
        if (result == (System.Int16)output)
                  retVal = string.Format("Math.Abs({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int32)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Abs((System.Int32)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("Math.Abs({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int64)) &&
        typeof(System.Int64).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Abs((System.Int64)input);
        if (result == (System.Int64)output)
                  retVal = string.Format("Math.Abs({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Single)) &&
        typeof(System.Single).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Abs((System.Single)input);
        if (result == (System.Single)output)
                  retVal = string.Format("Math.Abs({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Double).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Abs((System.Double)input);
        if (result == (System.Double)output)
                  retVal = string.Format("Math.Abs({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Decimal).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Abs((System.Decimal)input);
        if (result == (System.Decimal)output)
                  retVal = string.Format("Math.Abs({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.SByte)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sign((System.SByte)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("Math.Sign({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int16)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sign((System.Int16)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("Math.Sign({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int32)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sign((System.Int32)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("Math.Sign({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int64)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sign((System.Int64)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("Math.Sign({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Single)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sign((System.Single)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("Math.Sign({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sign((System.Double)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("Math.Sign({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Decimal)) &&
        typeof(System.Int32).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Math.Sign((System.Decimal)input);
        if (result == (System.Int32)output)
                  retVal = string.Format("Math.Sign({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Single)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Single.IsInfinity((System.Single)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("float.IsInfinity({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Single)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Single.IsPositiveInfinity((System.Single)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("float.IsPositiveInfinity({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Single)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Single.IsNegativeInfinity((System.Single)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("float.IsNegativeInfinity({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Single)) &&
        typeof(System.Boolean).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.Single.IsNaN((System.Single)input);
        if (result == (System.Boolean)output)
                  retVal = string.Format("float.IsNaN({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.TimeSpan.FromDays((System.Double)input);
        if (result == (System.TimeSpan)output)
                  retVal = string.Format("TimeSpan.FromDays({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.TimeSpan.FromHours((System.Double)input);
        if (result == (System.TimeSpan)output)
                  retVal = string.Format("TimeSpan.FromHours({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.TimeSpan.FromMilliseconds((System.Double)input);
        if (result == (System.TimeSpan)output)
                  retVal = string.Format("TimeSpan.FromMilliseconds({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.TimeSpan.FromMinutes((System.Double)input);
        if (result == (System.TimeSpan)output)
                  retVal = string.Format("TimeSpan.FromMinutes({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Double)) &&
        typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.TimeSpan.FromSeconds((System.Double)input);
        if (result == (System.TimeSpan)output)
                  retVal = string.Format("TimeSpan.FromSeconds({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
        if (inputType.IsConvertibleTo(typeof(System.Int64)) &&
        typeof(System.TimeSpan).IsConvertibleTo(outputType))
    {
      string retVal = null;
      try
      {
        var result = System.TimeSpan.FromTicks((System.Int64)input);
        if (result == (System.TimeSpan)output)
                  retVal = string.Format("TimeSpan.FromTicks({0})", callChain);
      }
      catch {}
      if (retVal != null) yield return retVal;
    }
      // 4. Single-argument fragmentation (1-to-2 instance)
  if (!foundSomething)
  {
    
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Equals((System.String)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Substring((System.Int32)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.Substring({1})", callChain, 
            ((System.Int32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Text.NormalizationForm)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).IsNormalized((System.Text.NormalizationForm)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.IsNormalized({1})", callChain, 
            ((System.Text.NormalizationForm)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Text.NormalizationForm)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Normalize((System.Text.NormalizationForm)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.Normalize({1})", callChain, 
            ((System.Text.NormalizationForm)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).CompareTo((System.String)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Contains((System.String)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Contains({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).EndsWith((System.String)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.EndsWith({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Char)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).IndexOf((System.Char)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.IndexOf({1})", callChain, 
            ((System.Char)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Char[])))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).IndexOfAny((System.Char[])arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.IndexOfAny({1})", callChain, 
            ((System.Char[])arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).IndexOf((System.String)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.IndexOf({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Char)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).LastIndexOf((System.Char)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.LastIndexOf({1})", callChain, 
            ((System.Char)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Char[])))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).LastIndexOfAny((System.Char[])arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.LastIndexOfAny({1})", callChain, 
            ((System.Char[])arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).LastIndexOf((System.String)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.LastIndexOf({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).PadLeft((System.Int32)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.PadLeft({1})", callChain, 
            ((System.Int32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).PadRight((System.Int32)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.PadRight({1})", callChain, 
            ((System.Int32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).StartsWith((System.String)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.StartsWith({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Globalization.CultureInfo)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).ToLower((System.Globalization.CultureInfo)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToLower({1})", callChain, 
            ((System.Globalization.CultureInfo)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Globalization.CultureInfo)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).ToUpper((System.Globalization.CultureInfo)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToUpper({1})", callChain, 
            ((System.Globalization.CultureInfo)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Remove((System.Int32)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.Remove({1})", callChain, 
            ((System.Int32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is DateTime 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is DateTime 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.TimeSpan)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).Add((System.TimeSpan)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.Add({1})", callChain, 
            ((System.TimeSpan)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Double)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).AddDays((System.Double)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.AddDays({1})", callChain, 
            ((System.Double)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Double)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).AddHours((System.Double)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.AddHours({1})", callChain, 
            ((System.Double)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Double)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).AddMilliseconds((System.Double)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.AddMilliseconds({1})", callChain, 
            ((System.Double)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Double)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).AddMinutes((System.Double)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.AddMinutes({1})", callChain, 
            ((System.Double)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).AddMonths((System.Int32)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.AddMonths({1})", callChain, 
            ((System.Int32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Double)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).AddSeconds((System.Double)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.AddSeconds({1})", callChain, 
            ((System.Double)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int64)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).AddTicks((System.Int64)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.AddTicks({1})", callChain, 
            ((System.Int64)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).AddYears((System.Int32)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.AddYears({1})", callChain, 
            ((System.Int32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is DateTime 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.DateTime)))
    {
      if (input is DateTime 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).CompareTo((System.DateTime)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.DateTime)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is DateTime 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.DateTime)))
    {
      if (input is DateTime 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).Equals((System.DateTime)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.DateTime)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.DateTime)))
    {
      if (input is DateTime 
          && typeof(System.TimeSpan).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).Subtract((System.DateTime)arg);
        if (result == (System.TimeSpan)output)
        {
                    yield return string.Format("{0}.Subtract({1})", callChain, 
            ((System.DateTime)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.TimeSpan)))
    {
      if (input is DateTime 
          && typeof(System.DateTime).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).Subtract((System.TimeSpan)arg);
        if (result == (System.DateTime)output)
        {
                    yield return string.Format("{0}.Subtract({1})", callChain, 
            ((System.TimeSpan)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is DateTime 
          && typeof(System.String[]).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).GetDateTimeFormats((System.IFormatProvider)arg);
        if (result == (System.String[])output)
        {
                    yield return string.Format("{0}.GetDateTimeFormats({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Char)))
    {
      if (input is DateTime 
          && typeof(System.String[]).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).GetDateTimeFormats((System.Char)arg);
        if (result == (System.String[])output)
        {
                    yield return string.Format("{0}.GetDateTimeFormats({1})", callChain, 
            ((System.Char)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Boolean 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Boolean)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Boolean 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Boolean)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Boolean)))
    {
      if (input is Boolean 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Boolean)input).Equals((System.Boolean)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Boolean)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Boolean 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Boolean)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Boolean)))
    {
      if (input is Boolean 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Boolean)input).CompareTo((System.Boolean)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Boolean)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Byte 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Byte)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Byte)))
    {
      if (input is Byte 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Byte)input).CompareTo((System.Byte)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Byte)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Byte 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Byte)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Byte)))
    {
      if (input is Byte 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Byte)input).Equals((System.Byte)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Byte)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is Byte 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Byte)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Byte 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Byte)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Char 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Char)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Char)))
    {
      if (input is Char 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Char)input).Equals((System.Char)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Char)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Char 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Char)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Char)))
    {
      if (input is Char 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Char)input).CompareTo((System.Char)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Char)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Char 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Char)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Decimal 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Decimal)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Decimal)))
    {
      if (input is Decimal 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Decimal)input).CompareTo((System.Decimal)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Decimal)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Decimal 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Decimal)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Decimal)))
    {
      if (input is Decimal 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Decimal)input).Equals((System.Decimal)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Decimal)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is Decimal 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Decimal)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Decimal 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Decimal)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Double 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Double)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Double)))
    {
      if (input is Double 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Double)input).CompareTo((System.Double)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Double)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Double 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Double)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Double)))
    {
      if (input is Double 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Double)input).Equals((System.Double)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Double)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is Double 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Double)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Double 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Double)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Int16 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Int16)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int16)))
    {
      if (input is Int16 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Int16)input).CompareTo((System.Int16)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Int16)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Int16 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Int16)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int16)))
    {
      if (input is Int16 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Int16)input).Equals((System.Int16)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Int16)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Int16 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int16)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is Int16 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int16)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Int32 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Int32)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is Int32 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Int32)input).CompareTo((System.Int32)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Int32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Int32 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Int32)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is Int32 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Int32)input).Equals((System.Int32)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Int32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is Int32 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int32)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Int32 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int32)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Int64 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Int64)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int64)))
    {
      if (input is Int64 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Int64)input).CompareTo((System.Int64)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Int64)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Int64 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Int64)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Int64)))
    {
      if (input is Int64 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Int64)input).Equals((System.Int64)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Int64)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Int64 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int64)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is Int64 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int64)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Object 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Object)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is SByte 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.SByte)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.SByte)))
    {
      if (input is SByte 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.SByte)input).CompareTo((System.SByte)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.SByte)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is SByte 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.SByte)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.SByte)))
    {
      if (input is SByte 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.SByte)input).Equals((System.SByte)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.SByte)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is SByte 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.SByte)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is SByte 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.SByte)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Single 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Single)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Single)))
    {
      if (input is Single 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.Single)input).CompareTo((System.Single)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Single)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Single 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Single)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Single)))
    {
      if (input is Single 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Single)input).Equals((System.Single)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Single)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Single 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Single)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is Single 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Single)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.TimeSpan)))
    {
      if (input is TimeSpan 
          && typeof(System.TimeSpan).IsConvertibleTo(outputType))
      {
        var result = ((System.TimeSpan)input).Add((System.TimeSpan)arg);
        if (result == (System.TimeSpan)output)
        {
                    yield return string.Format("{0}.Add({1})", callChain, 
            ((System.TimeSpan)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is TimeSpan 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.TimeSpan)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.TimeSpan)))
    {
      if (input is TimeSpan 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.TimeSpan)input).CompareTo((System.TimeSpan)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.TimeSpan)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is TimeSpan 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.TimeSpan)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.TimeSpan)))
    {
      if (input is TimeSpan 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.TimeSpan)input).Equals((System.TimeSpan)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.TimeSpan)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.TimeSpan)))
    {
      if (input is TimeSpan 
          && typeof(System.TimeSpan).IsConvertibleTo(outputType))
      {
        var result = ((System.TimeSpan)input).Subtract((System.TimeSpan)arg);
        if (result == (System.TimeSpan)output)
        {
                    yield return string.Format("{0}.Subtract({1})", callChain, 
            ((System.TimeSpan)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is TimeSpan 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.TimeSpan)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is UInt16 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt16)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.UInt16)))
    {
      if (input is UInt16 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt16)input).CompareTo((System.UInt16)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.UInt16)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is UInt16 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt16)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.UInt16)))
    {
      if (input is UInt16 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt16)input).Equals((System.UInt16)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.UInt16)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is UInt16 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt16)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is UInt16 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt16)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is UInt32 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt32)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.UInt32)))
    {
      if (input is UInt32 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt32)input).CompareTo((System.UInt32)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.UInt32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is UInt32 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt32)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.UInt32)))
    {
      if (input is UInt32 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt32)input).Equals((System.UInt32)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.UInt32)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is UInt32 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt32)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is UInt32 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt32)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is UInt64 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt64)input).CompareTo((System.Object)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.UInt64)))
    {
      if (input is UInt64 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt64)input).CompareTo((System.UInt64)arg);
        if (result == (System.Int32)output)
        {
                    yield return string.Format("{0}.CompareTo({1})", callChain, 
            ((System.UInt64)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is UInt64 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt64)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.UInt64)))
    {
      if (input is UInt64 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt64)input).Equals((System.UInt64)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.UInt64)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is UInt64 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt64)input).ToString((System.IFormatProvider)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.IFormatProvider)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is UInt64 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt64)input).ToString((System.String)arg);
        if (result == (System.String)output)
        {
                    yield return string.Format("{0}.ToString({1})", callChain, 
            ((System.String)arg));
        }
      }
    }
        foreach (var arg in fragEngine.Frag(input, typeof(System.Object)))
    {
      if (input is Object 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.Object)input).Equals((System.Object)arg);
        if (result == (System.Boolean)output)
        {
                    yield return string.Format("{0}.Equals({1})", callChain, 
            ((System.Object)arg));
        }
      }
    }
    
    // 5. Two-argument fragmentation; like the above but with quad the complexity.
    if (!foundSomething)
  {
    
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.StringComparison)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Equals(
          (System.String)arg1,
          (System.StringComparison)arg2
        );
        if (result == (System.Boolean)output)
        {
          yield return string.Format("{0}.Equals({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Int32)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.Char[]).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).ToCharArray(
          (System.Int32)arg1,
          (System.Int32)arg2
        );
        if (result == (System.Char[])output)
        {
          yield return string.Format("{0}.ToCharArray({1}, {2})", callChain, 
            ((System.Int32)arg1),
            ((System.Int32)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Char[])))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.String[]).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Split(
          (System.Char[])arg1,
          (System.Int32)arg2
        );
        if (result == (System.String[])output)
        {
          yield return string.Format("{0}.Split({1}, {2})", callChain, 
            ((System.Char[])arg1),
            ((System.Char[])arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Char[])))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.StringSplitOptions)))
    {
      if (input is String 
          && typeof(System.String[]).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Split(
          (System.Char[])arg1,
          (System.StringSplitOptions)arg2
        );
        if (result == (System.String[])output)
        {
          yield return string.Format("{0}.Split({1}, {2})", callChain, 
            ((System.Char[])arg1),
            ((System.Char[])arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String[])))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.StringSplitOptions)))
    {
      if (input is String 
          && typeof(System.String[]).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Split(
          (System.String[])arg1,
          (System.StringSplitOptions)arg2
        );
        if (result == (System.String[])output)
        {
          yield return string.Format("{0}.Split({1}, {2})", callChain, 
            ((System.String[])arg1),
            ((System.String[])arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Int32)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Substring(
          (System.Int32)arg1,
          (System.Int32)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.Substring({1}, {2})", callChain, 
            ((System.Int32)arg1),
            ((System.Int32)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.StringComparison)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).EndsWith(
          (System.String)arg1,
          (System.StringComparison)arg2
        );
        if (result == (System.Boolean)output)
        {
          yield return string.Format("{0}.EndsWith({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Char)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).IndexOf(
          (System.Char)arg1,
          (System.Int32)arg2
        );
        if (result == (System.Int32)output)
        {
          yield return string.Format("{0}.IndexOf({1}, {2})", callChain, 
            ((System.Char)arg1),
            ((System.Char)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Char[])))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).IndexOfAny(
          (System.Char[])arg1,
          (System.Int32)arg2
        );
        if (result == (System.Int32)output)
        {
          yield return string.Format("{0}.IndexOfAny({1}, {2})", callChain, 
            ((System.Char[])arg1),
            ((System.Char[])arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).IndexOf(
          (System.String)arg1,
          (System.Int32)arg2
        );
        if (result == (System.Int32)output)
        {
          yield return string.Format("{0}.IndexOf({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.StringComparison)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).IndexOf(
          (System.String)arg1,
          (System.StringComparison)arg2
        );
        if (result == (System.Int32)output)
        {
          yield return string.Format("{0}.IndexOf({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Char)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).LastIndexOf(
          (System.Char)arg1,
          (System.Int32)arg2
        );
        if (result == (System.Int32)output)
        {
          yield return string.Format("{0}.LastIndexOf({1}, {2})", callChain, 
            ((System.Char)arg1),
            ((System.Char)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Char[])))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).LastIndexOfAny(
          (System.Char[])arg1,
          (System.Int32)arg2
        );
        if (result == (System.Int32)output)
        {
          yield return string.Format("{0}.LastIndexOfAny({1}, {2})", callChain, 
            ((System.Char[])arg1),
            ((System.Char[])arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).LastIndexOf(
          (System.String)arg1,
          (System.Int32)arg2
        );
        if (result == (System.Int32)output)
        {
          yield return string.Format("{0}.LastIndexOf({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.StringComparison)))
    {
      if (input is String 
          && typeof(System.Int32).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).LastIndexOf(
          (System.String)arg1,
          (System.StringComparison)arg2
        );
        if (result == (System.Int32)output)
        {
          yield return string.Format("{0}.LastIndexOf({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Int32)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Char)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).PadLeft(
          (System.Int32)arg1,
          (System.Char)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.PadLeft({1}, {2})", callChain, 
            ((System.Int32)arg1),
            ((System.Int32)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Int32)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Char)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).PadRight(
          (System.Int32)arg1,
          (System.Char)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.PadRight({1}, {2})", callChain, 
            ((System.Int32)arg1),
            ((System.Int32)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.StringComparison)))
    {
      if (input is String 
          && typeof(System.Boolean).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).StartsWith(
          (System.String)arg1,
          (System.StringComparison)arg2
        );
        if (result == (System.Boolean)output)
        {
          yield return string.Format("{0}.StartsWith({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Int32)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Insert(
          (System.Int32)arg1,
          (System.String)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.Insert({1}, {2})", callChain, 
            ((System.Int32)arg1),
            ((System.Int32)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Char)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Char)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Replace(
          (System.Char)arg1,
          (System.Char)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.Replace({1}, {2})", callChain, 
            ((System.Char)arg1),
            ((System.Char)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.String)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Replace(
          (System.String)arg1,
          (System.String)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.Replace({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Int32)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.Int32)))
    {
      if (input is String 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.String)input).Remove(
          (System.Int32)arg1,
          (System.Int32)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.Remove({1}, {2})", callChain, 
            ((System.Int32)arg1),
            ((System.Int32)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is DateTime 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.Char)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is DateTime 
          && typeof(System.String[]).IsConvertibleTo(outputType))
      {
        var result = ((System.DateTime)input).GetDateTimeFormats(
          (System.Char)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String[])output)
        {
          yield return string.Format("{0}.GetDateTimeFormats({1}, {2})", callChain, 
            ((System.Char)arg1),
            ((System.Char)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Byte 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Byte)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Decimal 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Decimal)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Double 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Double)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Int16 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int16)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Int32 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int32)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Int64 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Int64)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is SByte 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.SByte)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is Single 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.Single)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is TimeSpan 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.TimeSpan)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is UInt16 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt16)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is UInt32 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt32)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    foreach (var arg1 in fragEngine.Frag(input, typeof(System.String)))
    foreach (var arg2 in fragEngine.Frag(input, typeof(System.IFormatProvider)))
    {
      if (input is UInt64 
          && typeof(System.String).IsConvertibleTo(outputType))
      {
        var result = ((System.UInt64)input).ToString(
          (System.String)arg1,
          (System.IFormatProvider)arg2
        );
        if (result == (System.String)output)
        {
          yield return string.Format("{0}.ToString({1}, {2})", callChain, 
            ((System.String)arg1),
            ((System.String)arg2)
          );
        }
      }
    }
    
  // 5. Assuming we found nothing and aren't in too deep (any-to-1), look for
  //    methods which do NOT yield outputType
  if (!foundSomething && depth < 2)
  {
    if (input is String /* && typeof(System.Char[]) != outputType */)
    {
            System.Char[] result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).ToCharArray();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToCharArray()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String[]) != outputType */)
    {
            System.String[] result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).Split();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.Split()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).Trim();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.Trim()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).TrimStart();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.TrimStart()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).TrimEnd();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.TrimEnd()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.Boolean) != outputType */)
    {
            System.Boolean? result = new System.Boolean?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).IsNormalized();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.IsNormalized()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).Normalize();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.Normalize()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).ToLower();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToLower()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).ToLowerInvariant();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToLowerInvariant()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).ToUpper();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToUpper()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).ToUpperInvariant();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToUpperInvariant()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.Object) != outputType */)
    {
            System.Object result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).Clone();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.Clone()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).Trim();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.Trim()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is String /* && typeof(System.CharEnumerator) != outputType */)
    {
            System.CharEnumerator result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.String)input).GetEnumerator();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetEnumerator()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.Boolean) != outputType */)
    {
            System.Boolean? result = new System.Boolean?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).IsDaylightSavingTime();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.IsDaylightSavingTime()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.Int64) != outputType */)
    {
            System.Int64? result = new System.Int64?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToBinary();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.ToBinary()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.Double) != outputType */)
    {
            System.Double? result = new System.Double?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToOADate();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.ToOADate()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.Int64) != outputType */)
    {
            System.Int64? result = new System.Int64?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToFileTime();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.ToFileTime()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.Int64) != outputType */)
    {
            System.Int64? result = new System.Int64?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToFileTimeUtc();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.ToFileTimeUtc()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.DateTime) != outputType */)
    {
            System.DateTime? result = new System.DateTime?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToLocalTime();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.ToLocalTime()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToLongDateString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToLongDateString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToLongTimeString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToLongTimeString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToShortDateString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToShortDateString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToShortTimeString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToShortTimeString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.DateTime) != outputType */)
    {
            System.DateTime? result = new System.DateTime?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).ToUniversalTime();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.ToUniversalTime()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.String[]) != outputType */)
    {
            System.String[] result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).GetDateTimeFormats();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetDateTimeFormats()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is DateTime /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.DateTime)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Boolean /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Boolean)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Boolean /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Boolean)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Boolean /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Boolean)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Byte /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Byte)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Byte /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Byte)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Byte /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Byte)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Char /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Char)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Char /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Char)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Char /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Char)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Decimal /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Decimal)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Decimal /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Decimal)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Decimal /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Decimal)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Double /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Double)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Double /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Double)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Double /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Double)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int16 /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int16)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int16 /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int16)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int16 /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int16)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int32 /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int32)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int32 /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int32)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int32 /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int32)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int64 /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int64)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int64 /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int64)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Int64 /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Int64)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is SByte /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.SByte)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is SByte /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.SByte)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is SByte /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.SByte)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Single /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Single)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Single /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Single)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Single /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Single)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is TimeSpan /* && typeof(System.TimeSpan) != outputType */)
    {
            System.TimeSpan? result = new System.TimeSpan?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.TimeSpan)input).Duration();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.Duration()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is TimeSpan /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.TimeSpan)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is TimeSpan /* && typeof(System.TimeSpan) != outputType */)
    {
            System.TimeSpan? result = new System.TimeSpan?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.TimeSpan)input).Negate();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.Negate()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is TimeSpan /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.TimeSpan)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt16 /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt16)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt16 /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt16)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt16 /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt16)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt32 /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt32)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt32 /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt32)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt32 /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt32)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt64 /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt64)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt64 /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt64)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is UInt64 /* && typeof(System.TypeCode) != outputType */)
    {
            System.TypeCode? result = new System.TypeCode?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.UInt64)input).GetTypeCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetTypeCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.String) != outputType */)
    {
            System.String result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).ToString();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.ToString()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Int32) != outputType */)
    {
            System.Int32? result = new System.Int32?();
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetHashCode();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result.HasValue && !Equals(result.Value, input))
      {
        foreach (var c in FindCandidates(result.Value, output, depth+1,
          string.Format("{0}.GetHashCode()", callChain)))
        {
          yield return c;
        }
      }
      
    }
    if (input is Object /* && typeof(System.Type) != outputType */)
    {
            System.Type result = null;
            try {
        // invoke in the hope it yields something useful down the line
                result = ((System.Object)input).GetType();
                  
          
      } catch { /* cannot reasonably handle this */}

            if (result != null && !Equals(result, input))
      {
        foreach (var c in FindCandidates(result, output, depth+1,
          string.Format("{0}.GetType()", callChain)))
        {
          yield return c;
        }
      }
      
    }
      }
  }
}
    }
  }
}

