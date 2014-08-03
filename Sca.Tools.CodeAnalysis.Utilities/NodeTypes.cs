using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sca.Tools.CodeAnalysis.Utilities
{
    public enum NodeTypes
    {
        ProjectDeclaration,
        NamespaceDeclaration,
        ClassDeclaration,
        FunctionDeclaration,
        VariableDeclaration,
        FunctionCall,
        Assignment,
        Include,
        Loop,
        Conditional,
        Comment,
        Default
    }
}
