using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallSharp;
using NUnit.Framework;

namespace Tests
{
  [TestFixture]
  public class FragTests
  {
    private FragmentationEngine fe;

    [SetUp]
    public void SetUp()
    {
      fe = new FragmentationEngine();
    }

    [Test]
    [TestCase("abc", 'a', 'b', 'c')]
    public void StringToCharFragmentationTests(string input, params char[] expectedOutputs)
    {
      var items = fe.Frag(input, typeof(char));
      foreach (var eo in expectedOutputs)
        Assert.That(items.Contains(eo), $"expected the result set [{string.Join(",", items)}] to contain [{eo}]");
    }

    [Test]
    [TestCase("a b c", " ", "a", " b ")]
    public void StringToStringFragmentationTest(string input, params string[] expectedOutputs)
    {
      var items = fe.Frag(input, typeof(string));
      foreach (var eo in expectedOutputs)
        Assert.That(items.Contains(eo), $"expected the result set [{string.Join(",", items)}] to contain [{eo}]");
    }
  }
}
