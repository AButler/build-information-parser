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
    public void Equals_Different_Case() {
      var sem1 = new SemVer1( 1, 2, 3, "-alpha001" );
      var sem2 = new SemVer1( 1, 2, 3, "-ALPHA001" );

      Assert.That( sem1.Equals( sem2 ), Is.True );
    }

    [Test]
    public void Equals_With_Empty_Suffix() {
      var sem1 = new SemVer1( 1, 2, 3 );
      var sem2 = new SemVer1( 1, 2, 3, "" );

      Assert.That( sem1.Equals( sem2 ), Is.True );
    }

    [Test]
    public void Not_Equal_Prerelease() {
      var sem1 = new SemVer1( 1, 2, 3, "-alpha001" );
      var sem2 = new SemVer1( 1, 2, 3 );

      Assert.That( sem1.Equals( sem2 ), Is.False );
    }

    [Test]
    public void Not_Equal_Patch() {
      var sem1 = new SemVer1( 1, 2, 0 );
      var sem2 = new SemVer1( 1, 2, 3 );

      Assert.That( sem1.Equals( sem2 ), Is.False );
    }

    [Test]
    public void Not_Equal_Minor() {
      var sem1 = new SemVer1( 1, 0, 0 );
      var sem2 = new SemVer1( 1, 2, 0 );

      Assert.That( sem1.Equals( sem2 ), Is.False );
    }

    [Test]
    public void Not_Equal_Major() {
      var sem1 = new SemVer1( 2, 0, 0 );
      var sem2 = new SemVer1( 1, 2, 3 );

      Assert.That( sem1.Equals( sem2 ), Is.False );
    }
  }
}