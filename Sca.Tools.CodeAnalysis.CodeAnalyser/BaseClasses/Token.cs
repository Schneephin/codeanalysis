﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses
{
    public class Token
    {
        public TokenType TokenType { get; private set; }

        public String TokenValue { get; private set; }

        public Token(TokenType tokenType, String token)
        {
            TokenType = tokenType;
            TokenValue = token;
        }

        public Token(TokenType tokenType)
        {
            TokenValue = null;
            TokenType = tokenType;
        }

        public override string ToString()
        {
            return TokenType + ": " + TokenValue;
        }
    }
}
