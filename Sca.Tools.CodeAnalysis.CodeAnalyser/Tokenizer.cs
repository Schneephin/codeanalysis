using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Sca.Tools.CodeAnalysis.CodeAnalyser.Lexer
{
    public class Tokenizer : TokenizableStreamBase<String>
    {
        public Tokenizer(String source)
            : base(() => source.ToCharArray().Select(i => i.ToString(CultureInfo.InvariantCulture)).ToList())
        {

        }
    }
}
