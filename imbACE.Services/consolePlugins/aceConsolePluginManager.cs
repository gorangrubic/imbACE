using System;
using System.Linq;
using System.Collections.Generic;
namespace imbACE.Services.consolePlugins
{
    using imbACE.Core.commands.menu.core;
    using imbACE.Core.core;
    using imbACE.Core.operations;
    using imbACE.Core.plugins.core;
    using imbACE.Services.console;
    using imbSCI.Core.reporting;
    using imbSCI.Core.reporting.render;
    using imbACE.Core.plugins;


    /// <summary>
    /// Manager for console plug-ins that can be dynamically instantiated console plug-ins
    /// </summary>
    /// <seealso cref="internalPluginManager{imbACE.Services.consolePlugins.IAceConsolePlugin}" />
    public class aceConsolePluginManager : internalPluginManager<IAceConsolePlugin>
    {

    }

}