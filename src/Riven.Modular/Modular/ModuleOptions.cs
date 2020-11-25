using JetBrains.Annotations;
using Riven.Modular.PlugIns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Modular
{
    public class ModuleOptions
    {
        [NotNull]
        public PlugInSourceList PlugInSources { get; }

        public ModuleOptions()
        {
            PlugInSources = new PlugInSourceList();
        }
    }
}
