using Riven.Modular;
using SampleCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace PluginB
{
    [DependsOn(
        typeof(TestModuleB)
        )]
    public class PluginBModule : AppModule
    {
    }
}
