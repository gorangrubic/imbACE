// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandConsoleDemoWorkspace.cs" company="imbVeles" >
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
// Project: imbACE.ApplicationDemo
// Author: Goran Grubic
// ------------------------------------------------------------------------------------------------------------------
// Project web site: http://blog.veles.rs
// GitHub: http://github.com/gorangrubic
// Mendeley profile: http://www.mendeley.com/profiles/goran-grubi2/
// ORCID ID: http://orcid.org/0000-0003-2673-9471
// Email: hardy@veles.rs
// </summary>
// ------------------------------------------------------------------------------------------------------------------

namespace imbACE.ApplicationDemo.consoleApp
{
    using imbACE.Services.console;

    /// <summary>
    /// Workspave for the console
    /// </summary>
    /// <seealso cref="imbACE.Services.console.aceAdvancedConsoleWorkspace" />
    public class CommandConsoleDemoWorkspace : aceAdvancedConsoleWorkspace
    {

        /// <summary>
        /// Initializes the workspace for the console specified
        /// </summary>
        /// <param name="console">The console using this workspace</param>
        public CommandConsoleDemoWorkspace(CommandConsoleDemo console) :base(console)
        {

        }


        /// <summary>
        /// Gets called during workspace construction, the method should initiate any additional subdirectories that console's project uses
        /// </summary>
        /// <remarks>
        /// <example>
        /// Place inside initiation of additional directories, required for your console's project class (inheriting: <see cref="T:imbACE.Services.console.aceAdvancedConsoleStateBase" />)
        /// <code><![CDATA[
        /// folderName = folder.Add(nameof(folderName), "Caption", "Description");
        /// ]]></code></example>
        /// </remarks>
        public override void setAdditionalWorkspaceFolders()
        {
            // place here your additional directories for console's project subdirectory as follows:
            // folderName = folder.Add(nameof(folderName), "Caption", "Description");
        }
    }

}