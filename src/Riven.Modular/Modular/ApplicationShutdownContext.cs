using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Riven.Modular
{
    public class ApplicationShutdownContext
    {
        public IServiceProvider ServiceProvider { get; }

        public ApplicationShutdownContext([NotNull] IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
