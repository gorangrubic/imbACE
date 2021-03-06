// --------------------------------------------------------------------------------------------------------------------
// <copyright file="userInterfaceType.cs" company="imbVeles" >
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
using System;
using System.Linq;
using System.Collections.Generic;
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

namespace imbACE.Core.enums.platform
{

    /// <summary>
    /// Defines the type of User Interface platform
    /// </summary>
    public enum userInterfaceType
    {
        /// <summary>
        /// The unknown: not specified
        /// </summary>
        unknown,

        /// <summary>
        /// The terminal: command-prompt kind of interface
        /// </summary>
        terminal,

        /// <summary>
        /// The text blocks TUI: ACE Text User Interface called TextBlocks
        /// </summary>
        textBlocksTUI,

        /// <summary>
        /// The xaml tui: Text User Interface based on open source project: CommandConsole
        /// </summary>
        xamlTUI,

        /// <summary>
        /// The application it self is a local web server that renders UI trough HTML and Web Browser
        /// </summary>
        selfServedWebUI,
        /// <summary>
        /// Web UI based on external web server
        /// </summary>
        externalWebUI,
        /// <summary>
        /// XAML based GUI
        /// </summary>
        xamlGUI,

    }

}