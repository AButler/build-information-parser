#addin nuget:?package=BuildInformationParser&version=1.0.0-alpha001&prerelease
using BuildInformationParser;

var target = Argument( "target", "Build" );
var solution = "../BuildInformationParser.sln";

Setup<BuildInformation>( ctx => {
  var buildInfo = BuildInformation.Create(
    Argument( "Configuration", "Release" ),
    Argument( "TagName", "0.0.0-localbuild" ), 
    Argument( "Commit", (string)null )
  );

  Information( buildInfo.ToConsoleMessage() );
  Information( "----------------------------------------" );

  return buildInfo;
} );

Task( "CommitBuild" )
  .IsDependentOn( "Build" )
  .IsDependentOn( "Test" );

Task( "Release" )
  .IsDependentOn( "Pack" );

Task( "Build" )
  .Does<BuildInformation>( buildInfo => {
    var settings = new DotNetCoreBuildSettings {
      Configuration = buildInfo.Configuration,
      ArgumentCustomization = args => args
        .Append( $"/p:Version=\"{buildInfo.SemanticVersion}\"" )
        .Append( $"/p:AssemblyVersion=\"{buildInfo.AssemblyVersion}\"" )
        .Append( $"/p:FileVersion=\"{buildInfo.FileVersion}\"" )
    };

    DotNetCoreBuild( solution, settings );
  } );

Task( "Test" )
  .IsDependentOn( "Build" )
  .Does<BuildInformation>( buildInfo => {
    var settings = new DotNetCoreTestSettings {
      Configuration = buildInfo.Configuration,
      NoBuild = true
    };

    DotNetCoreTest( solution, settings );
  } );

Task( "Pack" )
  .IsDependentOn( "Build" )
  .Does<BuildInformation>( buildInfo => {
    var outputDirectory = @"..\packages-build";

    var settings = new DotNetCorePackSettings {
      Configuration = buildInfo.Configuration,
      OutputDirectory = outputDirectory,
      NoBuild = true,
      ArgumentCustomization = args => args
        .Append( $" /p:Version=\"{buildInfo.SemanticVersion}\"" )
        .Append( $" /p:Copyright=\"Copyright {DateTime.Now.Year} WebButler\"" )
        .Append( $" /p:RepositoryBranch=\"{buildInfo.TagName}\"" )
        .Append( $" /p:RepositoryCommit=\"{buildInfo.Commit}\"" )
    };

    DotNetCorePack( solution, settings );
  } );

// Must go after all tasks
RunTarget( target );