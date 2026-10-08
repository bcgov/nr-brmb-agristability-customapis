using System;
using Microsoft.Xrm.Sdk;

namespace Benefit.CustomApis.Plugins;

public class AddNotePlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        var tracing =
            (ITracingService)serviceProvider.GetService(
                typeof(ITracingService));

        tracing.Trace("crb_AddNote called");
    }
}