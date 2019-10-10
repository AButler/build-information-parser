using NUnit.Framework;

namespace BuildInformationParser.UnitTests {
  public class SemVer1GreaterThanTests {
    [TestCaseSource( typeof( ComparisonTestCaseData ), nameof(ComparisonTestCaseData.TestCaseSource) )]
    public void GreaterThan( string version1, string version2 ) {
      var parser = new SemVer1Parser();

      var semVer1 = parser.Parse( version1 );
      var semVer2 = parser.Parse( version2 );

      Assert.That( semVer2, Is.GreaterThan( semVer1 ) );
    }
  }
}