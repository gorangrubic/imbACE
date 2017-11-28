// --------------------------------------------------------------------------------------------------------------------
// <copyright file="aceCommandLineCommonOptionsExecutor.cs" company="imbVeles" >
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
// Project: imbACE.Core
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

namespace imbACE.Core.operations
{

    /*
    [aceMenuItem(aceMenuItemAttributeRole.Category, "Startup")]
    [aceMenuItem(aceMenuItemAttributeRole.Description, "Startup operations accessable with command-line arguments")]
    public class aceCommandLineCommonOptionsExecutor : aceOperationSetExecutorBase
    {
        [aceMenuItem(aceMenuItemAttributeRole.Key, "?")]
        [aceMenuItem(aceMenuItemAttributeRole.aliasNames, "info;options")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "Help")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Basic info and complete list of commandline arguments / options.")]
        public void aceOperation_help(aceOperationArgs args)
        {

        }

        [aceMenuItem(aceMenuItemAttributeRole.Key, "A")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "About")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Introduction on this software and usage guidelines.")]
        public void aceOperation_about(aceOperationArgs args)
        {
        }

        [aceMenuItem(aceMenuItemAttributeRole.Key, "C")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "(Re)Configure")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "The program starts in re-configuration mode: allows review and modification of each parameter in software settings.")]
        public void aceOperation_configure(aceOperationArgs args)
        {
        }

        [aceMenuItem(aceMenuItemAttributeRole.Key, "S")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "Silent mode")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Runs the program in the silent mode - no output displayed, all windows minimized.")]
        public void aceOperation_silent(aceOperationArgs args)
        {
        }

        [aceMenuItem(aceMenuItemAttributeRole.Key, "H")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "Hidden mode")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Runs the program in super silent mode - do window, no messages - only process is visible in OS running process list.")]
        public void aceOperation_hidden(aceOperationArgs args)
        {
        }

        [aceMenuItem(aceMenuItemAttributeRole.Key, "D")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "Dispose logs and temps")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Before start of the program it will delete all temporary files and clean content of existing log file")]
        public void aceOperation_dispose(aceOperationArgs args)
        {
        }

        [aceMenuItem(aceMenuItemAttributeRole.Key, "B")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "Backup")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Makes a zipped copy of active log file, program settings, crash and other data files.")]
        [aceMenuItem(aceMenuItemAttributeRole.CmdParamList, "zip_filename:String;keep_old_limit:Int32")]
        public void aceOperation_backup(aceOperationArgs args)
        {

        }

        [aceMenuItem(aceMenuItemAttributeRole.Key, "K")]
        [aceMenuItem(aceMenuItemAttributeRole.aliasNames, "terminate;killOther")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "Kill")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Kills all instances of the program that (maybe) runs as the background process - but keep it self active. Useful when working in Hidden mode.")]
        [aceMenuItem(aceMenuItemAttributeRole.CmdParamList, "zip_filename:String;keep_old_limit:Int32")]
        public void aceOperation_kill(aceOperationArgs args)
        {

        }

        [aceMenuItem(aceMenuItemAttributeRole.Key, "Q")]
        [aceMenuItem(aceMenuItemAttributeRole.aliasNames, "quitAll;quitAndClose;")]
        [aceMenuItem(aceMenuItemAttributeRole.DisplayName, "Quit")]
        [aceMenuItem(aceMenuItemAttributeRole.Description, "Kills all instances of the program that (maybe) run as the background process - even it self. Useful when working in Hidden mode.")]
        [aceMenuItem(aceMenuItemAttributeRole.CmdParamList, "zip_filename:String;keep_old_limit:Int32")]
        public void aceOperation_quit(aceOperationArgs args)
        {

        }

    
    }
    */
}