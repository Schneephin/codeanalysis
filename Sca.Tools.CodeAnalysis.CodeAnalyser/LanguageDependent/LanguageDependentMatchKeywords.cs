using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.Interfaces;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.BaseClasses;
using Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer.LanguageDependent;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.LanguageDependent
{
    public static class LanguageDependentMatchKeywords
    {
        public static List<IMatcher> keywords;
        public static List<IMatcher> specialCharacters;

        public static void setKeywords(List<IMatcher> keywords, List<IMatcher> specialCharacters)
        {
            LanguageDependentMatchKeywords.keywords = keywords;
            LanguageDependentMatchKeywords.specialCharacters = specialCharacters;
        }

        public static List<IMatcher> getMatchKeywords()
        {
            if (keywords == null || specialCharacters == null) throw new Exception();
            // give each keyword the list of possible delimiters and not allow them to be 
            // substrings of other words, i.e. token fun should not be found in string "function"
            keywords.ForEach(keyword =>
            {
                var current = (keyword as MatchKeyword);
                current.AllowAsSubString = false;
                current.SpecialCharacters = specialCharacters.Select(i => i as MatchKeyword).ToList();
            });

            return keywords;
        }

        public static List<IMatcher> getSpecialCharacters()
        {
            if (keywords == null || specialCharacters == null) throw new Exception();
            return specialCharacters;
        }
    }
}
