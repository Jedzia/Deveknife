// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizardImplementation.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 13:58</date>
// --------------------------------------------------------------------------------------------------------------------

namespace CustomWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using EnvDTE;

    using Microsoft.VisualStudio.TemplateWizard;

    /*
     * Register with
     * E:\Projects\CSharp\Deveknife\Template_Wizard\bin\Debug>
     *   gacutil.exe /i Accelknide.TemplateWizard1.dll
     * Unregister with
     *   gacutil.exe /u Accelknide.TemplateWizard
     * in an elevated VS Tools command prompt.  
     * 
     */

    public static class GlobalData
    {
    }

    public class IWizardImplementation : IWizard
    {
        public static Dictionary<string, string> globalDictionary;

        private string lastprojectname;

        private UserInputForm inputForm;

        /// <summary>
        /// Runs custom wizard logic before opening an item in the template.
        /// </summary>
        /// <param name="projectItem">The project item that will be opened.</param>
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            // This method is called before opening any item that 
            // has the OpenInEditor attribute.
        }

        /// <summary>
        /// Runs custom wizard logic when a project has finished generating.
        /// </summary>
        /// <param name="project">The project that finished generating.</param>
        public void ProjectFinishedGenerating(Project project)
        {
        }

        /// <summary>
        /// Runs custom wizard logic when a project item has finished generating.
        /// </summary>
        /// <param name="projectItem">The project item that finished generating.</param>
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            // This method is only called for item templates,
            // not for project templates.
        }

        /// <summary>
        /// Runs custom wizard logic when the wizard has completed all tasks.
        /// </summary>
        public void RunFinished()
        {
            // This method is called after the project is created.
        }

        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        /// <param name="automationObject">The automation object being used by the template wizard.</param>
        /// <param name="replacementsDictionary">The list of standard parameters to be replaced.</param>
        /// <param name="runKind">A <see cref="T:Microsoft.VisualStudio.TemplateWizard.WizardRunKind" /> indicating the type of wizard run.</param>
        /// <param name="customParams">The custom parameters with which to perform parameter replacement in the project.</param>
        public void RunStarted(
            object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams)
        {
            globalDictionary = new Dictionary<string, string>();

            try
            {
                // Display a form to the user. The form collects 
                // input for the custom message.
                this.inputForm = new UserInputForm();
                try
                {
                    var projectName = replacementsDictionary["$safeprojectname$"];
                    var splits = projectName.Split('.');
                    var lastOrDefault = splits.LastOrDefault();
                    if(!string.IsNullOrEmpty(lastOrDefault))
                    {
                        var lastSplit = lastOrDefault.Trim();
                        this.inputForm.LastProjectNameText = lastSplit;
                    }
                }
                catch(Exception)
                {
                }

                this.inputForm.TextBox2Text = replacementsDictionary.Count.ToString();
                this.inputForm.ShowDialog();

                this.lastprojectname = this.inputForm.GetLastProjectName();

                // Add custom parameters.
                //var custommessage = "$custommessage$";
                const string TemplateLastprojectname = "$lastprojectname$";
                replacementsDictionary.Add(TemplateLastprojectname, this.lastprojectname);
                globalDictionary.Add(TemplateLastprojectname, this.lastprojectname);

                // replacementsDictionary.Add("$custommessage$",
                // "ashraful");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Indicates whether the specified project item should be added to the project.
        /// </summary>
        /// <param name="filePath">The path to the project item.</param>
        /// <returns>true if the project item should be added to the project; otherwise, false.</returns>
        public bool ShouldAddProjectItem(string filePath)
        {
            // This method is only called for item templates,
            // not for project templates.
            return true;
        }
    }
}