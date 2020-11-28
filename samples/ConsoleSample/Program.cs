using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven;
using Riven.Modular.PlugIns;
using SampleCommon;
using System;
using System.IO;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = GetAppConfiguration(); // 你应用的配置
            IServiceCollection services = new ServiceCollection();



            services.AddSingleton<IConfiguration>(Configuration);
            services.AddRivenModule<MyAppStartupModule>(Configuration, (options) =>
            {
                // 加载插件
                var rootPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                options.PlugInSources.Add(
                    // 扫描目录
                    new FolderPlugInSource(Path.Join(rootPath, "plugins", "netstandard2.0"))
                    );
                options.PlugInSources.Add(
                   // 指定文件
                   new FilePlugInSource(Path.Join(rootPath, "plugins", "netstandard2.0", "PluginA.dll"))
                   );
            });


            IServiceProvider serviceProvider = services.BuildServiceProvider();
            serviceProvider.UseRivenModule();
        }


        static IConfiguration GetAppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            return configurationBuilder
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
        }
    }
}
