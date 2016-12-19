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
  public class OtherTests
  {
    [Test]
    public void Test()
    {
      var l = new[] {'x', 'y', 'z'}.ToLiteral();
      Assert.That(l, Is.EqualTo("new [] { 'x', 'y', 'z' }"));
    }
  }
}
