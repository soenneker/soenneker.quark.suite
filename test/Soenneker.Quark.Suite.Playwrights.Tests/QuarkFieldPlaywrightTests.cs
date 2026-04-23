using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkFieldPlaywrightTests : PlaywrightUnitTest
{
    public QuarkFieldPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
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

        (sectionBox).Should().NotBeNull();
        (formBox).Should().NotBeNull();
        (cardNameBox).Should().NotBeNull();
        (cardNumberBox).Should().NotBeNull();
        (commentsBox).Should().NotBeNull();

        (formBox.Width).Should().BeInRange(400, 520);
        (formBox.Width < sectionBox.Width).Should().BeTrue();
        (cardNameBox.Width >= 300).Should().BeTrue();
        (cardNumberBox.Width >= 300).Should().BeTrue();
        (commentsBox.Width >= 300).Should().BeTrue();
    }
}
