using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Service;
using Microsoft.Dynamics.Framework.Tools;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.Labels;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;

namespace D365FONinjaDevTools.Kernel
{
    /// <summary>
    ///     A utility class for methods and properties used by add-in classes.
    /// </summary>
    public static class LocalUtils
    {

        static LocalUtils()
        {
            var metaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            MetaService = metaModelProviders?.CurrentMetaModelService;
            ProjectService = ServiceLocator.GetService(typeof(IDynamicsProjectService)) as IDynamicsProjectService;
            ElementService  = ServiceLocator.GetService(typeof(IDisplayElementProvider)) as IDisplayElementProvider;
            
        }

        public static IDisplayElementProvider ElementService { get; }
        public static IMetaModelService MetaService { get; }
        public static IDynamicsProjectService ProjectService { get; }

        // DTE interface represents Visual Studio object model and offers access to many related
        // objects and methods.
        // This is not anything specific to Dynamics AX.
        public static DTE2 MyDte => CoreUtility.ServiceProvider.GetService(typeof(DTE)) as DTE2;

       
        public static VSApplicationContext Context => new VSApplicationContext(MyDte.DTE);

        /// <summary>
        ///     Gets the currently selected project in Visual Studio.
        /// </summary>
        /// <returns>The selected project node, if any; otherwise null.</returns>
        public  static VSProjectNode GetActiveProjectNode()
        {
            var projects = MyDte.ActiveSolutionProjects as Array;

            if (projects?.Length > 0)
            {
                Project project = projects.GetValue(0) as Project;
                return project?.Object as VSProjectNode;
            }
            return null;
        }

        /// <summary>
        ///     Gets the currently selected project in Visual Studio.
        /// </summary>
        /// <returns>The selected project node, if any; otherwise null.</returns>
        public static Project GetActiveProject()
        {
            var projects = MyDte.ActiveSolutionProjects as Array;

            if (projects?.Length > 0)
            {
                var project = projects.GetValue(0) as Project;
                return project;
            }
            return null;
        }


        public static ModelSaveInfo GetModel()
        {

            var modelInfo = LocalUtils.GetActiveProjectNode().GetProjectsModelInfo();

             var model = new ModelSaveInfo
            {
                Id = modelInfo.Id,
                Layer = modelInfo.Layer
            };
            return model;
        }

        public static string ToCamelCase(this string txt)
        {
            return char.ToLowerInvariant(txt[0]) + txt.Substring(1);
        }

    }
}