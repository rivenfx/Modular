using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Modular
{
    public static class AppModuleExtensions
    {
        /// <summary>
        /// 模块接口类型全名称
        /// </summary>
        public static string ModuleInterfaceTypeFullName { get; } = typeof(IAppModule).FullName;

        /// <summary>
        /// 是否实现了 IAppModule 接口
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static bool IsAppModule(this Type moduleType)
        {

            // 过滤抽象类、接口、泛型类、非类
            if (moduleType.IsAbstract
                || moduleType.IsInterface
                || moduleType.IsGenericType
                || !moduleType.IsClass)
            {
                return false;
            }

            // 过滤没有实现IRModule接口的类
            var baseInterfaceType = moduleType.GetInterface(ModuleInterfaceTypeFullName, false);
            if (baseInterfaceType == null)
            {
                return false;
            }


            return true;
        }
    }
}
