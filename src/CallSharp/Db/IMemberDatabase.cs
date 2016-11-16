using System;
using System.Collections.Generic;
using System.Reflection;

namespace CallSharp
{
  public interface IMemberDatabase
  {
    /// <summary>
    /// Locates any non-static method of <code>inputType</code> that yields a
    /// value that is expressly <em>not</em> of <c>ignoreOutputType</c>.
    /// </summary>
    /// <param name="inputType">The type on which to search for member functions.</param>
    /// <param name="ignoreThisOutputType">Optional return type to avoid including in the results.</param>
    /// <remarks>
    /// The reason why <c>ignoreOutputType</c> exists is that when making the primary search,
    /// we search explicitly for f:A->B and already have cookies from those searches. We want
    /// to avoid performing the search again, so we search for f:A->Z where Z != B.
    /// </remarks>
    IEnumerable<MethodInfo> FindAnyToOneInstance(Type inputType, Type ignoreThisOutputType);

    /// <summary>
    /// This search ought to locate non-void extension methods available in extraneous types, such as
    /// <c>IEnumerable</c> extension methods.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="ignoreThisOutputType"></param>
    /// <returns></returns>
    IEnumerable<MethodInfo> FindAnyToOneStatic(Type inputType,
      Type ignoreThisOutputType);

    IEnumerable<ConstructorInfo> FindConstructorFor(Type inputType, Type outputType);

    /// <summary>
    /// Locate any non-static method of <c>inputType</c> that takes no parameters and
    /// returns a value of <c>outputType</c>.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="outputType"></param>
    /// <returns></returns>
    IEnumerable<MethodInfo> FindOneToOneInstance(Type inputType, Type outputType);

    /// <summary>
    /// Locate any non-static method of <c>inputType</c> that takes a single parameter or
    /// a <c>params[]</c>.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="outputType"></param>
    /// <returns></returns>
    IEnumerable<MethodInfo> FindOneToTwoInstance(Type inputType, Type outputType);

    /// <summary>
    /// Locates all static methods of any type that is in <see cref="TypeDatabase.CoreTypes"/>
    /// that takes an argument of <c>inputType</c> and returns a value of <c>outputType</c>.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="outputType"></param>
    /// <returns></returns>
    IEnumerable<MethodInfo> FindOneToOneStatic(Type inputType, Type outputType);

    /// <summary>
    /// Finds member functions that take 2 arguments and return a value of <c>outputType</c>.
    /// </summary>
    /// <param name="inputType"></param>
    /// <param name="outputType"></param>
    /// <returns></returns>
    IEnumerable<MethodInfo> FindOneToThreeInstance(Type inputType, Type outputType);


    IEnumerable<string> FindCandidates(object input, object output, int depth, string callChain = "input");
  }
}