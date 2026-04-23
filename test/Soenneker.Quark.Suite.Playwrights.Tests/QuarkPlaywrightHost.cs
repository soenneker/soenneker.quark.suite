using Soenneker.Playwrights.TestEnvironment.Options;
using Soenneker.Playwrights.TestHosts;
using System.IO;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

public sealed class QuarkPlaywrightHost : PlaywrightHostedTestHost
{

    protected override PlaywrightTestHostOptions CreateOptions()
    {
        return new PlaywrightTestHostOptions
        {
            SolutionFileName = "Soenneker.Quark.Suite.slnx",
            ProjectRelativePath = Path.Combine("test", "Soenneker.Quark.Suite.Demo", "Soenneker.Quark.Suite.Demo.csproj"),
            ApplicationName = "Quark demo",
            Restore = false,
            BuildConfiguration = "Debug",
            ReuseBrowserContextAcrossSessions = false,
            ReusePageAcrossSessions = false
        };
    }

}