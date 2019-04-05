using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using CallSharp;

namespace Tests
{
  [TestFixture]
  public class BasicMatchTests
  {
    private static readonly IMemberDatabase mdb = new StaticMemberDatabase();
    private static CancellationToken token = new CancellationToken();

    [Test, Category(Categories.LongRunning)]
    [TestCase("foo   ", "foo", "input.TrimEnd()")]
    [TestCase(" a b c ", "abc", "string.Concat(input.Split())")]
    [TestCase("xxyy", "xx", "input.TrimEnd('y')")]
    public void StringCalls(string input, string output, string requiredCandidate)
    {
      var candidates = new List<string>();
      mdb.FindCandidates(x => { candidates.Add(x); }, input, input, output, 2, token);
      Assert.That(candidates, Contains.Item(requiredCandidate));
    }

    [Test, Category(Categories.LongRunning)]
    [TestCase("abc", 3, "input.Length")]
    public void StringTransformations(string input, object output,
      string requiredCandidate)
    {
      var candidates = new List<string>();
      mdb.FindCandidates(x => candidates.Add(x), input, input, output, 2, token);
      Assert.That(candidates, Contains.Item(requiredCandidate));
    }

    [Test]
    public void FloatToIntImplicitTest()
    {
      var candidates = new List<string>();
      mdb.FindCandidates(x => candidates.Add(x), 1.0f, 1.0f, 1, 2, token);
      Assert.That(candidates, Contains.Item("input"));
    }

    [Test]
    public void IntToDoubleImplicitTest()
    {
      Assert.That(typeof(int).IsConvertibleTo(typeof(double)));
    }
  }
}
