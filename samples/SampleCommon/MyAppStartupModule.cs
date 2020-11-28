using Riven;
using Riven.Modular;

namespace SampleCommon
{
    [DependsOn(
        typeof(TestModuleA)
        )]
    public class MyAppStartupModule : TestBaseModule
    {
        public override void OnPreConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务之前
            base.OnPreConfigureServices(context);
        }

        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务
            base.OnConfigureServices(context);
        }

        public override void OnPostConfigureServices(ServiceConfigurationContext context)
        {
            // 注册服务之后
            base.OnPostConfigureServices(context);
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化之前
            base.OnPreApplicationInitialization(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化
            base.OnApplicationInitialization(context);
        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            // 应用初始化之后
            base.OnPostApplicationInitialization(context);
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            // 应用停止
            base.OnApplicationShutdown(context);
        }
    }
}

