﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="acePluginDeployer.cs" company="imbVeles" >
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
using imbACE.Core.plugins.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbACE.Core.plugins.deployer
{
    /// <summary>
    /// Deployer is applicative plugin-registar, focused on plugins inheriting the specified generic type parameter <c>{T}</c>.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public abstract class acePluginDeployer<T> where T:IAcePluginBase
    {
        public Type baseType { get; protected set; }

        public acePluginDeployer()
        {
            baseType = typeof(T);
        }
    }
}
