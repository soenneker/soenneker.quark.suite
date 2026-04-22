using AwesomeAssertions;
using Soenneker.Tests.Unit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class QuarkSuiteTests : UnitTest
{
    [Test]
    public void Default()
    {

    }

    [Test]
    public void Accordion_contract_exposes_collapsible()
    {
        var property = typeof(IAccordion).GetProperty(nameof(IAccordion.Collapsible));

        property.Should().NotBeNull();
        property!.PropertyType.Should().Be(typeof(bool));
    }
}
