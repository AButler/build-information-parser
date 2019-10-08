using System;

namespace BuildInformationParser {
  public static class ISemVerExtensions {
    public static Version ToVersion( this ISemVer semVer, int fieldCount = 3, int revision = 0 ) {
      if ( fieldCount < 1 || fieldCount > 3 ) {
        throw new ArgumentOutOfRangeException( nameof(fieldCount), fieldCount, $"{nameof(fieldCount)} must be between 1 and 3" );
      }

      var minor = fieldCount >= 2 ? semVer.Minor : 0;
      var patch = fieldCount >= 3 ? semVer.Patch : 0;

      return new Version( semVer.Major, minor, patch, revision );
    }
  }
}