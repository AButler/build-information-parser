using System;

namespace BuildInformationParser {
  public interface ISemVer : IEquatable<ISemVer> {
    int Major { get; }

    int Minor { get; }

    int Patch { get; }

    string Suffix { get; }

    bool IsPrerelease { get; }
  }
}