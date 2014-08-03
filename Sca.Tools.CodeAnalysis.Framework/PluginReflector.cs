using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Sca.Tools.CodeAnalysis.Utilities;

namespace Sca.Tools.CodeAnalysis.Framework
{
    class PluginReflector
    {

        public static List<LanguagePlugin> addLanguagePlugin(String pluginFileName)
        {
            List<LanguagePlugin> plugIns = new List<LanguagePlugin>();

            try
            {
                Assembly currentAssembly = Assembly.LoadFrom(pluginFileName);
                foreach(Type pluginClass in currentAssembly.GetTypes())
                {
                    if (pluginClass.GetInterface("Sca.Tools.CodeAnalysis.Utilities.iLanguagePlugIn") != null)
                    {
                        plugIns.Add(new LanguagePlugin(pluginClass.FullName, currentAssembly));   
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("An error occured with message: " + Environment.NewLine + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            if (plugIns.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No valid language plug-in was found in the provided dll.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return plugIns;
        }

        public static TreeNode analyzeSourceCode(LanguagePlugin plugin, String sourceFolder)
        {
            try
            {
                Type pluginClass = plugin.pluginAssembly.GetType(plugin.className);
                object ipluginObject = Activator.CreateInstance(pluginClass);
                object[] ipluginParameters = new object[] { sourceFolder };

                // http://msdn.microsoft.com/en-us/library/system.reflection.bindingflags.aspx
                object returnObject = pluginClass.InvokeMember("analyseCode", BindingFlags.Default | BindingFlags.InvokeMethod, null, ipluginObject, ipluginParameters);

                if (returnObject == null)
                {
                    System.Windows.Forms.MessageBox.Show("The source code could not be analysed. Check if the plug-in is fitting or if there are errors in the provided source code.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                return (TreeNode)returnObject;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("An error occured with message: " + Environment.NewLine + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return null;
        }
    }
}
