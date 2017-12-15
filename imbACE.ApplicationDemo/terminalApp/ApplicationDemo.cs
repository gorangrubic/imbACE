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
    using imbACE.Services.terminal.dialogs;
    using imbACE.Services.terminal.dialogs.core;
    using imbACE.Services.terminal.smartScreen;
    using imbACE.Services.textBlocks.smart;
    using System;

    public class ApplicationDemo : aceTerminalApplication
    {
        public static ApplicationDemo application;

        public static void Main(string[] args)
        {
            application = new ApplicationDemo();
            
            application.StartApplication(args);
            
            // here you may place code that has to be performed after user called application closing
            // ...
        }


        public string TEXT_WELCOME = @"This is imbACE: Advanced Console Environment application with Text User Interface called TextBlocks."
           + "This is Welcome Screen customizable preset class, showing this message and given number of menu options in block below this text box. You maybe noticed there is whole-word line break algorithm applied!";

        public ApplicationDemo()
        {
            screenWelcome = new aceTerminalWelcomeScreen<ApplicationDemo>(this, TEXT_WELCOME, "ACE Terminal Application DEMO", "Welcome screen");
            
        }

        
        public override void doUpdateInterface()
        {
            //
        }



        public override void goToMainPage()
        {
         
            dialogSelectFile dSelectFile = new dialogSelectFile(platform, folder_projects.path, dialogSelectFileMode.selectFileToOpen, "*.*", "DEMO for dialogSelectFile");
            var results = dSelectFile.open(platform, new dialogFormatSettings(dialogStyle.greenDialog, dialogSize.mediumBox));

            var dUniversal = new dialogMessageBoxWithOptions<String>(platform, "Dialog with options", "Array of strings as options", new String[] { "Option 01", "Option 02", "Last Option" });
            var option = dUniversal.open(platform, new dialogFormatSettings(dialogStyle.greenDialog, dialogSize.mediumBox));
            
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