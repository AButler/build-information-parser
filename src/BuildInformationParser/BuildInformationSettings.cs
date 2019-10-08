using System;

namespace BuildInformationParser {
  public class BuildInformationSettings {
    private const int DefaultAssemblyVersionFieldCount = 2;
    private const int DefaultFileVersionFieldCount = 3;

    private int _fileVersionFieldCount;
    private int _assemblyVersionFieldCount;

    public BuildInformationSettings() {
      _assemblyVersionFieldCount = DefaultAssemblyVersionFieldCount;
      _fileVersionFieldCount = DefaultFileVersionFieldCount;
    }

    public int FileVersionFieldCount {
      get => _fileVersionFieldCount;
      set {
        if ( value < 1 || value > 3 ) {
          throw new ArgumentOutOfRangeException( nameof(value), "Field Count must be between 1 and 3" );
        }

        _fileVersionFieldCount = value;
      }
    }

    public int AssemblyVersionFieldCount {
      get => _assemblyVersionFieldCount;
      set {
        if ( value < 1 || value > 3 ) {
          throw new ArgumentOutOfRangeException( nameof(value), "Field Count must be between 1 and 3" );
        }

        _assemblyVersionFieldCount = value;
      }
    }

    public static BuildInformationSettings Default =>
      new BuildInformationSettings {
        AssemblyVersionFieldCount = DefaultAssemblyVersionFieldCount,
        FileVersionFieldCount = DefaultFileVersionFieldCount
      };
  }
}