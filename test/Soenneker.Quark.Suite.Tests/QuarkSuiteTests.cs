using AwesomeAssertions;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.CreditCards;
using Soenneker.Blazor.CreditCards.Abstract;
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

    [Test]
    public void FloatingWindow_contract_exposes_visibility_and_imperative_methods()
    {
        typeof(IFloatingWindow).GetProperty(nameof(IFloatingWindow.Visible))!.PropertyType.Should().Be(typeof(bool));
        typeof(IFloatingWindow).GetProperty(nameof(IFloatingWindow.VisibleChanged)).Should().NotBeNull();
        typeof(IFloatingWindow).GetMethod(nameof(IFloatingWindow.Show)).Should().NotBeNull();
        typeof(IFloatingWindow).GetMethod(nameof(IFloatingWindow.Hide)).Should().NotBeNull();
        typeof(IFloatingWindow).GetMethod(nameof(IFloatingWindow.Toggle)).Should().NotBeNull();
        typeof(IFloatingWindow).GetMethod(nameof(IFloatingWindow.Center)).Should().NotBeNull();
    }

    [Test]
    public void FloatingWindow_registrar_registers_interop()
    {
        var services = new ServiceCollection();

        services.AddQuarkFloatingWindowAsScoped();

        services.Should().Contain(x => x.ServiceType == typeof(IFloatingWindowInterop) && x.ImplementationType == typeof(FloatingWindowInterop));
    }

    [Test]
    public void PaymentCard_registrar_registers_services()
    {
        var services = new ServiceCollection();

        services.AddQuarkPaymentCardAsScoped();

        services.Should().Contain(x => x.ServiceType == typeof(ICardDisplayService) && x.ImplementationType == typeof(CardDisplayService));
        services.Should().Contain(x => x.ServiceType == typeof(ICreditCardsInterop) && x.ImplementationType == typeof(CreditCardsInterop));
    }
}
