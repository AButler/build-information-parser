using System.Text;

namespace BuildInformationParser {
  public static class BuildInformationExtensions {
    public static string ToConsoleMessage( this BuildInformation buildInfo ) {
      var sb = new StringBuilder();

      sb.AppendLine( $"Tag Name      : {buildInfo.TagName}" );
      sb.AppendLine( $"Configuration : {buildInfo.Configuration}" );
      sb.AppendLine( $"SemVer        : {buildInfo.SemanticVersion}" );
      sb.AppendLine( $"IsPrerelease  : {buildInfo.SemanticVersion.IsPrerelease}" );
      sb.AppendLine( $"Assembly      : {buildInfo.AssemblyVersion}" );
      sb.AppendLine( $"File          : {buildInfo.FileVersion}" );
      sb.AppendLine( $"Commit        : {buildInfo.Commit}" );

      return sb.ToString().Trim();
    }
  }
}
