using Riven.Modular;
using SampleCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace PluginA
{
    [DependsOn(
        typeof(TestModuleA)
        )]
    public class PluginAModule : TestBaseModule
    {
    }
}
