using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

[Collection("Collection")]
public sealed class QuarkSuiteTests : FixturedUnitTest
{
    public QuarkSuiteTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
