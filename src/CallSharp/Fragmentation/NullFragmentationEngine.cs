using System;
using System.Collections.Generic;

namespace CallSharp
{
  /// <summary>
  /// When you're too scared to search through combinations of strings or characters.
  /// </summary>
  class NullFragmentationEngine : IFragmentationEngine
  {
    public IEnumerable<object> Frag(object source, Type partType)
    {
      yield break; // nothing here!
    }
  }
}