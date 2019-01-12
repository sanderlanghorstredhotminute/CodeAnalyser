using System;

namespace CodeAnalysis.Analysers.Dependency
{
    [Flags]
    public enum DependencyOptions
    {
        IncludeSystemTypes = 1 << 0,
        IncludeSelfReferences = 1 << 1
    }
}