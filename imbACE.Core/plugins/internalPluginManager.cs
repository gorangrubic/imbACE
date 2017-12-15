using System;
using System.Linq;
using System.Collections.Generic;
using imbACE.Core.application;
using imbACE.Core.operations;
using imbACE.Core.plugins.core;
using imbACE.Core.plugins.deployer;
using imbSCI.Core.files.folders;
using imbSCI.Core.reporting;
using imbSCI.Data;
using imbSCI.Data.collection.nested;
using System.IO;
using System.Reflection;
using System.Text;

namespace imbACE.Core.plugins
{

    /// <summary>
    /// Scans for plug-ins that have {T} interface
    /// </summary>
    /// <typeparam name="T">Interface for managed plugin type</typeparam>
    public class internalPluginManager<T> : pluginManagerBase where T: class, IAcePluginBase
    {
        public internalPluginManager()
        {
            name = typeof(T).Name + " Manager";
        }

        protected void loadPlugins(ILogBuilder output)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type pluginType = typeof(T);

            foreach (Assembly ass in assemblies)
            {
                var types = ass.GetTypes();

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
                            registerPlugin(type, ass.GetName().FullName, output);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Creates the type instance
        /// </summary>
        /// <param name="plugin_name">Name of the plugin.</param>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="output">The output.</param>
        /// <returns></returns>
        public T GetPluginInstance(String plugin_name, String instanceName="", ILogBuilder output = null)
        {

            Type t = resolvePlugin(plugin_name, output);

            if (t == null) return null;


            T instance = null;

            try
            {
                instance = Activator.CreateInstance(t, new Object[] { }) as T;

                if (instanceName != "") instance.instanceName = instanceName;
            }
            catch (Exception ex) {

                output.log("Plugin instance creation for [" + t.Name + "] failed as argument-less constructor not found?! Exception: " + ex.Message);
            }
            return instance;
        }
    }

}