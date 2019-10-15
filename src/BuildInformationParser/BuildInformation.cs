using System;
using System.Collections.Generic;

namespace BuildInformationParser {
  public class BuildInformation {
    public string Configuration { get; }

    public string TagName { get; }

    public Version FileVersion { get; }

    public Version AssemblyVersion { get; }

    public ISemVer SemanticVersion { get; }

    public string Commit { get; }

    public IDictionary<string, string> Data { get; }

    private BuildInformation( string configuration, string tagName, Version fileVersion, Version assemblyVersion, ISemVer semanticVersion, string commit ) {
      Configuration = configuration;
      TagName = tagName;
      FileVersion = fileVersion;
      AssemblyVersion = assemblyVersion;
      SemanticVersion = semanticVersion;
      Commit = commit;
      Data = new Dictionary<string, string>();
    }

    public static BuildInformation Create( string configuration, string rawTagName, string commit, int buildNumber = 0 ) {
      return Create( configuration, rawTagName, commit, buildNumber, BuildInformationSettings.Default );
    }

    public static BuildInformation Create( string configuration, string rawTagName, string commit, BuildInformationSettings settings ) {
      return Create( configuration, rawTagName, commit, 0, settings );
    }

    public static BuildInformation Create( string configuration, string rawTagName, string commit, int buildNumber, BuildInformationSettings settings ) {
      if ( settings == null ) {
        throw new ArgumentNullException( nameof(settings) );
      }

      if ( buildNumber < 0 ) {
        throw new ArgumentOutOfRangeException( nameof(buildNumber), buildNumber, "Build number must be greater than or equal to zero" );
      }

      var tagNameParser = new TagNameParser();
      var tagName = tagNameParser.Parse( rawTagName );

      var parser = new SemVer1Parser();

      var semVer = parser.Parse( tagName );

      var fileVersion = semVer.ToVersion( settings.FileVersionFieldCount, buildNumber );
      var assemblyVersion = semVer.ToVersion( settings.AssemblyVersionFieldCount );

      return new BuildInformation( configuration, tagName, fileVersion, assemblyVersion, semVer, commit );
    }
  }
}