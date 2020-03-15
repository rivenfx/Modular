using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven;
using SampleCommon;
using System;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = null; // 你应用的配置
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddRivenModule<MyAppStartupModule>(Configuration);

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
