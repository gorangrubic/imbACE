// --------------------------------------------------------------------------------------------------------------------
// <copyright file="aceTerminalWelcomeScreen.cs" company="imbVeles" >
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
namespace imbACE.Services.terminal.smartScreen
{
    using System;
    using imbACE.Core.commands.menu.core;
    using imbACE.Core.core.exceptions;
    using imbACE.Core.enums.platform;
    using imbACE.Core.operations;
    using imbACE.Services.application;
    using imbACE.Services.platform.core;
    using imbACE.Services.platform.interfaces;
    using imbACE.Services.terminal.core;
    using imbACE.Services.terminal.screen;
    using imbACE.Services.textBlocks.enums;
    using imbACE.Services.textBlocks.input;
    using imbACE.Services.textBlocks.smart;
    using imbSCI.Core.reporting.zone;

    //using imbACE.Core.zone;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class aceTerminalWelcomeScreen<T>:aceTerminalMenuScreenBase<T> where T:aceTerminalApplication
    {

        #region --- message ------- sadrzaj poruke koja se prikazuje

        private String _message = "";
        /// <summary>
        /// sadrzaj poruke koja se prikazuje
        /// </summary>
        protected String message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged("message");
            }
        }
        #endregion


        #region --- messageTitle ------- sadrzaj naslova

        private String _messageTitle = "";
        /// <summary>
        /// Bindable property
        /// </summary>
        protected String messageTitle
        {
            get
            {
                return _messageTitle;
            }
            set
            {
                _messageTitle = value;
                OnPropertyChanged("messageTitle");
            }
        }
        #endregion


        #region --- messageSection ------- section sa porukom
        private smartMessageSection _messageSection;
        /// <summary>
        /// section sa porukom
        /// </summary>
        public smartMessageSection messageSection
        {
            get
            {
                return _messageSection;
            }
            set
            {
                _messageSection = value;
                OnPropertyChanged("messageSection");
            }
        }
        #endregion
        

        public aceTerminalWelcomeScreen(T _app)
            : base(_app, " ")
        {
            message = _app.appAboutInfo.welcomeMessage;
            messageTitle = _app.appAboutInfo.applicationName;
            init(_app.platform);
        }


        public aceTerminalWelcomeScreen(T _app, String welcomeMessage, String welcomeTitle, String __title = "Introduction") : base(_app, __title)
        {
            message = welcomeMessage;
            messageTitle = welcomeTitle;

            

            /// pozvati na kraju
            init(_app.platform);

        }

       
        /// <summary>
        /// #0 Izvrsava se prvi put - kada se instancira. Customized sekvenca inicijalizacije
        /// </summary>
        /// <param name="platform"> </param>
        public override void init(IPlatform platform)
        {
            menu = new aceMenu();
            menu.menuTitle = "Welcome menu";
            menu.setItem(menuItemContinue);
            menu.setItem(menuItemAbout);
            menu.setItem(menuItemQuit);
            menu.selected = menuItemContinue;



            //menu.setItem("Continue", "", "C", true);
            //menu.setItem("About", "", "A", false);
            //menu.setItem("Quit", "", "Q", false);

            messageSection = new smartMessageSection(messageTitle, message, platform.height, platform.width, 2, 2);
            messageSection.height = 17;
            messageSection.margin.top = 3;
            messageSection.margin.bottom = 1;
            messageSection.padding.bottom = 1;
            messageSection.doInverseColors = false;
            messageSection.backColor = platformColorName.Black;
            messageSection.foreColor = platformColorName.Gray;

            menuSection = new smartMenuSection(menu, platform.height, platform.width, 2, 1);
            menuSection.renderView = textInputMenuRenderView.inlineKeyListGroup;
            menuSection.doShowTitle = false;
            menuSection.doInverseColors = false;
            menuSection.padding.top = 1;
            menuSection.padding.bottom = 1;
            menuSection.backColor = platformColorName.White;
            menuSection.foreColor = platformColorName.Blue;

            menuSection.setStyle(textSectionLineStyleName.itemlinst);


            messageSection.setAttachment(menuSection);

          
            layout.addLayer(messageSection, layerBlending.transparent, 50);

            
        }

        protected aceMenuItem menuItemContinue = new aceMenuItem("Continue", "C", "", "", null);
        protected aceMenuItem menuItemAbout = new aceMenuItem("About", "A", "", "", null);
        protected aceMenuItem menuItemQuit = new aceMenuItem("Quit", "Q", "", "", null);

        /// <summary>
        /// Obnavlja dinamicki deo sadrzaja
        /// </summary>
        public override void refresh()
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// #3 Vrsi rad nakon sto je obradjen ulaz
        /// </summary>
        public override inputResultCollection execute(inputResultCollection __inputs)
        {
            var menuResult = __inputs.getBySection(menuSection);
            var selectedItem = menuResult.result as aceMenuItem;

            if (selectedItem == menuItemContinue)
            {
                application.goToMainPage();
            }

            if (selectedItem == menuItemAbout)
            {
                
            }

            if (selectedItem == menuItemQuit)
            {
                application.doExit();
            }

            return __inputs;
        }

       
    }
}
