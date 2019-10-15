using System;
using System.Text.RegularExpressions;

namespace BuildInformationParser {
  public class TagNameParser {
    private static readonly Regex TagNameRegex = new Regex( @"^refs/tags/(?<TagName>.+)$" );

    public string Parse( string rawTagName ) {
      if ( rawTagName == null ) {
        throw new ArgumentNullException( nameof(rawTagName) );
      }

      var match = TagNameRegex.Match( rawTagName );

      if ( !match.Success ) {
        throw new ArgumentException( "Invalid tag name", nameof(rawTagName) );
      }

      var tagName = match.Groups["TagName"].Value;

      if ( string.IsNullOrWhiteSpace( tagName ) ) {
        throw new ArgumentException( "Invalid tag name", nameof(rawTagName) );
      }

      return tagName;
    }
  }
}