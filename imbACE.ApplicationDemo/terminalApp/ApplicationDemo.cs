// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDemo.cs" company="imbVeles" >
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

namespace imbACE.ApplicationDemo.terminalApp
{
    using imbACE.Core.application;
    using imbACE.Services.application;
    using imbACE.Services.terminal.core;

    public class ApplicationDemo : aceTerminalApplication
    {
        public static ApplicationDemo application;

        public static void Main(string[] args)
        {
            application = new ApplicationDemo();

            application.StartApplication(args);


            
        }

        public override void doUpdateInterface()
        {
            //
        }

        public override void goBack()
        {
            //
        }

        public override void goToMainPage()
        {
            //
        }

        public override void goToWelcomePage()
        {
            //
        }

        public override void onScreenClosed(IAceTerminalScreen closedScreen)
        {
            //
        }

        public override void onScreenOpened(IAceTerminalScreen openedScreen)
        {
            //
        }

        public override void setAboutInformation()
        {
            appAboutInfo = new aceApplicationInfo
            {
                software = "Terminal Demo",
                copyright = "Copyright 2017",
                organization = "imbVeles",
                author = "Goran Grubić",
                license = "Licensed under GNU General Public License v3.0",
                applicationVersion = "0.1v",
                welcomeMessage = "This is demo terminal application."
            };
        }

       
    }

}