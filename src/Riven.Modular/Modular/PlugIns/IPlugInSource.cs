using JetBrains.Annotations;
using System;
using System.Text;

namespace Riven.Modular.PlugIns
{
    public interface IPlugInSource
    {
        [NotNull]
        Type[] GetModules();
    }
}
