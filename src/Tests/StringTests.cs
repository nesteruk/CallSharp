using NUnit.Framework;
using CallSharp;

namespace Tests
{
  [TestFixture]
  public class BasicMatchTests
  {
    private static MemberDatabase mdb = new MemberDatabase();

    [Test, Category(Categories.LongRunning)]
    public void SearchHasSuggestion(string search, string requiredCandidate)
    {

    }
  }
}
