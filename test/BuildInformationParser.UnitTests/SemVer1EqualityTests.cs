using NUnit.Framework;

namespace BuildInformationParser.UnitTests {
  [TestFixture]
  public class SemVer1EqualityTests {
    [Test]
    public void ReferenceEquals() {
      var sem1 = new SemVer1( 1, 2, 3, "-alpha001" );

      Assert.That( sem1.Equals( sem1 ), Is.True );
    }

    [Test]
    public void Equals() {
      var sem1 = new SemVer1( 1, 2, 3, "-alpha001" );
      var sem2 = new SemVer1( 1, 2, 3, "-alpha001" );

      Assert.That( sem1.Equals( sem2 ), Is.True );
    }

    [Test]
    public void Equals_With_Empty_Suffix() {
      var sem1 = new SemVer1( 1, 2, 3 );
      var sem2 = new SemVer1( 1, 2, 3, "" );

      Assert.That( sem1.Equals( sem2 ), Is.True );
    }
  }
}