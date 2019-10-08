using System;
using System.Text.RegularExpressions;

namespace BuildInformationParser {
  public class SemVer1Parser {
    private static readonly Regex SemVer1Regex = new Regex( @"^v?(?<Major>\d+)\.(?<Minor>\d+)\.(?<Patch>\d+)(?<Suffix>-[A-Za-z0-9-]+)?$" );

    public bool TryParse( string version, out SemVer1 semVer ) {
      if ( string.IsNullOrWhiteSpace( version ) ) {
        semVer = null;
        return false;
      }

      var match = SemVer1Regex.Match( version );

      if ( !match.Success ) {
        semVer = null;
        return false;
      }

      var major = int.Parse( match.Groups["Major"].Value );
      var minor = int.Parse( match.Groups["Minor"].Value );
      var patch = match.Groups["Patch"].Success ? int.Parse( match.Groups["Patch"].Value ) : 0;
      var suffix = match.Groups["Suffix"].Value;

      semVer = new SemVer1( major, minor, patch, suffix );

      return true;
    }

    public SemVer1 Parse( string version ) {
      if ( TryParse( version, out var semVer ) ) {
        return semVer;
      }

      throw new ArgumentException( "Invalid version string", nameof(version) );
    }
  }
}