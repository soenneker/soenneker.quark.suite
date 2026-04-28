using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkValidationPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkValidationPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Validation_demo_wires_errors_aria_pattern_and_success_feedback()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, error) => pageErrors.Add(error);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}validation",
            static p => p.Locator("#requiredField"),
            expectedTitle: "Validation - Quark Suite");

        var required = page.Locator("#requiredField");
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Validate Form" }).First.ClickAsync();

        var requiredError = page.Locator("[data-slot='field-error'][role='alert']").Filter(new LocatorFilterOptions { HasText = "This field is required." }).First;
        await Assertions.Expect(requiredError).ToBeVisibleAsync();
        await Assertions.Expect(required).ToHaveAttributeAsync("aria-invalid", "true");

        var describedBy = await required.GetAttributeAsync("aria-describedby");
        describedBy.Should().NotBeNullOrWhiteSpace();
        describedBy!.Split(' ').Should().Contain(await requiredError.GetAttributeAsync("id"));

        await required.FillAsync("Ada");
        await page.Locator("#emailField").FillAsync("ada@example.com");
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Validate Form" }).First.ClickAsync();
        await Assertions.Expect(requiredError).ToBeHiddenAsync();
        await Assertions.Expect(required).Not.ToHaveAttributeAsync("aria-invalid", "true");

        await page.Locator("#zipCode").FillAsync("abc");
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Validate Patterns" }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-slot='field-error']").Filter(new LocatorFilterOptions { HasText = "Please enter a valid US ZIP code" })).ToBeVisibleAsync();

        await page.Locator("#successField").FillAsync("success");
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Validate Success" }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-slot='validation-success']").Filter(new LocatorFilterOptions { HasText = "Looks good." })).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
