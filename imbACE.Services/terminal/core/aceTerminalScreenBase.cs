// --------------------------------------------------------------------------------------------------------------------
// <copyright file="aceTerminalScreenBase.cs" company="imbVeles" >
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
    using imbACE.Services.platform.core;
    using imbACE.Services.textBlocks;
    using imbACE.Services.textBlocks.depracated;
    using imbACE.Services.textBlocks.input;
    using imbACE.Core.extensions;
    using imbACE.Core.operations;
    using imbACE.Services.platform.interfaces;


    /// <summary>
    /// Osnova mehanizma za renderovanje konzolarnog ekrana
    /// </summary>
    public abstract class aceTerminalScreenBase: aceOperationSetExecutorBase, IRenderReadExecute, ItextLayout, IAceTerminalScreen
    {

        protected aceTerminalScreenBase(String __title)
        {
            title = __title;
        }



        #region --- parentScreen ------- ekran na koji se vraca kada korisnik pritisne back - automatski podesava ovu vrednost
        private IAceTerminalScreen _parentScreen = null;
        /// <summary>
        /// ekran na koji se vraca kada korisnik pritisne back - automatski podesava ovu vrednost
        /// </summary>
        public IAceTerminalScreen parentScreen
        {
            get
            {
                return _parentScreen;
            }
            set
            {
                _parentScreen = value;
                OnPropertyChanged("parentScreen");
            }
        }
        #endregion
        


        #region --- title ------- naslov ekrana

        private String _title = "";
        /// <summary>
        /// naslov ekrana
        /// </summary>
        public String title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        #endregion



        #region --- layout ------- Layout terminalnog ekrana
        private textLayout _layout;
        /// <summary>
        /// Layout terminalnog ekrana
        /// </summary>
        public textLayout layout
        {
            get
            {
                return _layout;
            }
            set
            {
                _layout = value;
                OnPropertyChanged("layout");
            }
        }
        #endregion

        /// <summary>
        /// #0 Izvrsava se prvi put - kada se instancira. Customized sekvenca inicijalizacije
        /// </summary>
        /// <param name="platform"> </param>
        public abstract void init(IPlatform platform);


        

        /// <summary>
        /// Obnavlja dinamicki deo sadrzaja
        /// </summary>
        public abstract void refresh();


        /// <summary>
        /// #2 Očitava ulaz -- reseno na nivou aceTErminalScreenBase
        /// </summary>
        public abstract inputResultCollection read(inputResultCollection __results); 
        

        /// <summary>
        /// #3 Vrsi rad nakon sto je obradjen ulaz
        /// </summary>
        public abstract inputResultCollection execute(inputResultCollection __inputs);

        /// <summary>
        /// #1 Generise sadrzaj
        /// </summary>
        public abstract void render(IPlatform platform, Boolean doClearScreen=true);

        /// <summary>
        /// porukaZaFooter
        /// </summary>
        public string layoutFooterMessage
        {
            get { return layout.layoutFooterMessage; }
            set { layout.layoutFooterMessage = value; }
        }

        /// <summary>
        /// poruka za naslov prozora
        /// </summary>
        public string layoutTitleMessage
        {
            get { return layout.layoutTitleMessage; }
            set { layout.layoutTitleMessage = value; }
        }

        /// <summary>
        /// Poruka za statusni header
        /// </summary>
        public string layoutStatusMessage
        {
            get { return layout.layoutStatusMessage; }
            set { layout.layoutStatusMessage = value; }
        }

       
    }
}