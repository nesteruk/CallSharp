using System;
using System.Collections.Generic;
using System.Reflection;

namespace CallSharp
{
  public interface IMemberDatabase
  {
    IEnumerable<string> FindCandidates(object origin, object input, object output, int depth, string callChain = "input");
  }
}