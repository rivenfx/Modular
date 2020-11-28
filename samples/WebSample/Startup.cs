using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven;
using SampleCommon;

namespace WebSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRivenModule<MyAppStartupModule>(Configuration, (options) =>
            {
                // 加载模块插件

                //var rootPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //// 目录插件源
                //options.PlugInSources.Add(
                //    new FolderPlugInSource(Path.Join(rootPath, "plugins", "netstandard2.0"))
                //    );
                //
                //// 文件插件源
                //options.PlugInSources.Add(
                //   new FilePlugInSource(Path.Join(rootPath, "plugins", "netstandard2.0", "PluginA.dll"))
                //   );
                //
                //// 类型插件源
                //options.PlugInSources.Add(
                //   new TypePlugInSource(typeof(TestModuleC))
                //   );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.UseRivenModule();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
