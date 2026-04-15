using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[CollectionDefinition("Collection", DisableParallelization = true)]
public sealed class Collection : ICollectionFixture<QuarkPlaywrightFixture>
{
}
