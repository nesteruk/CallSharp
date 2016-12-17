

// String
          // DateTime
          // Decimal
          // Decimal
          // Decimal
          // Decimal
          // Decimal
          // Decimal
          // Decimal
          // TimeSpan
           // cannot get exported types of Anonymously Hosted DynamicMethods Assembly 
namespace CallSharp
{
  using System;
  using System.Diagnostics;
  using System.Collections.Generic;

  class StaticMemberDatabase : IMemberDatabase
  {
    public IEnumerable<string> FindCandidates(object input, object output, int depth,
      string callChain = "input")
    {
      Trace.WriteLine(callChain);

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
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).ToCharArray();
            if (result == (System.Char[])output)
      {
                yield return string.Format("{0}.ToCharArray()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).Length;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Length", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).Split();
            if (result == (System.String[])output)
      {
                yield return string.Format("{0}.Split()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).Trim();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.Trim()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).TrimStart();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.TrimStart()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).TrimEnd();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.TrimEnd()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).IsNormalized();
            if (result == (System.Boolean)output)
      {
                yield return string.Format("{0}.IsNormalized()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).Normalize();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.Normalize()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).ToLower();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToLower()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).ToLowerInvariant();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToLowerInvariant()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).ToUpper();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToUpper()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).ToUpperInvariant();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToUpperInvariant()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).Clone();
            if (result == (System.Object)output)
      {
                yield return string.Format("{0}.Clone()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).Trim();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.Trim()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is String && typeof(String).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.String)input).GetEnumerator();
            if (result == (System.CharEnumerator)output)
      {
                yield return string.Format("{0}.GetEnumerator()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Day;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Day", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Hour;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Hour", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Month;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Month", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Minute;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Minute", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Second;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Second", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Year;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Year", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).IsDaylightSavingTime();
            if (result == (System.Boolean)output)
      {
                yield return string.Format("{0}.IsDaylightSavingTime()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToBinary();
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.ToBinary()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Date;
            if (result == (System.DateTime)output)
      {
                yield return string.Format("{0}.Date", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).DayOfWeek;
            if (result == (System.DayOfWeek)output)
      {
                yield return string.Format("{0}.DayOfWeek", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).DayOfYear;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.DayOfYear", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Kind;
            if (result == (System.DateTimeKind)output)
      {
                yield return string.Format("{0}.Kind", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Millisecond;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Millisecond", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).Ticks;
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.Ticks", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).TimeOfDay;
            if (result == (System.TimeSpan)output)
      {
                yield return string.Format("{0}.TimeOfDay", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToOADate();
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.ToOADate()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToFileTime();
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.ToFileTime()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToFileTimeUtc();
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.ToFileTimeUtc()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToLocalTime();
            if (result == (System.DateTime)output)
      {
                yield return string.Format("{0}.ToLocalTime()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToLongDateString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToLongDateString()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToLongTimeString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToLongTimeString()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToShortDateString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToShortDateString()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToShortTimeString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToShortTimeString()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).ToUniversalTime();
            if (result == (System.DateTime)output)
      {
                yield return string.Format("{0}.ToUniversalTime()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).GetDateTimeFormats();
            if (result == (System.String[])output)
      {
                yield return string.Format("{0}.GetDateTimeFormats()", callChain);
      }
    }
        if (input is DateTime && typeof(DateTime).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.DateTime)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Boolean && typeof(Boolean).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Boolean)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Boolean && typeof(Boolean).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Boolean)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Boolean && typeof(Boolean).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Boolean)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Byte && typeof(Byte).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Byte)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Byte && typeof(Byte).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Byte)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Byte && typeof(Byte).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Byte)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Char && typeof(Char).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Char)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Char && typeof(Char).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Char)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Char && typeof(Char).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Char)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Decimal && typeof(Decimal).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Decimal)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Decimal && typeof(Decimal).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Decimal)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Decimal && typeof(Decimal).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Decimal)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Double && typeof(Double).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Double)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Double && typeof(Double).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Double)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Double && typeof(Double).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Double)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Int16 && typeof(Int16).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int16)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Int16 && typeof(Int16).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int16)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Int16 && typeof(Int16).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int16)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Int32 && typeof(Int32).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int32)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Int32 && typeof(Int32).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int32)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Int32 && typeof(Int32).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int32)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Int64 && typeof(Int64).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int64)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Int64 && typeof(Int64).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int64)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Int64 && typeof(Int64).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Int64)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is SByte && typeof(SByte).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.SByte)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is SByte && typeof(SByte).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.SByte)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is SByte && typeof(SByte).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.SByte)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Single && typeof(Single).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Single)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Single && typeof(Single).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Single)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Single && typeof(Single).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Single)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Ticks;
            if (result == (System.Int64)output)
      {
                yield return string.Format("{0}.Ticks", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Days;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Days", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Hours;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Hours", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Milliseconds;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Milliseconds", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Minutes;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Minutes", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Seconds;
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.Seconds", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalDays;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalDays", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalHours;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalHours", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalMilliseconds;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalMilliseconds", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalMinutes;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalMinutes", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).TotalSeconds;
            if (result == (System.Double)output)
      {
                yield return string.Format("{0}.TotalSeconds", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Duration();
            if (result == (System.TimeSpan)output)
      {
                yield return string.Format("{0}.Duration()", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).Negate();
            if (result == (System.TimeSpan)output)
      {
                yield return string.Format("{0}.Negate()", callChain);
      }
    }
        if (input is TimeSpan && typeof(TimeSpan).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.TimeSpan)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is UInt16 && typeof(UInt16).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt16)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is UInt16 && typeof(UInt16).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt16)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is UInt16 && typeof(UInt16).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt16)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is UInt32 && typeof(UInt32).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt32)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is UInt32 && typeof(UInt32).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt32)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is UInt32 && typeof(UInt32).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt32)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is UInt64 && typeof(UInt64).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt64)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is UInt64 && typeof(UInt64).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt64)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is UInt64 && typeof(UInt64).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.UInt64)input).GetTypeCode();
            if (result == (System.TypeCode)output)
      {
                yield return string.Format("{0}.GetTypeCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).ToString();
            if (result == (System.String)output)
      {
                yield return string.Format("{0}.ToString()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetHashCode();
            if (result == (System.Int32)output)
      {
                yield return string.Format("{0}.GetHashCode()", callChain);
      }
    }
        if (input is Object && typeof(Object).IsConvertibleTo(output.GetType()))
    {
      // invoke!
            var result = ((System.Object)input).GetType();
            if (result == (System.Type)output)
      {
                yield return string.Format("{0}.GetType()", callChain);
      }
    }
        }
  }
}

