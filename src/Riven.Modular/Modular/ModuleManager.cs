using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using JetBrains.Annotations;

namespace Riven.Modular
{
    public class ModuleManager : IModuleManager
    {
        /// <summary>
        /// 模块明细和实例
        /// </summary>
        public virtual IReadOnlyList<IModuleDescriptor> ModuleDescriptors { get; protected set; }

        /// <summary>
        /// ioc容器
        /// </summary>
        public virtual IServiceProvider ServiceProvider { get; protected set; }

        protected readonly ModuleOptions _moduleOptions;

        public ModuleManager([NotNull] ModuleOptions moduleOptions)
        {
            if (moduleOptions == null)
            {
                throw new ArgumentNullException(nameof(moduleOptions));
            }

            _moduleOptions = moduleOptions;
        }


        /// <inheritdoc/>
        public virtual void StartModule<TModule>(IServiceCollection services)
           where TModule : IAppModule
        {
            var moduleDescriptors = new List<IModuleDescriptor>();


            // 查找所有模块
            var modules = this.FindAllModule<TModule>();

            // 排序
            modules = this.ModuleSort<TModule>(modules);
            foreach (var item in modules)
            {
                if (moduleDescriptors.Any(o => o.ModuleType.FullName == item.ModuleType.FullName))
                {
                    continue;
                }

                moduleDescriptors.Add(item);
                services.AddSingleton(item.ModuleType, item.Instance);
            }

            ModuleDescriptors = moduleDescriptors.AsReadOnly();
        }

        /// <inheritdoc/>
        public virtual IServiceCollection ConfigurationService(IServiceCollection services, IConfiguration configuration)
        {
            var context = new ServiceConfigurationContext(services, configuration);


            foreach (var module in ModuleDescriptors)
            {
                (module.Instance as IAppModule)?.OnPreConfigureServices(context);
            }

            foreach (var module in ModuleDescriptors)
            {
                (module.Instance as IAppModule)?.OnConfigureServices(context);
            }

            foreach (var module in ModuleDescriptors)
            {
                (module.Instance as IAppModule)?.OnPostConfigureServices(context);
            }


            services.AddSingleton(context);

            return services;
        }

        /// <inheritdoc/>
        public virtual IServiceProvider ApplicationInitialization(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var context = new ApplicationInitializationContext(serviceProvider, configuration);


            foreach (var module in ModuleDescriptors)
            {
                (module.Instance as IAppModule)?.OnPreApplicationInitialization(context);
            }

            foreach (var module in ModuleDescriptors)
            {
                (module.Instance as IAppModule)?.OnApplicationInitialization(context);
            }

            foreach (var module in ModuleDescriptors)
            {
                (module.Instance as IAppModule)?.OnPostApplicationInitialization(context);
            }


            this.ServiceProvider = serviceProvider;
            return serviceProvider;
        }


        /// <inheritdoc/>
        public virtual void ApplicationShutdown()
        {
            var context = new ApplicationShutdownContext(this.ServiceProvider);

            var modules = ModuleDescriptors.Reverse().ToList();

            foreach (var module in modules)
            {
                (module.Instance as IAppModule)?.OnApplicationShutdown(context);
            }
        }

        #region 模块排序

        public virtual List<IModuleDescriptor> FindAllModule<TModule>()
           where TModule : IAppModule
        {
            // 查找所有模块
            var modules = this.VisitModule(typeof(TModule));

            // 加载插件模块
            foreach (var moduleType in this._moduleOptions.PlugInSources?.GetAllModules())
            {
                if (modules.Any(m => m.ModuleType == moduleType))
                {
                    continue;
                }
                modules.AddRange(this.VisitModule(moduleType));
            }

            return modules;
        }

        /// <inheritdoc/>
        public virtual List<IModuleDescriptor> ModuleSort<TModule>(List<IModuleDescriptor> input)
        where TModule : IAppModule
        {
            return Topological.Sort(input, o => o.Dependencies);
        }


        /// <summary>
        /// 遍历模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        protected virtual List<IModuleDescriptor> VisitModule(Type moduleType, Type parentModuleType = null, Dictionary<Type, bool> parentDependModuleType = null)
        {
            var moduleDescriptors = new List<IModuleDescriptor>();
            if (!moduleType.IsModule())
            {
                goto end;
            }
            if (parentDependModuleType == null)
            {
                parentDependModuleType = new Dictionary<Type, bool>();
            }

            // 获取依赖模块信息
            var dependModulesAttribute = moduleType.GetCustomAttribute<DependsOnAttribute>(false);

            // 创建模块信息
            // 依赖模块为空
            if (dependModulesAttribute == null)
            {
                moduleDescriptors.Add(
                    new ModuleDescriptor(moduleType)
                    );
                goto end;
            }

            // 依赖属性不为空,递归获取依赖
            var dependModuleDescriptors = new List<IModuleDescriptor>();
            foreach (var dependModuleType in dependModulesAttribute.DependModuleTypes)
            {
                if (parentDependModuleType.TryGetValue(dependModuleType, out var isEx) || dependModuleType == parentModuleType)
                {
                    throw new InvalidOperationException($"There are cyclic dependencies！\r\n{moduleType.FullName} <--> {dependModuleType.FullName}");
                }

                parentDependModuleType.Add(dependModuleType, true);
                dependModuleDescriptors.AddRange(
                    this.VisitModule(dependModuleType, moduleType, parentDependModuleType)
                );
            }

            parentDependModuleType.Clear();
            // 创建模块信息,内容为模块类型和依赖信息
            moduleDescriptors.Add(
                new ModuleDescriptor(moduleType, dependModuleDescriptors.ToArray())
                );


        end: // 返回结果
            return moduleDescriptors;
        }


        #endregion
    }
}
