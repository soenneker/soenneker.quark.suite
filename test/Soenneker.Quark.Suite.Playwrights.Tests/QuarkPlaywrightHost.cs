using Soenneker.Playwrights.TestEnvironment.Options;
using Soenneker.Playwrights.TestHosts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

public sealed class QuarkPlaywrightHost : PlaywrightHostedTestHost
{
    public override async ValueTask DisposeAsync()
    {
        try
        {
            await base.DisposeAsync();
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName == "System.Threading.SemaphoreSlim")
        {
        }
    }

    protected override PlaywrightTestHostOptions CreateOptions()
    {
        return new PlaywrightTestHostOptions
        {
            SolutionFileName = "Soenneker.Quark.Suite.slnx",
            ProjectRelativePath = Path.Combine("test", "Soenneker.Quark.Suite.Demo", "Soenneker.Quark.Suite.Demo.csproj"),
            ApplicationName = "Quark demo",
            Restore = false,
            BuildConfiguration = "DebugDemo",
            ReuseBrowserContextAcrossSessions = false,
            ReusePageAcrossSessions = false
        };
    }

}
