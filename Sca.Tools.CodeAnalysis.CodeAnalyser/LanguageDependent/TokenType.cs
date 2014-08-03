using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent
{
    public enum TokenType
    {
        NamespaceDeclaration,
        FunctionDeclaration,
        ClassDeclaration,
        Include,

        Void,
        WhiteSpace,
        LBracket,
        RBracket,
        OpenParenth,
        CloseParenth,
        LSquareBracket,
        RSquareBracket,
        Foreach,
        For,
        Plus,
        Minus,
        Equals,
        HashTag,
        QuotedString,
        Word,
        Comma,
        Asterix,
        Slash,
        Carat,
        DeRef,
        Ampersand,
        GreaterThan,
        LessThan,
        SemiColon,
        If,
        Return,
        While,
        Else,
        ScopeStart,
        EOF,
        Float,
        Print,
        Dot,
        True,
        False,
        Boolean,
        Or,
        Int,
        Double,
        String,
        New,
        Compare,
        Nil,
        NotCompare,
        Try,
        Catch,
        NewLine
    }
}
