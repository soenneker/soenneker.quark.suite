using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkFieldPlaywrightTests : QuarkPlaywrightTest
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

    [Test]
    public async ValueTask Form_item_demo_wires_generated_accessible_relationships()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}fields",
            static p => p.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email", Exact = true }),
            expectedTitle: "Fields - Quark Suite");

        var demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "shadcn-style form item with generated label" }).First;
        var label = demoSection.Locator("[data-slot='form-label']");
        var input = demoSection.GetByRole(AriaRole.Textbox, new LocatorGetByRoleOptions { Name = "Email", Exact = true });
        var description = demoSection.Locator("[data-slot='form-description']");
        var message = demoSection.Locator("[data-slot='form-message']");

        await Assertions.Expect(input).ToBeVisibleAsync();
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-invalid", "true");
        await Assertions.Expect(label).ToHaveAttributeAsync("data-error", "true");
        await Assertions.Expect(message).ToHaveTextAsync("Email is required.");

        var inputId = await input.GetAttributeAsync("id");
        var labelFor = await label.GetAttributeAsync("for");
        var descriptionId = await description.GetAttributeAsync("id");
        var messageId = await message.GetAttributeAsync("id");
        var describedBy = await input.GetAttributeAsync("aria-describedby");

        inputId.Should().NotBeNullOrWhiteSpace();
        labelFor.Should().Be(inputId);
        describedBy.Should().Contain(descriptionId);
        describedBy.Should().Contain(messageId);

        await demoSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle invalid state", Exact = true }).ClickAsync();

        await Assertions.Expect(label).ToHaveAttributeAsync("data-error", "false");
        (await input.GetAttributeAsync("aria-invalid")).Should().BeNull();
        (await input.GetAttributeAsync("aria-describedby")).Should().Contain(descriptionId).And.NotContain(messageId);

        consoleErrors.Should().BeEmpty();
        sawPageError.Should().BeFalse();
    }
}
