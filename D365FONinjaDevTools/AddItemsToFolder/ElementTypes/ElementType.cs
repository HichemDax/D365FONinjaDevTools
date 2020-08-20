using System;
using System.Collections.Generic;
using D365FONinjaDevTools.Kernel;
using EnvDTE;
using Microsoft.Dynamics.AX.Metadata.MetaModel;
using Microsoft.Dynamics.AX.Metadata.Service;
using Microsoft.Dynamics.Framework.Tools.ProjectSystem;

namespace D365FONinjaDevTools.AddItemsToFolder.ElementTypes
{
    public abstract class ElementType
    {
        protected static IMetaModelService MetaService;
        protected static ModelSaveInfo Model;
        protected static VSProjectNode Project;
        protected Type AxElementType;
        protected string ElementName;
        protected VSProjectFolderNode FolderNode;

        public ElementType()
        {
            MetaService = LocalUtils.MetaService;
            
            Model = LocalUtils.GetModel();
            Project = LocalUtils.GetActiveProjectNode();
        }

        public virtual bool IsMatch(ProjectItem projectItem)
        {
            AxElementType = GetElementType();
            FolderNode = projectItem.Object as VSProjectFolderNode;

            if (FolderNode == null)
                throw new Exception("The selected item is not a project folder");

            var isMatch = AotElementCreateEngin.CheckIsMatchWithType(FolderNode.Caption, AxElementType);

            return isMatch;
        }

        public delegate IList<string> ElementNames();
        protected ElementNames GetElementNames;


        private void CheckExists(string elementName)
        {
            GetElementNames = GetElementTypesAction();

            var isExists = GetElementNames().Contains(elementName);

            if (isExists)
                throw new Exception($"Element with the name {elementName} already exists");

            ElementName = elementName;
        }

        protected abstract ElementNames GetElementTypesAction();

        private void AddAotElement()
        {
            Project.AddModelElementsToProject(
                new List<MetadataReference> {new MetadataReference(ElementName, AxElementType)},
                FolderNode.ID, true);
        }


        public void CreateAndAddToProject(string elementName)
        {
            CheckExists(elementName);
            Create();
            AddAotElement();
        }

        public abstract Type GetElementType();
        protected abstract void Create();
    }
}