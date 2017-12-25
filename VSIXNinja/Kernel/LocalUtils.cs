using System;
using EnvDTE;
using Microsoft.Dynamics.Framework.Tools.Labels;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;

namespace D365FONinjaDevExtensions.Kernel
{
    /// <summary>
    ///     A utility class for methods and properties used by add-in classes.
    /// </summary>
    public static class LocalUtils
    {
        // DTE interface represents Visual Studio object model and offers access to many related
        // objects and methods.
        // This is not anything specific to Dynamics AX.
        public static DTE MyDte => CoreUtility.ServiceProvider.GetService(typeof(DTE)) as DTE;

       
        public static VSApplicationContext Context => new VSApplicationContext(MyDte);

        /// <summary>
        ///     Gets the currently selected project in Visual Studio.
        /// </summary>
        /// <returns>The selected project node, if any; otherwise null.</returns>
        public  static object GetActiveProject()
        {
            var projects = MyDte.ActiveSolutionProjects as Array;

            if (projects?.Length > 0)
            {
                var project = projects.GetValue(0) as Project;

                return project?.Object ;
            }

            
            return null;
        }


        public static string toCamelCase(string txt)
        {
            return char.ToLowerInvariant(txt[0]) + txt.Substring(1);
        }
    }
}