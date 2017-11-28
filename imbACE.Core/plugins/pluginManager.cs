// --------------------------------------------------------------------------------------------------------------------
// <copyright file="pluginManager.cs" company="imbVeles" >
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
using imbACE.Core.application;
using imbACE.Core.operations;
using imbACE.Core.plugins.core;
using imbACE.Core.plugins.deployer;
using imbSCI.Core.files.folders;
using imbSCI.Core.reporting;
using imbSCI.Data;
using imbSCI.Data.collection.nested;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace imbACE.Core.plugins
{
    /// <summary>
    /// Loads plugin dlls from the associated directory and creates plugin instances on request
    /// </summary>
    public sealed class pluginManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="pluginManager"/> class.
        /// </summary>
        /// <param name="__folderToLoad">The folder to load.</param>
        public pluginManager(folderNode __folderToLoad)
        {
            folderWithPlugins = __folderToLoad;
        }

        /// <summary>
        /// List of DLL files detected
        /// </summary>
        /// <value>
        /// The DLL file names.
        /// </value>
        public List<String> dllFileNames { get; protected set; } = new List<string>();

        /// <summary>
        /// Dictionary indexing plugins by relative directory path and type short name: e.g. /myPlugins/reporter.dll -> myPlugins.reporter
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public aceConcurrentDictionary<Type> pluginTypesByPathName { get; protected set; } = new aceConcurrentDictionary<Type>();


        /// <summary>
        /// Short type name dictionary - for easier resolution/call from the ACE Script
        /// </summary>
        /// <value>
        /// The name of the plugin types by.
        /// </value>
        public aceConcurrentDictionary<Type> pluginTypesByName { get; protected set; } = new aceConcurrentDictionary<Type>();


        /// <summary>
        /// Collection of short type names that are banned because of ambiquity
        /// </summary>
        /// <value>
        /// The banned short names.
        /// </value>
        public aceDictionarySet<String, Type> bannedShortNames { get; protected set; } = new aceDictionarySet<string, Type>();



        /// <summary>
        /// Gets a new instance of plugin, specified by type name of sub directory.name path
        /// </summary>
        /// <param name="pluginName">Name of the plugin.</param>
        /// <param name="console">The console.</param>
        /// <param name="deployer">The deployer.</param>
        /// <param name="application">The application.</param>
        /// <param name="output">The output.</param>
        /// <returns></returns>
        public IAcePluginBase GetPluginInstance(String pluginName, IAceCommandConsole console, IAcePluginDeployerBase deployer, IAceApplicationBase application, ILogBuilder output = null)
        {
            if (output == null)
            {
                if (console != null) output = console.output;
            }

            Type resolution = resolvePlugin(pluginName, output);

            if (resolution == null) return null;

            IAcePluginBase plugin =  Activator.CreateInstance(resolution, new Object[] { }) as IAcePluginBase;


            return plugin;

           // resolution.getInstance();
        }



        /// <summary>
        /// Resolves the plugin by name or directory.name path
        /// </summary>
        /// <param name="plugin_name">Name of the plugin.</param>
        /// <param name="output">The output.</param>
        /// <returns></returns>
        protected Type resolvePlugin(String plugin_name, ILogBuilder output)
        {
            if (!bannedShortNames.ContainsKey(plugin_name))
            {
                if (pluginTypesByName.ContainsKey(plugin_name))
                {
                    if (output != null)
                    {
                        output.log("Plugin class [" + plugin_name + "] class resolved. ");
                    }
                    return pluginTypesByName[plugin_name];
                }
            }

            if (pluginTypesByPathName.ContainsKey(plugin_name))
            {
                if (output != null)
                {
                    output.log("Plugin class [" + plugin_name + "] class resolved. ");
                }
                return pluginTypesByPathName[plugin_name];
            } else
            {
                if (output != null)
                {
                    output.log("Plugin class [" + plugin_name + "] not found.");
                }
            }
            return null;
        }


        /// <summary>
        /// Registers the plugin.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="sourceDllPath">The source DLL path.</param>
        /// <param name="output">The output.</param>
        protected void registerPlugin(Type type, String sourceDllPath, ILogBuilder output)
        {
            if (bannedShortNames.ContainsKey(type.Name)) {

                if (pluginTypesByName.ContainsKey(type.Name))
                {
                    if (output != null) output.log("Short-name registration of [" + type.Name + "] failed: name already occupied. You'll have to call both by [directory].[typename] path.");
                    bannedShortNames.Add(type.Name, type);

                    Type pair = null;
                    if (pluginTypesByName.TryRemove(type.Name, out pair))
                    {
                        bannedShortNames.Add(type.Name, pair);
                    }
                    
                } else
                {
                    pluginTypesByName.Add(type.Name, type);
                    if (output != null) output.log("Short-name registration of [" + type.Name + "] done.");
                }
            } else
            {
                if (output != null) output.log("Short-name registration of [" + type.Name + "] failed, the name is banned from short-name registration for [" + bannedShortNames[type.Name] + "] plugins, so far"); 
            }

            String dirSufix = sourceDllPath.removeStartsWith(folderWithPlugins.path).Replace("\\", ".");
            dirSufix = dirSufix.Replace("/", ".");
            dirSufix = dirSufix.Replace("..", ".");

            String dirNamePath = dirSufix.add(type.Name, ".");

            if (pluginTypesByPathName.ContainsKey(dirNamePath))
            {
                if (output != null) output.log("[directory].[typename] (" + dirNamePath + ") registration of [" + type.Name + "] failed - can't have multiple plugins with the same name, in the same directory. Move it in sub folder or recompile under another class name");
            } else
            {
                if (output != null) output.log("[directory].[typename] (" + dirNamePath + ") registration of [" + type.Name + "] done. ");
            }
        }



        /// <summary>
        /// Loads all available plugins from the <see cref="folderNode"/> specified 
        /// </summary>
        /// <param name="output">The log builder to output info to</param>
        /// <param name="altFolder">Alternative folder with plugins to load from, at the end of the process it will set back to the existing one (if there was no existing folder, it will set this as default)</param>
        public void loadPlugins(ILogBuilder output, folderNode altFolder=null)
        {
            
            folderNode old = folderWithPlugins;


            if (altFolder != null) {

                
                folderWithPlugins = altFolder;
                if (output != null) output.log("Loading from alternative directory: " + folderWithPlugins.path);
            }

            dllFileNames.AddRange(folderWithPlugins.findFiles("*.dll", System.IO.SearchOption.AllDirectories));

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Count);
            foreach (string dllFile in dllFileNames)
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);

                try
                {
                    Assembly assembly = Assembly.Load(an);
                    //assemblies.Add(assembly);

                    Type pluginType = typeof(IAcePluginBase);
                    ICollection<Type> pluginTypes = new List<Type>();
                    //foreach (Assembly ass in assemblies)
                    //{
                        if (assembly != null)
                        {
                            Type[] types = assembly.GetTypes();
                            foreach (Type type in types)
                            {
                                if (type.IsInterface || type.IsAbstract)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (type.GetInterface(pluginType.FullName) != null)
                                    {
                                        registerPlugin(type, dllFile, output);
                                    }
                                }
                            }
                        }
                    //}



                } 
                catch (IOException ex)
                {
                    if (output != null)
                    {
                        output.log("Assembly load failed - [" + dllFile + "] - consider removing the file from the plugin directory. [" + ex.Message + "] ");
                        
                    }
                } catch (BadImageFormatException ex)
                {
                    if (output != null)
                    {
                        output.log("Invalid assembly detected: remove dll file [" + dllFile + "] from the plugin directory. [" + ex.Message + "] ");
                        output.open("fussion-log", "Assembly load failure log:", dllFile);

                        output.Append(ex.FusionLog, imbSCI.Data.enums.appends.appendType.comment, true);

                        output.close();
                    }
                } catch (Exception ex) { 
                        output.log("Plugin assembly import failed [" + dllFile + "] [" + ex.Message + "] ");
                }

                if (output != null) output.log("Plugin assembly loaded: " + an.Name);

               
            }
            if (old != null) folderWithPlugins = old;
            
        }

        /// <summary>
        /// Gets or sets the folder with plugins.
        /// </summary>
        /// <value>
        /// The folder with plugins.
        /// </value>
        public folderNode folderWithPlugins { get; set; }

        private String _name = "";
        /// <summary>
        /// Name of the manager
        /// </summary>
        public String name
        {
            get { return _name; }
            set { _name = value; }
        }

        private String _description = "";
        /// <summary>
        /// Descriptive purpose of this manager
        /// </summary>
        public String description
        {
            get { return _description; }
            set { _description = value; }
        }


    }
}
