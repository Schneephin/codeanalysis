using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sca.Tools.CodeAnalysis.PlugIn.CSharp
{
    class SourceFileReader
    {
        public static String getSourceFileContent(String filename)
        {
            String sourceContent = File.ReadAllText(filename);

            return sourceContent;
        }
    }
}
