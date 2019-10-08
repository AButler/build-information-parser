using System;
using NUnit.Framework;

namespace BuildInformationParser.UnitTests {
  public class SemVer1ConstructorTests {
    [Test]
    public void Release_version() {
      var version = new SemVer1( 1, 2, 3 );

      Assert.That( version.Major, Is.EqualTo( 1 ) );
      Assert.That( version.Minor, Is.EqualTo( 2 ) );
      Assert.That( version.Patch, Is.EqualTo( 3 ) );
      Assert.That( version.IsPrerelease, Is.False );
      Assert.That( version.Suffix, Is.Null );
    }

    [Test]
    public void Release_version_empty_suffix() {
      var version = new SemVer1( 1, 2, 3, "" );

      Assert.That( version.Major, Is.EqualTo( 1 ) );
      Assert.That( version.Minor, Is.EqualTo( 2 ) );
      Assert.That( version.Patch, Is.EqualTo( 3 ) );
      Assert.That( version.IsPrerelease, Is.False );
      Assert.That( version.Suffix, Is.Null );
    }

    [Test]
    public void Prerelease_version() {
      var version = new SemVer1( 1, 2, 3, "-alpha001" );

      Assert.That( version.Major, Is.EqualTo( 1 ) );
      Assert.That( version.Minor, Is.EqualTo( 2 ) );
      Assert.That( version.Patch, Is.EqualTo( 3 ) );
      Assert.That( version.IsPrerelease, Is.True );
      Assert.That( version.Suffix, Is.EqualTo( "-alpha001" ) );
    }

    [Test]
    public void Major_cannot_be_below_zero() {
      Assert.That( () => { new SemVer1( -1, 0 ); }, Throws.InstanceOf<ArgumentOutOfRangeException>() );
    }

    [Test]
    public void Minor_cannot_be_below_zero() {
      Assert.That( () => { new SemVer1( 0, -1 ); }, Throws.InstanceOf<ArgumentOutOfRangeException>() );
    }

    [Test]
    public void Patch_cannot_be_below_zero() {
      Assert.That( () => { new SemVer1( 0, 0, -1 ); }, Throws.InstanceOf<ArgumentOutOfRangeException>() );
    }

    [Test]
    public void Suffix_must_start_with_hyphen() {
      Assert.That( () => { new SemVer1( 0, 0, 0, "foo" ); }, Throws.InstanceOf<ArgumentException>() );
    }

    [Test]
    public void Suffix_cannot_start_with_plus() {
      // SemVer1 cannot start with a plus
      Assert.That( () => { new SemVer1( 0, 0, 0, "+foo" ); }, Throws.InstanceOf<ArgumentException>() );
    }

    [Test]
    public void Suffix_must_be_alphanumeric() {
      Assert.That( () => { new SemVer1( 0, 0, 0, "-alpha!" ); }, Throws.InstanceOf<ArgumentException>() );
    }
  }
}