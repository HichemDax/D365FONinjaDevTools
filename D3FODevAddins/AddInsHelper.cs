// [====================================================================================
//             Class: AddInsHelper
//            Author: joe booth
//              Date: 10/17/2012
//           Website: www.joebooth-consulting.com
//  
// Class Information:
//          Inherits: Object
//  
//  Public Interface:
//  
//        Properties: app (EnvDTE80.DTE2)
//           Methods: MakeEmptySolution(folder:string,SolName:string)
//                    GetVSProjectsFolder ==> string
//                    FindMenuIndex(MainMenu:string,subMenu:string) ==> int
// =====================================================================================]
using System;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Windows.Forms;

public class AddInsHelper
{
    /// <summary>
    /// Application Object
    /// </summary>
    public DTE2 app { get; set; }

    /// <summary>
    /// Create a new, empty solution 
    /// </summary>
    /// <param name="folder">Folder to hold solution</param>
    /// <param name="SolName">Solution name</param>
	public void MakeEmptySolution(string folder,string SolName)
	{
        string FullFolder;
        // Close solution if open
        if (app.Solution.IsOpen)
        {
            app.Solution.Close(true);
        }
        // Get or make the folder where for solutionn
        FullFolder = System.IO.Path.Combine(GetVSProjectsFolder(), folder);
        if (!System.IO.Directory.Exists(FullFolder))
        {
            System.IO.Directory.CreateDirectory(FullFolder);
        }
        string tempFile  = System.IO.Path.Combine(FullFolder, "TempSolution.sln");
        app.Solution.Create(FullFolder,tempFile);
        tempFile = System.IO.Path.Combine(FullFolder, SolName);
        app.Solution.SaveAs(tempFile);
	}

    /// <summary>
    /// Return name of projects folder for Visual Studio
    /// </summary>
    /// <returns></returns>
    public string GetVSProjectsFolder()
    {
        string res = "";
        EnvDTE.Properties theProp =   app.get_Properties("Environment","ProjectsAndSolution");
        res = theProp.Item("ProjectsLocation").Value.ToString();
        return res;
   }
    /// <summary>
    /// Returns index of menu option or 1 if not found
    /// </summary>
    /// <param name="MainMenu">File | Edit |View, etc</param>
    /// <param name="subMenu">Caption within menu</param>
    /// <returns></returns>
    public int FindMenuIndex(string MainMenu,string subMenu)
    {
        int res = 1;
        try
        {
            CommandBar menuBar = ((CommandBars)app.CommandBars)["MenuBar"];
            {
                foreach (CommandBarControl cb in menuBar.Controls)
                {
                    if (MainMenu.ToString().ToUpper() == cb.Caption.ToString().ToUpper().Replace("&", ""))
                    {
                        CommandBarPopup toolsPopup = (CommandBarPopup)cb;
                        for (int xx = 1; xx <= toolsPopup.Controls.Count; xx++)
                        {
                            if (toolsPopup.Controls[xx].Caption.ToString().ToUpper().Replace("&", "")
                                     == subMenu.ToUpper())
                            {
                                res = toolsPopup.Controls[xx].Index;
                                break;
                            }
                        }

                        break;
                    }
                }
            }
        }
        catch 
        {
        }
        return res;
    }

}
