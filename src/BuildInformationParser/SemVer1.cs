using System;
using System.Text.RegularExpressions;

namespace BuildInformationParser {
  public class SemVer1 : ISemVer {
    private static readonly Regex SuffixRegEx = new Regex( "^-[A-Za-z0-9-]+$" );

    public int Major { get; }

    public int Minor { get; }

    public int Patch { get; }

    public string Suffix { get; }

    public bool IsPrerelease => Suffix != null;

    public SemVer1( int major, int minor, int patch = 0, string suffix = null ) {
      if ( major < 0 ) {
        throw new ArgumentOutOfRangeException( nameof(major), major, $"{nameof(major)} must be greater than zero" );
      }

      if ( minor < 0 ) {
        throw new ArgumentOutOfRangeException( nameof(minor), major, $"{nameof(minor)} must be greater than zero" );
      }

      if ( patch < 0 ) {
        throw new ArgumentOutOfRangeException( nameof(patch), major, $"{nameof(patch)} must be greater than zero" );
      }

      Major = major;
      Minor = minor;
      Patch = patch;
      Suffix = GetValidSuffix( suffix );
    }

    private static string GetValidSuffix( string suffix ) {
      if ( string.IsNullOrEmpty( suffix ) ) {
        return null;
      }

      if ( !SuffixRegEx.IsMatch( suffix ) ) {
        throw new ArgumentException( "Invalid suffix", nameof(suffix) );
      }

      return suffix;
    }

    public override string ToString() {
      return $"{Major}.{Minor}.{Patch}{Suffix}";
    }

    public bool Equals( ISemVer other ) {
      if ( ReferenceEquals( null, other ) ) {
        return false;
      }

      if ( ReferenceEquals( this, other ) ) {
        return true;
      }

      return Major == other.Major && Minor == other.Minor && Patch == other.Patch && Suffix == other.Suffix;
    }

    public override bool Equals( object obj ) {
      if ( ReferenceEquals( null, obj ) ) {
        return false;
      }

      if ( ReferenceEquals( this, obj ) ) {
        return true;
      }

      if ( obj is ISemVer objSemVer ) {
        return Equals( objSemVer );
      }

      return false;
    }

    public override int GetHashCode() {
      unchecked {
        var hashCode = Major;
        hashCode = ( hashCode * 397 ) ^ Minor;
        hashCode = ( hashCode * 397 ) ^ Patch;
        hashCode = ( hashCode * 397 ) ^ ( Suffix != null ? Suffix.GetHashCode() : 0 );
        return hashCode;
      }
    }
  }
}