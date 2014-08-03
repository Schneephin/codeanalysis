using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.Interfaces;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses
{
    public abstract class MatcherBase : IMatcher
    {
        public Token IsMatch(Tokenizer tokenizer)
        {
            if (tokenizer.End())
            {
                return new Token(TokenType.EOF);
            }

            tokenizer.TakeSnapshot();

            var match = IsMatchImpl(tokenizer);

            if (match == null)
            {
                tokenizer.RollbackSnapshot();
            }
            else
            {
                tokenizer.CommitSnapshot();
            }

            return match;
        }

        protected abstract Token IsMatchImpl(Tokenizer tokenizer);
    }
}
