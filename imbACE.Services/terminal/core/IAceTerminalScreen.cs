﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAceTerminalScreen.cs" company="imbVeles" >
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
namespace imbACE.Services.terminal.core
{
    using System; 
 using imbACE.Core.core.exceptions;
    using imbACE.Core.operations;
    using imbACE.Services.platform.core;
    using imbACE.Services.platform.interfaces;
    using imbACE.Services.textBlocks;
    using imbACE.Services.textBlocks.input;

    /// <summary>
    /// Opsti interfejs prema terminalnom ekranu
    /// </summary>
    public interface IAceTerminalScreen
    {
        /// <summary>
        /// naslov ekrana
        /// </summary>
        String title { get; set; }

        /// <summary>
        /// Layout terminalnog ekrana
        /// </summary>
        textLayout layout { get; set; }

        /// <summary>
        /// porukaZaFooter
        /// </summary>
        string layoutFooterMessage { get; set; }

        /// <summary>
        /// poruka za naslov prozora
        /// </summary>
        string layoutTitleMessage { get; set; }

        /// <summary>
        /// Poruka za statusni header
        /// </summary>
        string layoutStatusMessage { get; set; }

        /// <summary>
        /// ekran na koji se vraca kada korisnik pritisne back - automatski podesava ovu vrednost
        /// </summary>
        IAceTerminalScreen parentScreen { get; set; }

        /// <summary>
        /// Obnavlja dinamicki deo sadrzaja
        /// </summary>
        void refresh();

        /// <summary>
        /// #2 Očitava ulaz -- reseno na nivou aceTErminalScreenBase
        /// </summary>
        inputResultCollection read(inputResultCollection __results);

        /// <summary>
        /// #3 Vrsi rad nakon sto je obradjen ulaz
        /// </summary>
        inputResultCollection execute(inputResultCollection __inputs);

        /// <summary>
        /// #1 Generise sadrzaj
        /// </summary>
        void render(IPlatform platform, Boolean doClearScreen=true);
    }
}