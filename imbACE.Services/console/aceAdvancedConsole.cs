// --------------------------------------------------------------------------------------------------------------------
// <copyright file="aceAdvancedConsole.cs" company="imbVeles" >
//
// Copyright (C) 2017 imbVeles
//
// This program is free software: you can redistribute it and/or modify
// it under the +terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/. 
// </copyright>
// <summary>
// Project: imbACE.Services
// Author: Goran Grubic
// ------------------------------------------------------------------------------------------------------------------
// Project web site: http://blog.veles.rs
// GitHub: http://github.com/gorangrubic
// Mendeley profile: http://www.mendeley.com/profiles/goran-grubi2/
// ORCID ID: http://orcid.org/0000-0003-2673-9471
// Email: hardy@veles.rs
// </summary>
// ------------------------------------------------------------------------------------------------------------------
using imbACE.Core;
using imbSCI.Core;
using imbSCI.Core.attributes;
using imbSCI.Core.enums;
using imbSCI.Core.extensions.text;
using imbSCI.Core.extensions.typeworks;
using imbSCI.Core.interfaces;
using imbSCI.Data;
using imbSCI.Data.collection;
using imbSCI.Data.data;
using imbSCI.Data.interfaces;
using imbSCI.DataComplex;
using imbSCI.Reporting;
using imbSCI.Reporting.enums;
using imbSCI.Reporting.interfaces;

namespace imbACE.Services.console
{
    using System;
    using System.Linq;
    using imbACE.Core.commands;
    using imbACE.Core.commands.menu;
    using imbACE.Core.commands.tree;
    using imbACE.Core.extensions.io;
    using imbACE.Core.operations;
    using imbACE.Services.terminal;
    using imbSCI.Core.data;
    using imbSCI.Core.extensions.io;
    using imbSCI.Core.reporting.render.builders;
    using imbSCI.Data.enums;
    using imbACE.Core.application;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    //using System.Windows.Forms;
    //using imbACE.Core.reporting;
    //using imbACE.Core.reporting.render.builders;


    /// <summary>
    /// Advanced command console environment with its workspace and other stuff
    /// </summary>
    /// <seealso cref="aceCommandConsole" />
    public abstract class aceAdvancedConsole<TState, TWorkspace> : aceCommandConsole, IAceAdvancedConsole where TState : aceAdvancedConsoleStateBase, new()
        where TWorkspace : aceAdvancedConsoleWorkspace
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="aceAdvancedConsole{TState, TWorkspace}"/> class.
        /// </summary>
        protected aceAdvancedConsole()
        {
            state.Poke();
            workspace.Poke();
        }

        /// <summary>
        /// Logs the specified message, using the other color if required
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="otherColor">if set to <c>true</c> [other color].</param>
        public void log(String message, Boolean otherColor = false)
        {
            if (otherColor) output.consoleAltColorToggle();
            output.log(message);
            if (otherColor) output.consoleAltColorToggle();
        }



        /// <summary>
        /// Gets a value indicating whether this instance is ready.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ready; otherwise, <c>false</c>.
        /// </value>
        public Boolean isReady
        {
            get
            {
                if (_workspace == null) return false;
                if (_state == null) return false;

                return true;
            }
        }


        

        protected TWorkspace _workspace; // = "";
                                       
        public abstract TWorkspace workspace { get; }
       


        protected TState _state; // = "";
                                    /// <summary>
                                    /// Bindable property
                                    /// </summary>
        public virtual TState state
        {
            get {
                if (_state == null)
                {
                    
                    _state = new TState();
                    _state.Load();

                }
                return _state;
            }
        }


        private settingsEntriesForObject _stateInfo; // = "";
                                    /// <summary>
                                    /// Bindable property
                                    /// </summary>
        public settingsEntriesForObject stateInfo
        {
            get {
                if (_stateInfo == null)
                {
                    _stateInfo = new settingsEntriesForObject(state);
                }

                return _stateInfo;
            }
        }



        private Object _SelectedObject;
        /// <summary>
        /// 
        /// </summary>
        public Object SelectedObject
        {
            get {
                if (_SelectedObject == null) _SelectedObject = state;
                return _SelectedObject;
            }
            set { _SelectedObject = value; }
        }



