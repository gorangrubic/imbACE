// --------------------------------------------------------------------------------------------------------------------
// <copyright file="aceConsolePluginBase.cs" company="imbVeles" >
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
    using System.Collections.Generic;
    using imbACE.Core.commands.menu.core;
    using imbACE.Core.core;
    using imbACE.Core.operations;
    using imbSCI.Core.reporting;
    using imbSCI.Core.reporting.render;

    /// <summary>
    /// Console plugin -- use it as public property of a <see cref="aceCommandConsole"/> class to enable command execution. 
    /// </summary>
    /// <remarks>
    /// Methods from a plugin should be called with proper command prefix pointing to property (of <see cref="aceConsolePluginBase"/> type) name. 
    /// Like: <c>printPlugin.print</c> where <c>printPlugin</c> is property within a aceCommandConsole object and <c>print</c> is method of the plugin.
    /// </remarks>
    /// <seealso cref="aceOperationSetExecutorBase" />
    /// <seealso cref="IAceOperationSetExecutor" />
    public abstract class aceConsolePluginBase : aceOperationSetExecutorBase, IAceOperationSetExecutor
    {
        public IAceOperationSetExecutor parent { get; set; }

        public Boolean IsStandalone { get
            {
                return (parent == null);
            }
        }

        public String name { get; set; } = "";
        
        public virtual String consoleTitle
        {
            get
            {
                if (IsStandalone) return name;
                return parent.consoleTitle + "." + name;
            }
        }

        private String _consoleHelp = "";
        public virtual String consoleHelp
        {
            get
            {
                return _consoleHelp;
                
            }
        }

        protected aceMenu _commands;
        aceMenu IAceOperationSetExecutor.commands
        {
            get
            {
                if (_commands == null)
                {
                    _commands = new aceMenu();
                    _commands.setItems(this);
                }
                return _commands;
            }
        }


        protected builderForLog _output;
        private ITextRender _response;

        public ILogBuilder output
        {
            get
            {
                if (IsStandalone) return _output;
                return parent.output;
            }
        }

        public virtual ITextRender response
        {
            get {
                if (_response == null) return output;
                return _response; }
            set { _response = value; }
        }

        ILogBuilder IAceOperationSetExecutor.output => throw new NotImplementedException();

        public List<string> helpHeader => throw new NotImplementedException();

        protected void prepare()
        {
            if (output == null)
            {
                _output = new builderForLog();
                aceLog.consoleControl.setAsOutput(_output, consoleTitle);
            }

        }

        public aceConsolePluginBase(String __name, String __help = "", builderForLog __output =null)
        {
            name = __name;
            _consoleHelp = __help;
            _output = __output;
            prepare();
        }

        /// <summary>
        /// Nested use
        /// </summary>
        /// <param name="__parent">The parent.</param>
        /// <param name="__name">The name.</param>
        public aceConsolePluginBase(IAceOperationSetExecutor __parent, String __name, String __help="")
        {
            name = __name;
            _consoleHelp = __help;
            parent = __parent;
            prepare();
        }

    }

}