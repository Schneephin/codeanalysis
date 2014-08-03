using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;

namespace Sca.Tools.CodeAnalysis.Utilities
{
    public static class ExtensionMethods
    {
        public static bool IsOneOf<T>(this T value, params T[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                if (items[i].Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static int PeekUntil(this ParseableTokenStream value, TokenType needle, int maxLookAhead, params TokenType[] excluding)
        {
            int currentLookAhead = 1;
            while (value.Peek(currentLookAhead).TokenType != needle && currentLookAhead < maxLookAhead)
            {
                if (value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(excluding))
                {
                    return -1;
                }
                currentLookAhead++;
            }
            if (value.Peek(currentLookAhead).TokenType == needle)
            {
                return currentLookAhead;
            }

            return -1;
        }

        public static int PeekUntil(this ParseableTokenStream value, TokenType needle, int startLookAhead, int maxLookAhead, params TokenType[] excluding)
        {
            int currentLookAhead = startLookAhead;
            while (value.Peek(currentLookAhead).TokenType != needle && currentLookAhead < maxLookAhead)
            {
                if (value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(excluding))
                {
                    return -1;
                }
                currentLookAhead++;
            }
            if (value.Peek(currentLookAhead).TokenType == needle)
            {
                return currentLookAhead;
            }

            return -1;
        }

        public static int PeekBackUntilMatchingOpen(this ParseableTokenStream value, TokenType open, TokenType close, int lookAhead)
        {
            int currentLookBack = lookAhead;
            int depth = 0;
            while (value.Peek(currentLookBack).TokenType != open && currentLookBack > 0 || depth != 1)
            {
                if (value.Peek(currentLookBack).TokenType.IsOneOf<TokenType>(close))
                {
                    depth++;
                }
                if (value.Peek(currentLookBack).TokenType.IsOneOf<TokenType>(open) && depth > 0)
                {
                    depth--;
                }
                currentLookBack--;
            }
            if (value.Peek(currentLookBack).TokenType == open)
            {
                return currentLookBack;
            }

            return -1;
        }

        public static int PeekAheadUntilMatchingClose(this ParseableTokenStream value, TokenType open, TokenType close)
        {
            int currentLookAhead = 1;
            int depth = 0;
            while ((!value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(close) || depth > 0) && !value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(TokenType.EOF))
            {
                if (value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(open))
                {
                    depth++;
                }
                if (value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(close) && depth > 0)
                {
                    depth--;
                }
                currentLookAhead++;
            }
            if (value.Peek(currentLookAhead).TokenType == close)
            {
                return value.getCurrentIndex() + currentLookAhead;
            }

            return -1;
        }

        public static int CountTokenTypeUntilMatchingClose(this ParseableTokenStream value, TokenType open, TokenType close, TokenType needle)
        {
            int currentLookAhead = 1;
            int counts = 0;
            int depth = 0;
            while ((!value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(close) || depth > 0) && !value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(TokenType.EOF))
            {
                if (value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(open))
                {
                    depth++;
                }
                if (value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(close) && depth > 0)
                {
                    depth--;
                }
                if (value.Peek(currentLookAhead).TokenType.IsOneOf<TokenType>(needle))
                {
                    counts++;
                }
                currentLookAhead++;
            }
            if (value.Peek(currentLookAhead).TokenType == close)
            {
                return counts;
            }

            return -1;
        }

        public static String getPeekValues(this ParseableTokenStream value, int startLookAhead, int endLookAhead, String seperator = "")
        {
            String values = "";
            for (int i = startLookAhead; i <= endLookAhead; i++)
            {
                values += value.Peek(i).TokenValue + seperator;
            }
            return values;
        }

        public static String getPeekBackValuesOfType(this ParseableTokenStream value, int startLookBack, params TokenType[] types)
        {
            List<String> values = new List<String>();
            int currentLookBack = startLookBack;
            while (value.PeekBack(currentLookBack).TokenType.IsOneOf<TokenType>(types))
            {
                values.Add(value.PeekBack(currentLookBack).TokenValue);
                currentLookBack++;
            }
            values.Reverse();
            return String.Join("", values.ToArray<String>());
        }
    }
}
