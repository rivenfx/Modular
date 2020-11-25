using JetBrains.Annotations;
using System;

namespace Riven.Modular
{
    public interface IDependedTypesProvider
    {
        /// <summary>
        /// 获取依赖的类型
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Type[] GetDependedTypes();
    }
}
