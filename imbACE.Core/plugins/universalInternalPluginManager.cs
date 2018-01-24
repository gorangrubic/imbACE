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
using imbSCI.Data.interfaces;

namespace imbACE.Core.plugins
{


    /// <summary>
    /// Universal types / plugins manager for general classes, having constructor without arguments
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="imbACE.Core.plugins.internalPluginManager{T}" />
    public class universalInternalPluginManager<T>:internalPluginManager<T> where T:class, new()
    {
        protected override bool supportDirtyNaming { get { return true; } }

        public universalInternalPluginManager()
        {

        }

        public void LoadPlugins(ILogBuilder output)
        {
            loadPlugins(output);
        }


        public T GetInstance(String plugin_name,ILogBuilder output = null)
        {
            return GetPluginInstance(plugin_name);
        }

    }

}