using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sca.Tools.CodeAnalysis.Utilities;

namespace Sca.Tools.CodeAnalysis.Visualizer
{
    public class ComboBoxItem
    {
        internal TreeNode node;

        public ComboBoxItem(TreeNode node)
        {
            this.node = node;
        }

        public override string ToString()
        {
            return this.node.nodeContent;
        }
    }
}
