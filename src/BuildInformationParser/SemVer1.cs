using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BuildInformationParser {
  public class SemVer1 : ISemVer, IComparable<SemVer1> {
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

      return Major == other.Major && Minor == other.Minor && Patch == other.Patch && string.Equals( Suffix, other.Suffix, StringComparison.OrdinalIgnoreCase );
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

    public int CompareTo( SemVer1 other ) {
      if ( ReferenceEquals( this, other ) ) {
        return 0;
      }

      if ( ReferenceEquals( null, other ) ) {
        return 1;
      }

      var majorComparison = Major.CompareTo( other.Major );
      if ( majorComparison != 0 ) {
        return majorComparison;
      }

      var minorComparison = Minor.CompareTo( other.Minor );
      if ( minorComparison != 0 ) {
        return minorComparison;
      }

      var patchComparison = Patch.CompareTo( other.Patch );
      if ( patchComparison != 0 ) {
        return patchComparison;
      }

      if( Suffix == null && other.Suffix == null ) {
        return 0;
      }

      if( Suffix == null ) {
        return 1;
      }

      if( other.Suffix == null ) {
        return -1;
      }

      return string.Compare( Suffix, other.Suffix, StringComparison.OrdinalIgnoreCase );
    }

    public static bool operator <( SemVer1 left, SemVer1 right ) {
      return Comparer<SemVer1>.Default.Compare( left, right ) < 0;
    }

    public static bool operator >( SemVer1 left, SemVer1 right ) {
      return Comparer<SemVer1>.Default.Compare( left, right ) > 0;
    }

    public static bool operator <=( SemVer1 left, SemVer1 right ) {
      return Comparer<SemVer1>.Default.Compare( left, right ) <= 0;
    }

    public static bool operator >=( SemVer1 left, SemVer1 right ) {
      return Comparer<SemVer1>.Default.Compare( left, right ) >= 0;
    }
  }
}