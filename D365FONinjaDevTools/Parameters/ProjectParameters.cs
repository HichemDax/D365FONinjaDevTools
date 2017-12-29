using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Dynamics.Framework.Tools.MetaModel.Core;
using Microsoft.Dynamics.Framework.Tools.ProjectSupport;
using Exception = System.Exception;

namespace D365FONinjaDevTools.Parameters
{
    /// <summary>
    ///     Singleton save all project parameters
    /// </summary>
    [XmlRoot("ProjectParameters")]
    public class ProjectParameters
    {
        private static readonly Lazy<ProjectParameters> Lazy =
            new Lazy<ProjectParameters>(() => new ProjectParameters());

        private ProjectParameters()
        {
        }

        public static string ParamFilePath { get; set; }

        [XmlElement("Extension")]
        public string Extension { get; set; }

        [XmlElement("DefaultLabelFileName")]
        public string DefaultLabelsFileName { get; set; }

        public static ProjectParameters Instance => Lazy.Value;

        public static void Contruct()
        {
            XmlDocument doc = new XmlDocument();
            var xsSubmit = new XmlSerializer(typeof(ProjectParameters));
            ParamFilePath =  Kernel.LocalUtils.GetActiveProjectNode().ProjectFolder + @"\NinjaDevAddinsParam.xml";

            if (File.Exists(ParamFilePath))
            {
                doc.Load(ParamFilePath);

                using (TextReader reader = new StringReader(doc.InnerXml))
                {
                    var projParams = (ProjectParameters) xsSubmit.Deserialize(reader);
                    Instance.Extension = projParams.Extension;
                }
            }
        }


        public void Save()
        {
            var serializableObject = Instance;
            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(serializableObject.GetType());
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(ParamFilePath);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                CoreUtility.HandleExceptionWithErrorMessage(ex);
            }
        }
    }
}