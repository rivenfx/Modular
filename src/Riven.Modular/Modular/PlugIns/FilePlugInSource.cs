using System;
using System.Collections.Generic;
using System.Runtime.Loader;

namespace Riven.Modular.PlugIns
{
    public class FilePlugInSource : IPlugInSource
    {
        public string[] FilePaths { get; }

        public FilePlugInSource(params string[] filePaths)
        {
            FilePaths = filePaths ?? new string[0];
        }

        public Type[] GetModules()
        {
            var modules = new List<Type>();

            foreach (var filePath in FilePaths)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);

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
    }
}
