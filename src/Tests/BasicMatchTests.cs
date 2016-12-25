using NUnit.Framework;
using CallSharp;

namespace Tests
{
  [TestFixture]
  public class BasicMatchTests
  {
    private static readonly IMemberDatabase mdb = new StaticMemberDatabase();

    [Test, Category(Categories.LongRunning)]
    [TestCase("foo   ", "foo", "input.TrimEnd()")]
    [TestCase(" a b c ", "abc", "string.Concat(input.Split())")]
    [TestCase("xxyy", "xx", "input.TrimEnd('y')")]
    public void StringCalls(string input, string output, string requiredCandidate)
    {
      Assert.That(mdb.FindCandidates(input,input, output, 2), Contains.Item(requiredCandidate));
    }

    [Test, Category(Categories.LongRunning)]
    [TestCase("abc", 3, "input.Length")]
    public void StringTransformations(string input, object output,
      string requiredCandidate)
    {
      Assert.That(mdb.FindCandidates(input,input, output, 2), Contains.Item(requiredCandidate));
    }

    [Test]
    public void FloatToIntImplicitTest()
    {
      Assert.That(mdb.FindCandidates(1.0f, 1.0f, 1, 2), Contains.Item("input"));
    }

    [Test]
    public void IntToDoubleImplicitTest()
    {
      Assert.That(typeof(int).IsConvertibleTo(typeof(double)));
    }
  }
}
