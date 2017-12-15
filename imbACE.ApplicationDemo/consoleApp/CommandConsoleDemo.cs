// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandConsoleDemo.cs" company="imbVeles" >
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
    /// 
    /// </summary>
    /// <seealso cref="imbACE.Services.console.aceAdvancedConsole{imbACE.ApplicationDemo.consoleApp.CommandConsoleDemoState, imbACE.ApplicationDemo.consoleApp.CommandConsoleDemoWorkspace}" />
    public class CommandConsoleDemo : aceAdvancedConsole<CommandConsoleDemoState, CommandConsoleDemoWorkspace>
    {
        public override string consoleTitle { get { return "Demo Console"; } }

        public CommandConsoleDemo() : base()
        {


        }

        public override aceCommandConsoleIOEncode encode => aceCommandConsoleIOEncode.dos;

        /// <summary>
        /// Gets the workspace.
        /// </summary>
        /// <value>
        /// The workspace.
        /// </value>
        public override CommandConsoleDemoWorkspace workspace {
            get
            {
                if (_workspace == null)
                {
                    _workspace = new CommandConsoleDemoWorkspace(this);
                }
                return _workspace;
            }
        }

        /// <summary>
        /// Customized code to be executed once the console is started
        /// </summary>
        public override void onStartUp()
        {
            base.onStartUp();

            // put here your code 
        }

        protected override void doCustomSpecialCall(aceCommandActiveInput input)
        {
            
        }
    }

}