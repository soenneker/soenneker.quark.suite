using AwesomeAssertions;
using Bunit;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void PaymentCard_detects_visa_from_card_number()
    {
        var cut = Render<PaymentCard>(parameters => parameters
            .Add(p => p.CardNumber, "4111111111111111")
            .Add(p => p.CardHolderName, "Jordan Lee")
            .Add(p => p.ExpiryDate, "08/29")
            .Add(p => p.Cvc, "123"));

        cut.Find(".quark-payment-card-container").Should().NotBeNull();
        var card = cut.Find(".credit-card");

        card.ClassList.Should().Contain("card--visa");
        card.ClassList.Should().Contain("card--standard");
        cut.Markup.Should().Contain("JORDAN LEE");
        cut.Find(".card__expiry").TextContent.Should().Be("08/29");
    }

    [Test]
    public async Task PaymentCard_set_last_four_uses_supplied_card_metadata()
    {
        var cut = Render<PaymentCard>();

        await cut.InvokeAsync(() => cut.Instance.SetLast4("4242", "visa", "visa", "standard").AsTask());

        var card = cut.Find(".credit-card");

        card.ClassList.Should().Contain("card--visa");
        card.ClassList.Should().Contain("card--standard");
        cut.Markup.Should().Contain("4242");
    }
}
