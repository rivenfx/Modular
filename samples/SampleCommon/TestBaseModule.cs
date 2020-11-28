using Riven.Modular;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleCommon
{
    public class TestBaseModule : AppModule
    {
        protected Type MyType => this.GetType();

        public override void OnPreConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务之前
            Console.WriteLine($"{nameof(OnPreConfigureServices)}  {MyType.Name}");
        }

        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务
            Console.WriteLine($"{nameof(OnConfigureServices)}  {MyType.Name}");
        }

        public override void OnPostConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务之后
            Console.WriteLine($"{nameof(OnPostConfigureServices)}  {MyType.Name}");
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化之前
            Console.WriteLine($"{nameof(OnPreApplicationInitialization)}  {MyType.Name}");
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化
            Console.WriteLine($"{nameof(OnApplicationInitialization)}  {MyType.Name}");
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化之后
            Console.WriteLine($"{nameof(OnPostApplicationInitialization)}  {MyType.Name}");
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            // 应用停止
            Console.WriteLine($"{nameof(OnApplicationShutdown)}  {MyType.Name}");
        }
    }
}
