using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace CallSharp
{
  public interface IMemberDatabase
  {
    void FindCandidates(Action<string> visitor, object origin, object input, 
      object output, int depth, CancellationToken token, string callChain = "input");
  }
}