using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.Interfaces;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;
using Sca.Tools.CodeAnalysis.CodeAnalyser.LanguageDependent;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer
{
    public class Lexer
    {
        private Tokenizer Tokenizer { get; set; }

        private List<IMatcher> Matchers { get; set; }

        public Lexer(String source)
        {
            Tokenizer = new Tokenizer(source);
        }

        public IEnumerable<Token> Lex()
        {
            Matchers = InitializeMatchList();

            var current = Next();

            while (current != null && current.TokenType != TokenType.EOF)
            {
                // skip whitespace
                if (current.TokenType != TokenType.WhiteSpace)
                {
                    yield return current;
                }

                current = Next();
            }
        }

        private List<IMatcher> InitializeMatchList()
        {
            // the order here matters because it defines token precedence

            var matchers = new List<IMatcher>(64);

            matchers.Add(new MatchString(MatchString.QUOTE));
            matchers.Add(new MatchString(MatchString.TIC));
            matchers.AddRange(LanguageDependentMatchKeywords.getSpecialCharacters());
            matchers.AddRange(LanguageDependentMatchKeywords.getMatchKeywords());
            matchers.AddRange(new List<IMatcher>
                                                {
                                                    new MatchWhiteSpace(),
                                                    new MatchNumber(),
                                                    new MatchWord(LanguageDependentMatchKeywords.getSpecialCharacters())
                                                });

            return matchers;
        }

        private Token Next()
        {
            if (Tokenizer.End())
            {
                return new Token(TokenType.EOF);
            }

            return
                 (from match in Matchers
                  let token = match.IsMatch(Tokenizer)
                  where token != null
                  select token).FirstOrDefault();
        }
    }
}
