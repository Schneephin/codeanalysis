using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.Interfaces
{
    public interface IMatcher
    {
        Token IsMatch(Tokenizer tokenizer);
    }
}
