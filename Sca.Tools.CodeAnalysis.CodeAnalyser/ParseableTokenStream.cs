using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer
{
    public class ParseableTokenStream : TokenizableStreamBase<Token>
    {
        public ParseableTokenStream(Lexer lexer)
            : base(() => lexer.Lex().ToList())
        {
        }

        public Boolean IsMatch(TokenType type)
        {
            if (Current.TokenType == type)
            {
                return true;
            }

            return false;
        }

        public Token Take(TokenType type)
        {
            if (IsMatch(type))
            {
                var current = Current;

                Consume();
                
                return current;
            }

            throw new Exception(String.Format("Invalid Syntax. Expecting {0} but got {1}", type, Current.TokenType));
        }

        public override Token Peek(int lookahead)
        {
            var peeker = base.Peek(lookahead);

            if (peeker == null)
            {
                return new Token(TokenType.EOF);
            }

            return peeker;
        }

        public override Token Current
        {
            get
            {
                var current = base.Current;
                if (current == null)
                {
                    return new Token(TokenType.EOF);
                }
                return current;
            }
        }

        public int getCurrentIndex()
        {
            return base.Index;
        }
    }
}
