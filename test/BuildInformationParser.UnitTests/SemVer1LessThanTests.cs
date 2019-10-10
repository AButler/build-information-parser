using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace BuildInformationParser.UnitTests {
  [TestFixture]
  public class SemVer1LessThanTests {
    [TestCaseSource( typeof( ComparisonTestCaseData ), nameof(ComparisonTestCaseData.TestCaseSource) )]
    public void LessThan( string version1, string version2 ) {
      var parser = new SemVer1Parser();

      var semVer1 = parser.Parse( version1 );
      var semVer2 = parser.Parse( version2 );

      Assert.That( semVer1, Is.LessThan( semVer2 ) );
    }
  }
}