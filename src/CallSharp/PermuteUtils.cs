using System.Collections.Generic;

namespace CallSharp
{
  public static class PermuteUtils
  {
    // Returns an enumeration of enumerators, one for each permutation
    // of the input.
    public static IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> list, int count)
    {
      if (count == 0)
      {
        yield return new T[0];
      }
      else
      {
        var startingElementIndex = 0;
        foreach (var startingElement in list)
        {
          var remainingItems = AllExcept(list, startingElementIndex);

          foreach (
            var permutationOfRemainder in Permute(remainingItems, count - 1))
          {
            yield return Concat(
              new[] {startingElement},
              permutationOfRemainder);
          }
          startingElementIndex += 1;
        }
      }
    }

    // Enumerates over contents of both lists.
    public static IEnumerable<T> Concat<T>(IEnumerable<T> a, IEnumerable<T> b)
    {
      foreach (var item in a)
      {
        yield return item;
      }
      foreach (var item in b)
      {
        yield return item;
      }
    }

    // Enumerates over all items in the input, skipping over the item
    // with the specified offset.
    public static IEnumerable<T> AllExcept<T>(IEnumerable<T> input, int indexToSkip)
    {
      var index = 0;
      foreach (var item in input)
      {
        if (index != indexToSkip) yield return item;
        index += 1;
      }
    }
  }
}