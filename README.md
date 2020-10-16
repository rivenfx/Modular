# Riven.Modular

Language: 中文

`Riven.Modular` 是一个模块化实现的基础库。你可以使用它来创建模块化的应用。


## 996.ICU LICENSE
[![LICENSE](https://img.shields.io/badge/license-Anti%20996-blue.svg)](https://github.com/996icu/996.ICU/blob/master/LICENSE)

请注意：一旦使用本开源项目以及引用了本项目或包含本项目代码的公司因为违反劳动法（包括但不限定非法裁员、超时用工、雇佣童工等）在任何法律诉讼中败诉的，项目作者有权利追讨本项目的使用费，或者直接不允许使用任何包含本项目的源代码！


## Nuget Packages


[![Build Status](https://dev.azure.com/rivenfx/RivenFx/_apis/build/status/rivenfx.Modular?branchName=master)](https://dev.azure.com/rivenfx/RivenFx/_build/latest?definitionId=4&branchName=master)


|Package|Status|
|:------|:-----:|
|Riven.Modular|[![NuGet version](https://badge.fury.io/nu/Riven.Modular.svg)](https://www.nuget.org/packages/Riven.Modular/)|





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

