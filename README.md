# Riven.Modular

Language: 中文

`Riven.Modular` 是一个模块化实现的基础库。你可以使用它来创建模块化的应用。



[![Latest version](https://img.shields.io/nuget/v/Riven.Modular.svg)](https://www.nuget.org/packages/Riven.Modular/)

[![Build Status](https://dev.azure.com/rivenfx/Modular/_apis/build/status/rivenfx.Modular?branchName=master)](https://dev.azure.com/rivenfx/Modular/_build/latest?definitionId=2&branchName=master)

## 1.快速开始

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

ASPNET Core使用Demo: [link](/samples/WebSample)

控制台使用 Demo: [link](/samples/ConsoleSample)

## 2.高级

待补充

## 3.配置

待补充

## 4.Q&A

如果你遇到问题，你可以到 [Issues](https://github.com/rivenfx/modular/issues)提问。

## 5.参考项目

> 本项目直接或间接指下列项目

- [ABP VNext](https://github.com/abpframework/abp)

