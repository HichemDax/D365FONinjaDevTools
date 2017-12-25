using System.Linq;
using System.Text.RegularExpressions;
using D365FONinjaDevTools.Parameters;
using Microsoft.Dynamics.AX.Metadata.Service;
using Microsoft.Dynamics.Framework.Tools.Extensibility;
using Microsoft.Dynamics.Framework.Tools.Labels;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.ProjectSupport;

namespace D365FONinjaDevTools.Kernel
{
    public static class AddinsUtility
    {
        public static ProjectNode Project;
        public static IMetaModelProviders MetaModelProviders;
        public static IMetaModelService MetaModelService;

        public static string Convert(this string name)
        {
            Project = (ProjectNode)LocalUtils.GetActiveProject();
            ProjectParameters.Contruct();
            MetaModelProviders = ServiceLocator.GetService(typeof(IMetaModelProviders)) as IMetaModelProviders;
            MetaModelService = MetaModelProviders.CurrentMetaModelService;

            var extension = ProjectParameters.Instance.Extension;

            var englishLabelFile =
                MetaModelService.GetLabelFileNames()
                    .FirstOrDefault(
                        lableFileName => lableFileName.StartsWith(extension) && lableFileName.Contains("en-US"));

            var lableFile = MetaModelService.GetLabelFile(englishLabelFile);
            var labelKey = name.Replace(extension, "");

            var lableTxt = Regex.Replace(labelKey, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1").Trim().ToLower().UppercaseFirst();

            LabelControllerFactory factory = new LabelControllerFactory();
            LabelEditorController labelEditorController = factory.GetOrCreateLabelController(lableFile, LocalUtils.Context);
            
            //CoreUtility.DisplayInfo(labelKey);

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