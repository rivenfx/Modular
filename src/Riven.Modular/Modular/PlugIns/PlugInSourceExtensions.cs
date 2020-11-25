using JetBrains.Annotations;
using System;
using System.Linq;

namespace Riven.Modular.PlugIns
{
    public static class PlugInSourceExtensions
    {
        [NotNull]
        public static Type[] GetModulesWithAllDependencies([NotNull] this IPlugInSource plugInSource)
        {
            if (plugInSource == null)
            {
                throw new ArgumentNullException(nameof(plugInSource));
            }

            return plugInSource
                .GetModules()
                .SelectMany(ModuleHelper.FindAllModuleTypes)
                .Distinct()
                .ToArray();
        }
    }
}
