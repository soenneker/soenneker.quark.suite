using Soenneker.Tests.FixturedUnit;
using AwesomeAssertions;
using Xunit;
using System.Reflection;

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

    [Fact]
    public void Accordion_contract_exposes_collapsible()
    {
        PropertyInfo? property = typeof(IAccordion).GetProperty(nameof(IAccordion.Collapsible));

        property.Should().NotBeNull();
        property!.PropertyType.Should().Be(typeof(bool));
    }
}
