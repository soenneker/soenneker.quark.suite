using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[CollectionDefinition("Collection")]
public sealed class Collection : ICollectionFixture<QuarkPlaywrightFixture>
{
}
