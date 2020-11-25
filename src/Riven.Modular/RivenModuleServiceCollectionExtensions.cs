using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven.Modular;

namespace Riven
{
    /// <summary>
    /// 模块服务扩展
    /// </summary>
    public static class RivenModuleServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Riven模块服务
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="moduleOptionsConfiguration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRivenModule<TModule>(this IServiceCollection services, IConfiguration configuration, Action<ModuleOptions> moduleOptionsConfiguration = null)
            where TModule : IAppModule
        {
            // 模块配置
            var moduleOptions = new ModuleOptions();
            moduleOptionsConfiguration?.Invoke(moduleOptions);

            // 创建模块管理器
            var moduleManager = new ModuleManager(moduleOptions);

            // 启动模块
            moduleManager.StartModule<TModule>(services);

            // 配置服务
            moduleManager.ConfigurationService(services, configuration);


            services.TryAddSingleton<IModuleManager>(moduleManager);
            return services;
        }
    }
}
