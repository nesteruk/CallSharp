using System;
using System.Collections.Generic;

namespace CallSharp
{
  public interface IFragmentationEngine
  {
    /// <summary>
    /// Finds all constituent parts of 
    /// </summary>
    /// <param name="source">The object to fragment.</param>
    /// <param name="partType">The type of each of the parts after fragmentation.</param>
    /// <returns></returns>
    IEnumerable<object> Frag(object source, Type partType);
  }
}