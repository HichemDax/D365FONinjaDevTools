using EnvDTE;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XppDevs.AddinsDemo
{
    /// <summary>
    /// A utility class for methods and properties used by add-in classes.
    /// </summary>
    public static class LocalUtils
    {
        // DTE interface represents Visual Studio object model and offers access to many related
        // objects and methods.
        // This is not anything specific to Dynamics AX.
        internal static DTE DTE => CoreUtility.ServiceProvider.GetService(typeof(DTE)) as DTE;
        

        /// <summary>
        /// Gets the currently selected project in Visual Studio.
        /// </summary>
        /// <returns>The selected project node, if any; otherwise null.</returns>
        internal static VSProjectNode GetActiveProject()
        {
            Array projects = LocalUtils.DTE.ActiveSolutionProjects as Array;

            if (projects?.Length > 0)
            {
                Project project = projects.GetValue(0) as Project;
                return project?.Object as VSProjectNode;
            }
            
            return null;
        }

        public static string toCamelCase(string txt)
        {
            return Char.ToLowerInvariant(txt[0]) + txt.Substring(1);
        }
    }
}
