using System;
using System.Collections.Generic;

namespace VoiceControl
{
    public interface IProjectInformation
    {
        IEnumerable<string> Classes { get; }
        List<string> FileNames { get; }
        List<string> Files { get; }
        IEnumerable<string> DefinedClasses { get; }
        IEnumerable<string> DefinedFunctions { get; }
        IEnumerable<string> DefinedVariables { get; }
        IEnumerable<string> UsedTypes { get; }
        IEnumerable<string> UsedFunctions { get; }
        IEnumerable<string> UsedGenerics { get; }
        IEnumerable<string> UsedMembers { get; }

        IEnumerable<string> UsedNamespaces { get; }

        event Action Changed;

    
    }
}