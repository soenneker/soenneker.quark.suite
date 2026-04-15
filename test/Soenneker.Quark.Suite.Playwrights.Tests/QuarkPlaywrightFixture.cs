using Soenneker.Playwrights.Fixtures;
using System.IO;
using Soenneker.Playwrights.TestEnvironment.Options;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

public sealed class QuarkPlaywrightFixture : PlaywrightFixture
{
    protected override PlaywrightFixtureOptions CreateOptions()
    {
        return new PlaywrightFixtureOptions
        {
            SolutionFileName = "Soenneker.Quark.Suite.slnx",
            ProjectRelativePath = Path.Combine("test", "Soenneker.Quark.Suite.Demo", "Soenneker.Quark.Suite.Demo.csproj"),
            ApplicationName = "Quark demo"
        };
    }
}
