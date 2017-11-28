// --------------------------------------------------------------------------------------------------------------------
// <copyright file="aceConsoleApplicationBase.cs" company="imbVeles" >
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
using System;
using System.Linq;
using System.Collections.Generic;
namespace imbACE.Services.application
{
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
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading;
    using imbACE.Core.application;
    using imbACE.Core.ataman;
    using imbACE.Core.core;
    using imbACE.Core.events;
    using imbACE.Core.operations;
    using imbACE.Core.plugins;
    using imbSCI.Core.data;
    using imbSCI.Core.files.folders;
    using imbSCI.Core.reporting;


    public abstract class aceConsoleApplication<T>:aceApplicationBase where T:IAceCommandConsole, new()
    {
        private IAceCommandConsole _mainConsole = null;

        protected aceConsoleApplication()
        {
           
        }

        /// <summary>
        /// Main command console of the application
        /// </summary>
        /// <value>
        /// The main console.
        /// </value>
        public IAceCommandConsole mainConsole
        {
            get { return _mainConsole; }
            protected set { _mainConsole = value; }
        }

        protected override void StartUp()
        {
            mainConsole = new T();
            
        }


        protected override bool doApplicationLoop()
        {
            mainConsole.start(this);

            return false;
        }
    }

}