        public Object GetScope()
        {
            if (state.scopePath.isNullOrEmpty()) return state;

            if (state.scopePath.Length>2)
            {
                return state.imbGetPropertySafe(state.scopePath, state, ".");
            }
            return state;
        }


        /// <summary>
        /// </summary>
        public override string linePrefix
        {
            get
            {
                return state.scopePath.add(base.linePrefix, " ");
                
            }
        }


        aceAdvancedConsoleStateBase IAceAdvancedConsole.state => state;
        

        aceAdvancedConsoleWorkspace IAceAdvancedConsole.workspace => workspace;





        [Display(GroupName = "help", Name = "ExportHelp", ShortName = "", Description = "Exports help file into current state project folder")]
        [aceMenuItem(aceMenuItemAttributeRole.ExpandedHelp, "Writes a txt file with content equeal to the result of help command")]
        /// <summary>Exports help file into current state project folder</summary> 
        /// <remarks><para>Writes a txt file with content equeal to the result of help command</para></remarks>
        /// <param name="filename">help.txt</param>
        /// <param name="open">true</param>
        /// <seealso cref="aceOperationSetExecutorBase"/>
        public void aceOperation_helpExportHelp(
            [Description("help.txt")] String filename = "word",
            [Description("true")] Boolean open = true)
        {
            builderForMarkdown mdBuilder = new builderForMarkdown();

            commandSetTree.ReportCommandTree(mdBuilder, false, 0, aceCommandConsoleHelpOptions.full);
            helpContent = mdBuilder.GetContent();

            String p = workspace.folder.pathFor(filename);
            if (p.saveToFile(helpContent))
            {
                response.log("Help file saved to: " + p);
            }
            if (open) externalTool.notepadpp.run(p);
        }







