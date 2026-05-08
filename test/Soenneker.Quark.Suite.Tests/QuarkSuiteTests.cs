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

    [Test]
    public void Default_dark_theme_uses_current_shadcn_card_surface_tokens()
    {
        var tokens = new Theme().Tokens.Dark;

        tokens.Card.Should().Be("#171717");
        tokens.Popover.Should().Be("#171717");
        tokens.Destructive.Should().Be("#ff6568");
        tokens.DestructiveForeground.Should().Be("#df2225");
    }
}
