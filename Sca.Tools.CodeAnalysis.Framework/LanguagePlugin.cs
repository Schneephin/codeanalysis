using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Sca.Tools.CodeAnalysis.Framework
{
    public class LanguagePlugin
    {
        public String className { get; private set; }
        public Assembly pluginAssembly { get; private set; }

        public LanguagePlugin(String className, Assembly pluginAssembly)
        {
            this.className = className;
            this.pluginAssembly = pluginAssembly;
        }
    }
}
