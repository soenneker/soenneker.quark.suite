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

        tokens.Card.Should().Be("oklch(0.205 0 0)");
        tokens.Popover.Should().Be("oklch(0.205 0 0)");
        tokens.Destructive.Should().Be("oklch(0.704 0.191 22.216)");
        tokens.DestructiveForeground.Should().Be("oklch(0.704 0.191 22.216)");
    }
}
