using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Modular
{
    /// <summary>
    /// 模块依赖的模块
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedTypesProvider
    {
        /// <summary>
        /// 依赖的模块类型
        /// </summary>
        [NotNull]
        public virtual Type[] DependModuleTypes { get; protected set; }

        public DependsOnAttribute(params Type[] dependModuleTypes)
        {
            DependModuleTypes = dependModuleTypes ?? new Type[0];
        }


        public virtual Type[] GetDependedTypes()
        {
            return DependModuleTypes;
        }
    }
}
