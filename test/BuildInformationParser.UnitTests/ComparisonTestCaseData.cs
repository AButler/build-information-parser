using System.Collections;
using NUnit.Framework;

namespace BuildInformationParser.UnitTests {
  public static class ComparisonTestCaseData {
    public static IEnumerable TestCaseSource {
      get {
        yield return new TestCaseData( "1.0.0", "2.0.0" ).SetName( "+Major, =Minor, =Patch" );
        yield return new TestCaseData( "1.1.0", "2.0.0" ).SetName( "+Major, +Minor, =Patch" );
        yield return new TestCaseData( "1.1.1", "2.0.0" ).SetName( "+Major, +Minor, +Patch" );

        yield return new TestCaseData( "1.0.0-alpha001", "2.0.0" ).SetName( "+Major, =Minor, =Patch, Prerelease" );
        yield return new TestCaseData( "1.1.0-alpha001", "2.0.0" ).SetName( "+Major, +Minor, =Patch, Prerelease" );
        yield return new TestCaseData( "1.1.1-alpha001", "2.0.0" ).SetName( "+Major, +Minor, +Patch, Prerelease" );

        yield return new TestCaseData( "1.0.0-alpha001", "2.0.0-alpha001" ).SetName( "+Major, =Minor, =Patch, Prerelease-Prerelease" );
        yield return new TestCaseData( "1.1.0-alpha001", "2.0.0-alpha001" ).SetName( "+Major, +Minor, =Patch, Prerelease-Prerelease" );
        yield return new TestCaseData( "1.1.1-alpha001", "2.0.0-alpha001" ).SetName( "+Major, +Minor, +Patch, Prerelease-Prerelease" );

        yield return new TestCaseData( "1.0.0-alpha001", "1.0.0" ).SetName( "=Major, =Minor, =Patch, Prerelease!=Release" );
        yield return new TestCaseData( "1.0.0-alpha001", "1.0.0-alpha002" ).SetName( "=Major, =Minor, =Patch, -Prerelease-Prerelease" );
      }
    }
  }
}