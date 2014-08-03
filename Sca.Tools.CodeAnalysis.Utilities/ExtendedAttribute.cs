using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sca.Tools.CodeAnalysis.Utilities
{
    public class ExtendedAttribute
    {
        public TreeNode parentNode { get; set; }
        public String attributeName { get; set; }
        public String attributeType { get; set; } //Enum?
        public String attributeInformation { get; set; } //qual/quant? -> obj?
        public String attributeComment { get; set; }

    }
}
