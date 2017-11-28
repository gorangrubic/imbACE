// --------------------------------------------------------------------------------------------------------------------
// <copyright file="commandLineEntry.cs" company="imbVeles" >
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

namespace imbACE.Core.commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 
    /// </summary>
    public class commandLineEntry
    {
        /// <summary>
        /// The commandformat explicit: explore word="kontakti";engine=hunspell;log=5;
        /// </summary>
        public static Regex COMMANDFORMAT_Explicit = new Regex("^([\\w\\.]*)[\\s]*((\\w*)[\\s=]+[\\\"]?([\\w\\d\\.\\*,:\\?\\+\\-\\\\_\\!`&\\$\\%\\s\\{\\}]*)[\\\"]?[;\\s]*)*");

        /// <summary>
        /// The commandformat implicit: explore "kontakti", 510, hunspell 
        /// </summary>
        public static Regex COMMANDFORMAT_Implicit = new Regex("^([\\w\\.]*)[\\s]*([\\\"]?([\\w\\d\\*\\.,:\\?\\+\\-\\\\_\\!`&\\$\\%\\s\\{\\}]*)[\\\"]?[; \\s]*)*");

        public static Regex COMMANDFORMAT_OnlyCommand = new Regex(@"^([\w_\.]*)$");

        public static Regex COMMANDFORMAT_CommandAndSpecialSufix = new Regex(@"^([\w_\.]*)\s{1,3}[\?\*\!\#\%]{1}$");

        public const String COMMANDPREFIX_SEPARATOR = ".";

        public commandLineSpecialFunction specialFunction { get; set; } = commandLineSpecialFunction.none;

        public Boolean hasPrefix { get
            {
                return prefix.Any();
            }
        }

        public List<String> prefix { get; set; } = new List<string>();


        /// <summary> </summary>
        public Boolean commentedOut { get; internal set; } = false;

        public Boolean hasComment
        {
            get
            {
                if (commentedOut) return true;
                return !commentLine.isNullOrEmpty();
            }
        }

        public Boolean hasCommand
        {
            get
            {
                if (format == commandLineFormat.emptyLine) return false;
                if (format == commandLineFormat.onlyComment) return false;
                if (command.isNullOrEmpty()) return false;
                return true;
            }
        }

        public String commentLine { get; internal set; } = "";

        public const String commentPrefix = "//";

        public const String PARAM_WILLCARD = "*";

        public const String PARAM_HELP = "?";

        /// <summary>
        /// The command line was empty, without any command
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty line; otherwise, <c>false</c>.
        /// </value>
        public Boolean isEmptyLine { get; protected set; }

        private static Object commandLineLock = new Object();


        public static void processParams(commandLineEntry entry, String[] args)
        {
            String __line = "";
            foreach (String arg in args)
            {
                __line = __line + arg + " ";
            }
            process(entry, __line);
        }


        /// <summary>
        /// Processes the specified input and populates the entry
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="input">The input.</param>
        public static void process(commandLineEntry entry, String input) {

            lock (commandLineLock)
            {
                try
                {

                    input = input.Trim();

                    if (input.isNullOrEmpty())
                    {
                        entry.format = commandLineFormat.emptyLine;
                        entry.isEmptyLine = true;
                        return;
                    }

                    if (input.StartsWith(commentPrefix))
                    {
                        entry.commentedOut = true;
                        entry.commentLine = input.removeStartsWith(commentPrefix);
                        entry.format = commandLineFormat.onlyComment;
                        return;
                    }

                   
                    if (input.Contains(commentPrefix))
                    {
                        var splits = imbSciStringExtensions.SplitOnce(input, commentPrefix);
                        input = splits[0];
                        if (splits.Count > 1)
                        {
                            entry.commentLine = splits[1];
                        }
                    }


                    MatchCollection mtch = null;
                    Match mtc = null;

                    if (COMMANDFORMAT_OnlyCommand.IsMatch(input))
                    {
                        entry.format = commandLineFormat.onlyCommand;
                        entry.command = input;
                    } else if (COMMANDFORMAT_CommandAndSpecialSufix.IsMatch(input))
                    {
                        entry.format = commandLineFormat.onlyCommand;

                        if (input.EndsWith(PARAM_WILLCARD))
                        {
                            entry.command = input.removeEndsWith(PARAM_WILLCARD).Trim();
                            entry.specialFunction = commandLineSpecialFunction.askForParameters;


                            

                        }

                        if (input.EndsWith(PARAM_HELP))
                        {
                            entry.command = input.removeEndsWith(PARAM_HELP).Trim();
                            entry.specialFunction = commandLineSpecialFunction.helpOnCommand;
                         
                        }

                    }
                    else
                    {

                        if (input.Contains("="))
                        {
                            mtch = COMMANDFORMAT_Explicit.Matches(input);
                            mtc = mtch[0];
                            entry.command = mtc.Groups[1].Value;
                            entry.format = commandLineFormat.explicitFormat;
                            foreach (Capture cap in mtc.Groups[3].Captures)
                            {
                                if (!cap.Value.isNullOrEmpty())
                                {
                                    entry.parameterName.Add(cap.Value);
                                }


                                
                            }

                            foreach (Capture cap in mtc.Groups[4].Captures)
                            {
                                if (!cap.Value.isNullOrEmpty())
                                {
                                    entry.parameterValue.Add(cap.Value);
                                }



                            }

                            
                        }
                        else
                        {
                            mtch = COMMANDFORMAT_Implicit.Matches(input);
                            mtc = mtch[0];
                            entry.command = mtc.Groups[1].Value;
                            entry.format = commandLineFormat.implicitFormat;
                            foreach (Capture cap in mtc.Groups[3].Captures)
                            {
                                if (!cap.Value.isNullOrEmpty())
                                {
                                    entry.parameterValue.Add(cap.Value);
                                }
                            }
                        }
                    }
                } catch (Exception ex)
                {
                    entry.isSyntaxError = true;
                    entry.errorMessage = "Command syntax error:"+ ex.Message;
                }

                if (entry.command.Contains(COMMANDPREFIX_SEPARATOR))
                {
                    var parts = entry.command.SplitSmart(COMMANDPREFIX_SEPARATOR, "", true);
                    entry.command = parts.Last();
                    parts.Remove(entry.command);
                    entry.prefix.AddRange(parts);
                }
            }

        }


        


        public commandLineEntry(String input)
        {
            if (input.isNullOrEmpty()) return;
            inputLine = input;
            process(this, input);
        }


        private String _inputLine = "";
        /// <summary> </summary>
        public String inputLine
        {
            get
            {
                return _inputLine;
            }
            protected set
            {
                _inputLine = value;
                
            }
        }


        #region ----------- Boolean [ isSyntaxError ] -------  [Indicates if the command had syntax error]
        private Boolean _isSyntaxError = false;
        /// <summary>
        /// Indicates if the command had syntax error
        /// </summary>
        [Category("Switches")]
        [DisplayName("isSyntaxError")]
        [Description("Indicates if the command had syntax error")]
        public Boolean isSyntaxError
        {
            get { return _isSyntaxError; }
            set { _isSyntaxError = value;  }
        }
        #endregion


        private commandLineFormat _format = commandLineFormat.unknown;
        /// <summary>
        /// 
        /// </summary>
        public commandLineFormat format
        {
            get { return _format; }
            set { _format = value; }
        }


        private String _command;
        /// <summary>
        /// 
        /// </summary>
        public String command
        {
            get { return _command; }
            set { _command = value; }
        }


        private String _errorMessage = "";

        /// <summary>
        /// 
        /// </summary>
        public String errorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }


        private List<String> _parameterName = new List<String>();
        /// <summary> </summary>
        public List<String> parameterName
        {
            get
            {
                return _parameterName;
            }
            protected set
            {
                _parameterName = value;
            }
        }


        private List<String> _parameterValue = new List<String>();
        /// <summary> </summary>
        public List<String> parameterValue
        {
            get
            {
                return _parameterValue;
            }
            protected set
            {
                _parameterValue = value;
            }
        }


    }

}