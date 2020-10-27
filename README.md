# Riven.Modular
`Riven.Modular` is the base library for a modular implementation.You can use it to create modular applications.


## LICENSES
![GitHub](https://img.shields.io/github/license/rivenfx/Modular?color=brightgreen)
[![Badge](https://img.shields.io/badge/link-996.icu-%23FF4D5B.svg?style=flat-square)](https://996.icu/#/zh_CN)
[![LICENSE](https://img.shields.io/badge/license-Anti%20996-blue.svg?style=flat-square)](https://github.com/996icu/996.ICU/blob/master/LICENSE)

Please note: once the use of the open source projects as well as the reference for the project or containing the project code for violating labor laws (including but not limited the illegal layoffs, overtime labor, child labor, etc.) in any legal action against the project, the author has the right to punish the project fee, or directly are not allowed to use any contains the source code of this project!

## Build Status

[![Build Status](https://dev.azure.com/rivenfx/RivenFx/_apis/build/status/rivenfx.Modular?branchName=master)](https://dev.azure.com/rivenfx/RivenFx/_build/latest?definitionId=4&branchName=master)

## Nuget Packages

|Package|Status|Downloads|
|:------|:-----:|:-----:|
|Riven.Modular|[![NuGet version](https://img.shields.io/nuget/v/Riven.Modular?color=brightgreen)](https://www.nuget.org/packages/Riven.Modular/)|[![Nuget](https://img.shields.io/nuget/dt/Riven.Modular?color=brightgreen)](https://www.nuget.org/packages/Riven.Modular/)|


## Quick start

（1）创建一个控制台项目

（2）安装nuget包

````shell
Install-Package Riven.Modular
````

（3）创建一个模块

````csharp
using Riven;
using Riven.Modular;


[DependsOn(
        typeof(依赖的其它模块))
        )]
public class MyAppStartupModule : AppModule
{
        public override void OnPreConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务之前
        }

        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务
        }

        public override void OnPostConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务之后
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化之前
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化之后
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            // 应用停止
        }
}


````

（4）在 asp.net core 应用程序中使用

````csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Riven;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRivenModule<MyAppStartupModule>(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ApplicationServices.UseRivenModule();
    }
}

````

（5）在控制台或其它应用程序启动

````csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Riven;

public class Program
{
    public static void Main(string[] args)
    {
        IConfiguration Configuration = null; // 你应用的配置
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(Configuration);
        services.AddRivenModule<MyAppStartupModule>(Configuration);

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        serviceProvider.UseRivenModule();
    }
}

````
（6）启动

## Demos

AspNetCore App Demo: [link](/samples/WebSample)

Console App Demo: [link](/samples/ConsoleSample)


## Q&A

If you have any questions, you can go to  [Issues](https://github.com/rivenfx/modular/issues) to ask them.


## Reference project

> This project directly or indirectly refers to the following items

- [ABP VNext](https://github.com/abpframework/abp)

## Stargazers over time

[![Stargazers over time](https://starchart.cc/rivenfx/Modular.svg)](https://starchart.cc/rivenfx/Modular)
