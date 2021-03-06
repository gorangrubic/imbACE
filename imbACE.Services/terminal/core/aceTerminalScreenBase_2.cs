// --------------------------------------------------------------------------------------------------------------------
// <copyright file="aceTerminalScreenBase_2.cs" company="imbVeles" >
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
 using imbACE.Core.core.exceptions;
using System.Linq;
using System.Collections.Generic;
namespace imbACE.Services.terminal.core
{
    using imbACE.Services.platform.core;
    using imbACE.Services.textBlocks;
    using imbACE.Services.textBlocks.depracated;
    using imbACE.Services.textBlocks.input;
    using imbACE.Core.extensions;
    using imbACE.Core.operations;
    using imbACE.Services.application;
    using imbACE.Services.platform.interfaces;
    using imbSCI.Data;


    /// <summary>
    /// Base class for an Terminal Application screen
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class aceTerminalScreenBase<T> : aceTerminalScreenBase where T:aceTerminalApplication
    {
        protected aceTerminalScreenBase(T __app, String __title)
            : base(__title)
        {
            application = __app;
            // konstruise zajednicki layout
            layout = application.initCommonLayout();
        }

        protected T application;

        /// <summary>
        /// #2 Očitava ulaz -- reseno na nivou aceTErminalScreenBase
        /// </summary>
        public override void render(IPlatform platform, Boolean doCleanScreen=true)
        {
            String __title = application.appAboutInfo.applicationName.add(" ", "").add(application.appAboutInfo.applicationVersion, "v").add(""); //application.applicationName.add(application.applicationVersion + " v");
            //application.headerLine.setData("DEL", __title, title);
            //application.headerLine.setData("DEL", __title, title);

            __title = __title.add(title, " : ");
            platform.title(__title);
            
            

            layout.render(platform);
        }
    }

}