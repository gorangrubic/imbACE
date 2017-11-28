// --------------------------------------------------------------------------------------------------------------------
// <copyright file="aceTerminalApplication.cs" company="imbVeles" >
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

namespace imbACE.Services.application
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using imbACE.Services.platform.interfaces;
    using imbACE.Services.terminal.core;
    using imbACE.Services.textBlocks;
    using imbACE.Services.textBlocks.enums;
    using imbACE.Services.textBlocks.input;
    using imbACE.Services.textBlocks.smart;
    using imbSCI.Core.reporting.zone;

    /// <summary>
    /// Text User Interface application base class
    /// </summary>
    /// <seealso cref="imbACE.Services.application.aceTerminalApplicationBase" />
    /// <seealso cref="imbACE.Services.terminal.core.IRenderReadExecute" />
    public abstract class aceTerminalApplication : aceTerminalApplicationBase, IRenderReadExecute
    {


  
        protected aceTerminalApplication():base()
        {
            
        }

        protected override void StartUp()
        {
            

            init(platform);
        }


        public textLayout initCommonLayout()
        {
            textLayout layout = new textLayout(platform);

            layout.addLayer(footerLine, layerBlending.transparent, 10);
          //  layout.addLayer(statusLine, layerBlending.transparent, 15);
           // layout.addLayer(headerLine, layerBlending.transparent, 20);
            
            return layout;
        }


        public virtual void doExit()
        {
            doKeepRunning = false;
            current = null;
            Environment.Exit(1);
            
        }

        public void setTitleContent(String sufix)
        {
           

        }


        public smartInfoLineSection headerLine { get; set; }
        public smartInfoLineSection statusLine { get; set; }
        public smartInfoLineSection footerLine { get; set; }

        
        public abstract void goToMainPage();

        public abstract void goToWelcomePage();

        public abstract void goBack();
       

        /// <summary>
        /// poziva se kada dodje do zatvaranja nekog ekrana
        /// </summary>
        /// <param name="openedScreen"> </param>
        /// <param name="closedScreen"></param>
        public abstract void onScreenOpened(IAceTerminalScreen openedScreen);


        /// <summary>
        /// poziva se kada dodje do zatvaranja nekog ekrana
        /// </summary>
        /// <param name="closedScreen"></param>
        public abstract void onScreenClosed(IAceTerminalScreen closedScreen);


        #region --- current ------- refernca prema trenutnom ekranu
        private IAceTerminalScreen _current;
        /// <summary>
        /// Reference to the current screen
        /// </summary>
        public IAceTerminalScreen current
        {
            get
            {
                return _current;
            }
            set
            {
                if (_current != null)
                {
                
                    lastScreen = _current;
                    if (lastScreen != null)
                    {
                        onScreenClosed(lastScreen);
                    }

                }
                _current = value;
                if (_current != null)
                {
                    onScreenOpened(_current);
                }

            }
        }
        #endregion

        public IAceTerminalScreen lastScreen { get; set; }

        

        /// <summary>
        /// Da li je omoguceno GoBack dugme
        /// </summary>
        public Boolean idGoBackEnabled
        {
            get
            {
                Boolean output = true;
                if (lastScreen == null) return false;
                if (lastScreen == current) return false;
                
                return output;
            }
        }


      

        /// <summary>
        /// Interface refresh loop
        /// </summary>
        /// <returns></returns>
        protected override bool doApplicationLoop()
        {
            if (current == null)
            {    
                return false;
            }

            doUpdateInterface();

            Boolean doKeepReading = true;

            inputResultCollection results = new inputResultCollection();
            results.platform = platform;
            
            while (doKeepReading)
            {
                render(platform);
                results = read(results);
                doKeepReading = results.doKeepReading();
                
            }

            results = execute(results);

            //  current.loop(this);

            return doKeepRunning;
        }



        #region --- platform ------- platforma za konzolu
        private IPlatform _platform;
        /// <summary>
        /// platforma za konzolu
        /// </summary>
        public IPlatform platform
        {
            get { return aceCommons.platform; }
            
        }
        #endregion

        /// <summary>
        /// #0 Izvrsava se prvi put - kada se instancira. Customized sekvenca inicijalizacije
        /// </summary>
        /// <param name="platform1"> </param>
        public void init(IPlatform platform1)
        {
            headerLineLeftContent = appAboutInfo.applicationName + " " + appAboutInfo.applicationVersion;

            footerLine = new smartInfoLineSection(25, platform, 0, 2);
            footerLine.setStyle(textSectionLineStyleName.heading);
        }

        /// <summary>
        /// #1 Generise sadrzaj
        /// </summary>
        public void render(IPlatform platform, Boolean doClearScreen=true)
        {
            doUpdateInterface();

            current.render(platform, doClearScreen);
        }

        /// <summary>
        /// #2 Očitava ulaz
        /// </summary>
        /// <param name="__results">todo: describe __results parameter on read</param>
        public inputResultCollection read(inputResultCollection __results)
        {
            if (__results == null) __results = new inputResultCollection();
            
            __results.platform = platform;

            return current.read(__results);
        }

        /// <summary>
        /// #3 Vrsi rad nakon sto je obradjen ulaz
        /// </summary>
        /// <param name="__inputs">todo: describe __inputs parameter on execute</param>
        public inputResultCollection execute(inputResultCollection __inputs)
        {
            __inputs.platform = platform;

            if (__inputs.doIfKey("Escape"))
            {
                goBack();
                return __inputs;
            }

            return current.execute(__inputs);
        }
    }
}
