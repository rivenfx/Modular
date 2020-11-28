using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Modular
{
    /// <summary>
    /// 应用模块管理器
    /// </summary>
    public interface IModuleManager
    {
        /// <summary>
        /// 模块描述信息
        /// </summary>
        IReadOnlyList<IModuleDescriptor> ModuleDescriptors { get; }

        /// <summary>
        /// 启动模块
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        void StartModule<TModule>(IServiceCollection services)
            where TModule : IAppModule;

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        IServiceCollection ConfigurationService(IServiceCollection services, IConfiguration configuration);


        /// <summary>
        /// 配置应用初始化
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        IServiceProvider ApplicationInitialization(IServiceProvider serviceProvider);

        /// <summary>
        /// 应用程序停止
        /// </summary>
        void ApplicationShutdown();

    }
}
