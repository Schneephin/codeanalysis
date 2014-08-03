using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sca.Tools.CodeAnalysis.Visualizer
{

    internal class Filter
    {
        public Mode mode;
        public String filterName;
        public List<String> filterValues;

        public Filter()
        {
            this.filterValues = new List<String>();
        }
    }
}