        [aceMenuItem(aceMenuItemAttributeRole.Key, "canonicalForm")]
        [aceMenuItem(aceMenuItemAttributeRole.aliasNames, "scriptProcess")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "ScriptProcess")]
        [aceMenuItem(aceMenuItemAttributeRole.Category, "run")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Processing script into selected form")]
        [aceMenuItem(aceMenuItemAttributeRole.ExpandedHelp, "It will load the specified script and process it into selected format")]
        [aceMenuItem(aceMenuItemAttributeRole.DisabledRemarks, "(disabled)")]
        // [aceMenuItem(aceMenuItemAttributeRole.ConfirmMessage, "Are you sure?")]  // [aceMenuItem(aceMenuItemAttributeRole.EnabledRemarks, "")]
        // [aceMenuItem(aceMenuItemAttributeRole.externalHelpFilename, "aceOperation_runScriptProcess.md")]
        [aceMenuItem(aceMenuItemAttributeRole.CmdParamList, "script=\"*\":String;format=explicitFormat:commandLineFormat;askForFormat=true:Boolean;")]
        /// <summary>
        /// Method of menu option ScriptProcess (key:canonicalForm). <args> expects param: script:String;format:commandLineFormat;askForFormat:Boolean;
        /// </summary>
        /// <param name="args"><seealso cref="aceOperationArgs"/> requered parameters:  script:String;format:commandLineFormat;askForFormat:Boolean;</param>
        /// <remarks>
        /// <para>It will load the specified script and process it into selected format</para>
        /// <para>Processing script into selected form</para>
        /// <para>Message if item disabled: (disabled)</para>
        /// </remarks>
        /// <seealso cref="aceOperationSetExecutorBase"/>
        public void aceOperation_runScriptProcess(aceOperationArgs args)
        {
            

            String script = args.Get<String>("script");
            commandLineFormat format = args.Get<commandLineFormat>("format");
            Boolean askForFormat = args.Get<Boolean>("askForFormat");

            

            if (script == "*")
            {
                var list = workspace.getScriptList();
                script = aceTerminalInput.askForOption("Select script file to load", list.First(), list, null).toStringSafe();
            }

            if (askForFormat)
            {
                format = (commandLineFormat)aceTerminalInput.askForOption("Choose command format to be applied on the specified ACE script.", format);
            }

            var ace_script = workspace.loadScript(script);

            String scriptName = aceTerminalInput.askForString("Please enter filename for reformated script:", ace_script.info.Name);

            var newScript = ace_script.GetScriptInForm(this, format, workspace.folder[aceCCFolders.scripts].pathFor(scriptName));
            


        }





        [aceMenuItem(aceMenuItemAttributeRole.Key, "reset")]
        [aceMenuItem(aceMenuItemAttributeRole.aliasNames, "reset")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "ResetState")]
        [aceMenuItem(aceMenuItemAttributeRole.Category, "run")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Creating blank work folder (job/project) after saving the curent")]
        [aceMenuItem(aceMenuItemAttributeRole.ExpandedHelp, "It will ask you for new job/state name and save the current state before cleaning the memory. If _autorename_ is true it will make new name for new state if the specified one is already taken.")]
        [aceMenuItem(aceMenuItemAttributeRole.DisabledRemarks, "(disabled)")]
        [aceMenuItem(aceMenuItemAttributeRole.ConfirmMessage, "Are you sure?")]  // [aceMenuItem(aceMenuItemAttributeRole.EnabledRemarks, "")]
        // [aceMenuItem(aceMenuItemAttributeRole.externalHelpFilename, "aceOperation_runResetCurrentState.md")]
        [aceMenuItem(aceMenuItemAttributeRole.CmdParamList, "name=\"analytic\":String;")]
        /// <summary>
        /// Method of menu option ResetCurrentState (key:r). <args> expects param: name=\"analytic\":String;savecurrent=true:Boolean;autorename=true:Boolean;
        /// </summary>
        /// <param name="args"><seealso cref="aceOperationArgs"/> requered parameters: :type;paramb:type;</param>
        /// <remarks>
        /// <para>It will ask you for new job/state name and save the current state before cleaning the memory</para>
        /// <para>Creating blank work state (job/project) after saving the curent</para>
        /// <para>Message if item disabled: (disabled)</para>
        /// </remarks>
        /// <seealso cref="aceOperationSetExecutorBase"/>
        public void aceOperation_runResetCurrentState(aceOperationArgs args)
        {
            String name = args.Get<String>("name");

            name = workspace.getNewProjectName(name);

            state.currentProjectName = name;
            state.Save();

            workspace.deployWorkspace();
            log("New workspace ready [" + workspace.projectRootPath + "].");
        }



        [aceMenuItem(aceMenuItemAttributeRole.Key, "sedit")]
        [aceMenuItem(aceMenuItemAttributeRole.aliasNames, "script_edit")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "ScriptEdit")]
        [aceMenuItem(aceMenuItemAttributeRole.Category, "edit")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Opens specified script file for editing in default external tool")]
        [aceMenuItem(aceMenuItemAttributeRole.ExpandedHelp, "If the script file don't exist it will create new one")]
        [aceMenuItem(aceMenuItemAttributeRole.DisabledRemarks, "(disabled)")]
        [aceMenuItem(aceMenuItemAttributeRole.CmdParamList, "filename=\"newscript\":String;")]
        /// <summary>
        /// Aces the operation edit script edit.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void aceOperation_editScriptEdit(aceOperationArgs args)
        {
            String filename = args.Get<String>("filename");
            var fi = workspace.getScriptFileInfo(filename);

            externalToolExtensions.run(externalTool.notepadpp, fi.FullName);
            
        }



        [aceMenuItem(aceMenuItemAttributeRole.Key, "exe")]
        [aceMenuItem(aceMenuItemAttributeRole.aliasNames, "script")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "ExecuteScript")]
        [aceMenuItem(aceMenuItemAttributeRole.Category, "run")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Basic automation facility: reads lines from the script file and executes it as it was typed by the console user. If filename parameter is * it will ask user to select script to load.")]
        [aceMenuItem(aceMenuItemAttributeRole.ExpandedHelp, "It opens the specified script (.ace) file from the scripts folder and performs commands from the script")]
        [aceMenuItem(aceMenuItemAttributeRole.DisabledRemarks, "(disabled)")]
        // [aceMenuItem(aceMenuItemAttributeRole.ConfirmMessage, "Are you sure?")]  // [aceMenuItem(aceMenuItemAttributeRole.EnabledRemarks, "")]
        // [aceMenuItem(aceMenuItemAttributeRole.externalHelpFilename, "aceOperation_runExecuteScript.md")]
        [aceMenuItem(aceMenuItemAttributeRole.CmdParamList, "filename=\"*\":String;repeat=1:Int32;delay=10:Int32;")]
        /// <summary>
        /// Method of menu option ExecuteScript (key:exe). <args> expects param: filename=\"autoexec.txt\":String;repeat=1:Int32;
        /// </summary>
        /// <param name="args"><seealso cref="aceOperationArgs"/> requered parameters: filename=\"autoexec.txt\":String;repeat=1:Int32;</param>
        /// <remarks>
        /// <para>It opens the specified script (.txt) file from the scripts folder and performs commands from the script</para>
        /// <para>Basic automation facility: reads lines from the script file and executes it as it was typed by the console user</para>
        /// <para>Message if item disabled: (disabled)</para>
        /// </remarks>
        /// <seealso cref="aceOperationSetExecutorBase"/>
        public void aceOperation_runExecuteScript(aceOperationArgs args)
        {
            String filename = args.Get<String>("filename");
            Int32 delay = args.Get<Int32>("delay");
            Int32 repeat = args.Get<Int32>("repeat");

            if (filename == "*")
            {
                var list = workspace.getScriptList();
                filename = aceTerminalInput.askForOption("Select script file to load", list.First(), list, null).toStringSafe();
            }

            var script = workspace.loadScript(filename);

            log("Script [" + filename + "] with [" + script.Count() + "] will execute [" + repeat + "] time/s.");

            while (repeat > 0)
            {
                executeScript(script, delay);
                repeat--;
            }

            log("Script [" + filename + "] executed.");
        }


        [aceMenuItem(aceMenuItemAttributeRole.Key, "templateScript")]
        [aceMenuItem(aceMenuItemAttributeRole.aliasNames, "templateScript")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "TemplateScript")]
        [aceMenuItem(aceMenuItemAttributeRole.Category, "run")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Uses template script file to dynamically create customized execution script")]
        [aceMenuItem(aceMenuItemAttributeRole.ExpandedHelp, "It loads specified template script file and applies provided parameters to the {n} template placeholders")]
        [aceMenuItem(aceMenuItemAttributeRole.DisabledRemarks, "(disabled)")]
        // [aceMenuItem(aceMenuItemAttributeRole.ConfirmMessage, "Are you sure?")]  // [aceMenuItem(aceMenuItemAttributeRole.EnabledRemarks, "")]
        // [aceMenuItem(aceMenuItemAttributeRole.externalHelpFilename, "aceOperation_runTemplateScript.md")]
        [aceMenuItem(aceMenuItemAttributeRole.CmdParamList, "templateName=\"performanceTestTemplate\":String;parameters=\"2,SM-LSD,1,preloadLexicon\":String;saveScript=true:Boolean;")]
        /// <summary>
        /// Method of menu option TemplateScript (key:templateScript). <args> expects param: templateName:String;parameteres:String;saveScript:Boolean;
        /// </summary>
        /// <param name="args"><seealso cref="aceOperationArgs"/> requered parameters:  templateName:String;parameteres:String;saveScript:Boolean;</param>
        /// <remarks>
        /// <para>It loads specified template script file and applies provided parameters to the {n} template placeholders</para>
        /// <para>Uses template script file to dynamically create customized execution script</para>
        /// <para>Message if item disabled: (disabled)</para>
        /// </remarks>
        /// <seealso cref="aceOperationSetExecutorBase"/>
        public void aceOperation_runTemplateScript(aceOperationArgs args)
        {
            
            String templateName = args.Get<String>("templateName");
            String parameters = args.Get<String>("parameters");
            Boolean saveScript = args.Get<Boolean>("saveScript");

            String[] pars = parameters.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            aceConsoleScript script = workspace.loadScript(templateName);
            aceConsoleScript scriptInstance = script.DeployTemplate(pars);

            if (saveScript) scriptInstance.Save();

            if (scriptInstance.isReady)
            {
                executeScript(scriptInstance);
            } else
            {
                output.log("Script instance [" + scriptInstance.info.Name + "] creation from template script [" + script.info.Name + "] failed to construct. Check number of parameters!");
            }


        }


    }

}