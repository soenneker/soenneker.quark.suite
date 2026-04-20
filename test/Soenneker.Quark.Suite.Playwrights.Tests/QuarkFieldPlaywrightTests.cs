using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkFieldPlaywrightTests : PlaywrightUnitTest
{
    public QuarkFieldPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Field_demo_renders_a_full_width_payment_form_without_collapsing()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}fields",
            static p => p.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Name on Card", Exact = true }),
            expectedTitle: "Fields - Quark Suite");

        var demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Multi-section payment form with field sets, selects, and horizontal actions." }).First;
        var form = page.Locator("form").First;
        var cardName = page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Name on Card", Exact = true });
        var cardNumber = page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Card Number", Exact = true });
        var comments = page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Comments", Exact = true });

        await Assertions.Expect(cardName).ToBeVisibleAsync();
        await Assertions.Expect(cardNumber).ToBeVisibleAsync();
        await Assertions.Expect(comments).ToBeVisibleAsync();

        var sectionBox = await demoSection.BoundingBoxAsync();
        var formBox = await form.BoundingBoxAsync();
        var cardNameBox = await cardName.BoundingBoxAsync();
        var cardNumberBox = await cardNumber.BoundingBoxAsync();
        var commentsBox = await comments.BoundingBoxAsync();

        Assert.NotNull(sectionBox);
        Assert.NotNull(formBox);
        Assert.NotNull(cardNameBox);
        Assert.NotNull(cardNumberBox);
        Assert.NotNull(commentsBox);

        Assert.InRange(formBox.Width, 400, 520);
        Assert.True(formBox.Width < sectionBox.Width, $"Expected the payment form to remain medium-width inside the preview, but measured form={formBox.Width} section={sectionBox.Width}.");
        Assert.True(cardNameBox.Width >= 300, $"Expected the card name input to render at usable width, but measured {cardNameBox.Width}.");
        Assert.True(cardNumberBox.Width >= 300, $"Expected the card number input to render at usable width, but measured {cardNumberBox.Width}.");
        Assert.True(commentsBox.Width >= 300, $"Expected the comments textarea to render at usable width, but measured {commentsBox.Width}.");
    }
}
