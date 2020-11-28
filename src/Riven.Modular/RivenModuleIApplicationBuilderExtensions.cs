using Riven.Modular;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace Riven
{
    public static class RivenModuleIApplicationBuilderExtensions
    {
        /// <summary>
        /// 使用RivenModule
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceProvider UseRivenModule(this IServiceProvider serviceProvider)
        {
            var moduleManager = serviceProvider.GetRequiredService<IModuleManager>();

            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                moduleManager.ApplicationShutdown();
            };


            return moduleManager.ApplicationInitialization(serviceProvider);
        }
    }
}
