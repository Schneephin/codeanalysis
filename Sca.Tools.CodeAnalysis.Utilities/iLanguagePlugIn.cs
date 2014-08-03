using System;
using System.Collections.Generic;

namespace Sca.Tools.CodeAnalysis.Utilities
{
    public interface iLanguagePlugIn
    {
        TreeNode analyseCode(String folder);

    }
}
