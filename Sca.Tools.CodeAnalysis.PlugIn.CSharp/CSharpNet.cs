using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.Utilities;

namespace Sca.Tools.CodeAnalysis.PlugIn.CSharp
{
    class CSharpNet: iLanguagePlugIn
    {

        public TreeNode analyseCode(string folder)
        {
            Parser parser = new Parser();
            return parser.parseSourceProject(folder);
        }
    }
}
