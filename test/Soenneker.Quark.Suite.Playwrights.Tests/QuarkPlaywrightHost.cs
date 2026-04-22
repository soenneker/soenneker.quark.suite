using Soenneker.Playwrights.TestHosts;
using System.IO;
using Soenneker.Playwrights.TestEnvironment.Options;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

public sealed class QuarkPlaywrightHost : PlaywrightHostedTestHost
{
    protected override PlaywrightFixtureOptions CreateOptions()
    {
        return new PlaywrightFixtureOptions
        {
            SolutionFileName = "Soenneker.Quark.Suite.slnx",
            ProjectRelativePath = Path.Combine("test", "Soenneker.Quark.Suite.Demo", "Soenneker.Quark.Suite.Demo.csproj"),
            ApplicationName = "Quark demo",
            Restore = false,
            BuildConfiguration = "Release",
            ReuseBrowserContextAcrossSessions = false,
            ReusePageAcrossSessions = false
        };
    }
}
