using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using D365FONinjaDevTools.Parameters;
using EnvDTE;
using EnvDTE80;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Service;
using Microsoft.Dynamics.Framework.Tools;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.Labels;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.ProjectSupport;
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
            
            var metaModelProviders = CoreUtility.ServiceProvider.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            MetaService = metaModelProviders?.CurrentMetaModelService;
            
            //ProjectService = CoreUtility.ServiceProvider.GetService(typeof(IDynamicsProjectService)) as IDynamicsProjectService;
            ElementService  = CoreUtility.ServiceProvider.GetService(typeof(IDisplayElementProvider)) as IDisplayElementProvider;

            
        }

     

        public static IDisplayElementProvider ElementService { get; }
        public static IMetaModelService MetaService { get; }
        //public static IDynamicsProjectService ProjectService { get; }

        // DTE interface represents Visual Studio object model and offers access to many related
        // objects and methods.
        // This is not anything specific to Dynamics AX.
        public static DTE2 MyDte => CoreUtility.ServiceProvider.GetService(typeof(DTE)) as DTE2;

       
        public static VSApplicationContext Context => new VSApplicationContext(MyDte.DTE);

        /// <summary>
        ///     Gets the currently selected project in Visual Studio.
        /// </summary>
        /// <returns>The selected project node, if any; otherwise null.</returns>
        public static VSProjectNode GetActiveProjectNode()
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

        public static ProjectNode Project;
        public static IMetaModelProviders MetaModelProviders;
        public static IMetaModelService MetaModelService;

        public static string Convert(this string name)
        {
            Project = GetActiveProjectNode();
            ProjectParameters.Contruct();

            MetaModelProviders = CoreUtility.ServiceProvider.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            if (MetaModelProviders != null)
                MetaModelService = MetaModelProviders.CurrentMetaModelService;

            var extension = ProjectParameters.Instance.Extension;
            var defaultLablesFileName = ProjectParameters.Instance.DefaultLabelsFileName;

            if (string.IsNullOrEmpty(defaultLablesFileName))
                throw new System.Exception(
                    "Label file name not specified in the Settings. Dynamics 365 > Addins > Ninja DevTools Settings");

            var lableFile = MetaModelService.GetLabelFile(defaultLablesFileName);
            if (lableFile == null)
                throw new Exception("File name not found");


            var labelKey = name.Replace(extension, "");
            var lableTxt = Regex.Replace(labelKey, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1").Trim().ToLower().UppercaseFirst();

            LabelControllerFactory factory = new LabelControllerFactory();
            LabelEditorController labelEditorController = factory.GetOrCreateLabelController(lableFile, LocalUtils.Context);

            if (!labelEditorController.Exists(labelKey))
            {
                labelEditorController.Insert(labelKey, lableTxt, string.Empty);
                labelEditorController.Save();
            }

            return $"@{extension}Labels:{labelKey}";
        }

        private static string UppercaseFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

    }
}