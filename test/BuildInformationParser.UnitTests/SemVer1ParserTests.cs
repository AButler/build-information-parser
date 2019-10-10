using System.Collections;
using NUnit.Framework;

namespace BuildInformationParser.UnitTests {
  [TestFixture]
  public class SemVer1ParserTests {
    public static IEnumerable ValidTestCaseSource {
      get {
        yield return new TestCaseData( "1.2.3", new SemVer1( 1, 2, 3 ) ).SetName( "Simple" );
        yield return new TestCaseData( "v1.2.3", new SemVer1( 1, 2, 3 ) ).SetName( "Simple (with prefix)" );

        yield return new TestCaseData( "01.02.03", new SemVer1( 1, 2, 3 ) ).SetName( "Preceding zeros" );
        yield return new TestCaseData( "v01.02.03", new SemVer1( 1, 2, 3 ) ).SetName( "Preceding zeros (with prefix)" );
        
        yield return new TestCaseData( "1.2.3-alpha001", new SemVer1( 1, 2, 3, "-alpha001" ) ).SetName( "Simple (with suffix)" );
        yield return new TestCaseData( "v1.2.3-alpha001", new SemVer1( 1, 2, 3, "-alpha001" ) ).SetName( "Simple (with prefix and suffix)" );
      }
    }

    [TestCaseSource( nameof(ValidTestCaseSource) )]
    public void Valid( string input, SemVer1 expected ) {
      var parser = new SemVer1Parser();

      var result = parser.TryParse( input, out var semVer );
      Assert.That( result, Is.True );
      Assert.That( semVer, Is.EqualTo( expected ) );
    }

    public static IEnumerable InvalidTestCaseSource {
      get {
        yield return new TestCaseData( "1.2.3+foo" ).SetName( "SemVer2 `+` syntax" );
        yield return new TestCaseData( "v1.2.3+foo" ).SetName( "SemVer2 `+` syntax (with prefix)" );
        
        yield return new TestCaseData( "1.2.3-build.1" ).SetName( "SemVer2 `.` syntax" );
        yield return new TestCaseData( "v1.2.3-build.1" ).SetName( "SemVer2 `.` syntax (with prefix)" );

        yield return new TestCaseData( "1.2" ).SetName( "Two fields" );
        yield return new TestCaseData( "v1.2" ).SetName( "Two fields (with prefix)" );

        yield return new TestCaseData( "1.2.3.4" ).SetName( "Four fields" );
        yield return new TestCaseData( "v1.2.3.4" ).SetName( "Four fields (with prefix)" );

        yield return new TestCaseData( "-1.2.3" ).SetName( "Negative" );
        yield return new TestCaseData( "v-1.2.3" ).SetName( "Negative (with prefix)" );

        yield return new TestCaseData( "1.2.3-" ).SetName( "No suffix" );
        yield return new TestCaseData( "v1.2.3-" ).SetName( "No suffix (with prefix)" );

        yield return new TestCaseData( null ).SetName( "Null" );
        yield return new TestCaseData( "" ).SetName( "Empty String" );
      }
    }

    [TestCaseSource( nameof(InvalidTestCaseSource) )]
    public void Invalid( string input ) {
      var parser = new SemVer1Parser();

      var result = parser.TryParse( input, out var semVer );
      Assert.That( result, Is.False );
      Assert.That( semVer, Is.Null );
    }
  }
}