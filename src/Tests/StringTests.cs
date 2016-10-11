using NUnit.Framework;
using CallSharp;

namespace Tests
{
  [TestFixture]
  public class BasicMatchTests
  {
    private static MemberDatabase mdb = new MemberDatabase();

    [Test, Category(Categories.LongRunning)]
    [TestCase("foo   ", "foo", "input.TrimEnd()")]
    [TestCase(" a b c ", "abc", "string.Concat(input.Split())")]
    public void StringCalls(string input, string output, string requiredCandidate)
    {
      Assert.That(mdb.FindCandidates(input, output, 2), Contains.Item(requiredCandidate));
    }
  }
}
