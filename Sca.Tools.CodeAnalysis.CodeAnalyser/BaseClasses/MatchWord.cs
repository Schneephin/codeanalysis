using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.Interfaces;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses
{
    public class MatchWord : MatcherBase
    {
        private List<MatchKeyword> SpecialCharacters { get; set; }
        public MatchWord(IEnumerable<IMatcher> keywordMatchers)
        {
            SpecialCharacters = keywordMatchers.Select(i => i as MatchKeyword).Where(i => i != null).ToList();
        }

        protected override Token IsMatchImpl(Tokenizer tokenizer)
        {
            String current = null;

            while (!tokenizer.End() && !String.IsNullOrWhiteSpace(tokenizer.Current) && SpecialCharacters.All(m => m.Match != tokenizer.Current))
            {
                current += tokenizer.Current;
                tokenizer.Consume();
            }

            if (current == null)
            {
                return null;
            }

            // can't start a word with a special character
            if (SpecialCharacters.Any(c => current.StartsWith(c.Match)))
            {
                throw new Exception(String.Format("Cannot start a word with a special character {0}", current));
            }

            return new Token(TokenType.Word, current);
        }
    }
}
