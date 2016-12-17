using System;
using System.Collections;
using System.Collections.Generic;

namespace CallSharp
{
  /// <summary>
  /// Provides a set of static methods for querying objects that implement IEnumerable&lt;T&gt;.
  /// </summary>
  /// <remarks>
  /// These are extensions to System.Linq.Enuemrable and declared and implemented in the same fashion.
  /// </remarks>
  public static class EnumerableExtensions
  {
    // This provides a useful extension-like method to find the index of and item from IEnumerable<T>
    // This was based off of the Enumerable.Count<T> extension method.
    /// <summary>
    /// Returns the index of an item in a sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence containing elements.</param>
    /// <param name="item">The item to locate.</param>        
    /// <returns>The index of the entry if it was found in the sequence; otherwise, -1.</returns>
    public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource item)
    {
      return IndexOf(source, item, null);
    }

    // This provides a useful extension-like method to find the index of and item from IEnumerable<T>
    // This was based off of the Enumerable.Count<T> extension method.
    /// <summary>
    /// Returns the index of an item in a sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <param name="source">A sequence containing elements.</param>
    /// <param name="item">The item to locate.</param>
    /// <param name="itemComparer">The item equality comparer to use.  Pass null to use the default comparer.</param>
    /// <returns>The index of the entry if it was found in the sequence; otherwise, -1.</returns>
    public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource item,
	    IEqualityComparer<TSource> itemComparer)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      var listOfT = source as IList<TSource>;
      if (listOfT != null)
      {
        return listOfT.IndexOf(item);
      }

      var list = source as IList;
      if (list != null)
      {
        return list.IndexOf(item);
      }

      if (itemComparer == null)
      {
        itemComparer = EqualityComparer<TSource>.Default;
      }

      int i = 0;
      foreach (TSource possibleItem in source)
      {
        if (itemComparer.Equals(item, possibleItem))
        {
          return i;
        }
        i++;
      }
      return -1;
    }
  }
}