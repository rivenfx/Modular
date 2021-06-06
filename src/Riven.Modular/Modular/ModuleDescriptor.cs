using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Modular
{
    public class ModuleDescriptor : IModuleDescriptor
    {
        protected IAppModule _instance;

        public virtual Type ModuleType { get; protected set; }

        public virtual IModuleDescriptor[] Dependencies { get; protected set; }

        public virtual IAppModule Instance
        {
            get
            {
                if (this._instance == null)
                {
                    this._instance = (IAppModule)Activator.CreateInstance(this.ModuleType);
                }
                return this._instance;
            }
        }

        public ModuleDescriptor(Type moduleType, params IModuleDescriptor[] dependencies)
        {
            this.ModuleType = moduleType;
            this.Dependencies = dependencies ?? new ModuleDescriptor[0];
        }

        public override string ToString()
        {
            return ModuleType.FullName;
        }
    }
}
