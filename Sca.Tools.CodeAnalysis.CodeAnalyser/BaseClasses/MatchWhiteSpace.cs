using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses
{
    class MatchWhiteSpace : MatcherBase
    {
        protected override Token IsMatchImpl(Tokenizer tokenizer)
        {
            bool foundWhiteSpace = false;

            while (!tokenizer.End() && String.IsNullOrWhiteSpace(tokenizer.Current))
            {
                foundWhiteSpace = true;

                tokenizer.Consume();
            }

            if (foundWhiteSpace)
            {
                return new Token(TokenType.WhiteSpace);
            }

            return null;
        }
    }
}
