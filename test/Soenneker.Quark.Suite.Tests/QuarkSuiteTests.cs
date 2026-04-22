using AwesomeAssertions;
using Soenneker.TestHosts.Unit;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Quark.Suite.Tests;

[ClassDataSource<UnitTestHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSuiteTests(UnitTestHost host) : HostedUnitTest(host)
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
