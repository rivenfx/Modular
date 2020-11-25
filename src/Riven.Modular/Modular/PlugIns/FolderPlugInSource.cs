using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Riven.Modular.PlugIns
{
    public class FolderPlugInSource : IPlugInSource
    {
        public string Folder { get; }

        public SearchOption SearchOption { get; set; }

        public Func<string, bool> Filter { get; set; }

        public FolderPlugInSource(
            [NotNull] string folder,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                throw new ArgumentException(nameof(folder));
            }

            Folder = folder;
            SearchOption = searchOption;
        }

        public Type[] GetModules()
        {
            var modules = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.IsAppModule() && !modules.Contains(type))
                        {
                            modules.Add(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not get module types from assembly: " + assembly.FullName, ex);
                }
            }

            return modules.ToArray();
        }

        protected virtual IEnumerable<Assembly> GetAssemblies()
        {
            var assemblyFiles = this.GetAssemblyFiles(Folder, SearchOption);

            if (Filter != null)
            {
                assemblyFiles = assemblyFiles.Where(Filter);
            }

            return assemblyFiles.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);
        }

        protected virtual IEnumerable<string> GetAssemblyFiles(string folderPath, SearchOption searchOption)
        {
            return Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));
        }
    }
}
