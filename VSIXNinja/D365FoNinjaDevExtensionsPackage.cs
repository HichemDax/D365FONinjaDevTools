//------------------------------------------------------------------------------
// <copyright file="Command1Package.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using D365FONinjaDevExtensions.AddItemsToFolder;
using Microsoft.VisualStudio.Shell;

namespace D365FONinjaDevExtensions
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(D365FoNinjaDevExtensionsPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
         Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad("ADFC4E64-0397-11D1-9F4E-00A0C911004F")]
    public sealed class D365FoNinjaDevExtensionsPackage : Package
    {
        public const string PackageGuidString = "a2d23409-8e73-41d4-a25f-c4ba334eb96b";
    

        protected override void Initialize()
        {
            AddItemCommand.Initialize(this);
            base.Initialize();
        }
      
    }
}