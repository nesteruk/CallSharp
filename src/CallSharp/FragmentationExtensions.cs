using System;
using System.Collections.Generic;
using System.Linq;

namespace CallSharp
{
  /// <summary>
  /// Contains extension methods that fragment a literal into its constituent
  /// parts for exploratory call chains. For example, 123 can be fragged into
  /// 1, 2, 3, 12, 23, 13, 100 and 20.
  /// </summary>
  public class FragmentationEngine
  {
    /// <summary>
    /// Finds all constituent parts of 
    /// </summary>
    /// <param name="source">The object to fragment.</param>
    /// <param name="partType">The type of each of the parts after fragmentation.</param>
    /// <returns></returns>
    public IEnumerable<object> Frag(object source, Type partType)
    {
      var s = source as string;
      if (s != null && partType == typeof(string))
      {
        foreach (var part in FragToString(s))
          yield return part;
        foreach (var part in FragToInt(s))
          yield return part;
      }


    }

    private IEnumerable<int> FragToInt(string text)
    {
      return Enumerable.Range(0, text.Length);
    }

    private IEnumerable<string> FragToString(string text)
    {
      return Enumerable.Range(0, text.Length)
          .SelectMany(i => Enumerable.Range(0, text.Length - i + 1), (i, j) => new { i, j })
          .Where(t => t.j >= 2)
          .Select(t => text.Substring(t.i, t.j));
    }

  }
}