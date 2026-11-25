using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkDemoParityPlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    public async ValueTask Dialog_examples_position_actions_and_preserve_the_share_url(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/dialog",
            static p => p.GetByRole(AriaRole.Button, new() { Name = "Share", Exact = true }));
        await page.GetByRole(AriaRole.Button, new() { Name = "Share", Exact = true }).ClickAsync();
        var dialog = page.GetByRole(AriaRole.Dialog);
        await Assertions.Expect(dialog.GetByRole(AriaRole.Textbox)).Not.ToBeEditableAsync();
        var footer = dialog.Locator("[data-slot='dialog-footer']");
        var footerBox = await footer.BoundingBoxAsync();
        var buttonBox = await footer.GetByRole(AriaRole.Button).BoundingBoxAsync();
        await Assert.That(buttonBox!.X + buttonBox.Width).IsLessThan(footerBox!.X + footerBox.Width / 2);
        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(dialog).ToHaveCountAsync(0);

        await page.GetByRole(AriaRole.Button, new() { Name = "فتح الحوار" }).ClickAsync();
        await Assertions.Expect(dialog).ToHaveCSSAsync("direction", "rtl");
        var close = await dialog.Locator("button[data-slot='dialog-close']").BoundingBoxAsync();
        var cancel = await dialog.GetByRole(AriaRole.Button, new() { Name = "إلغاء" }).BoundingBoxAsync();
        var save = await dialog.GetByRole(AriaRole.Button, new() { Name = "حفظ التغييرات" }).BoundingBoxAsync();
        await Assert.That(close!.X).IsLessThan(cancel!.X);
        await Assert.That(save!.X).IsLessThan(cancel.X);
    }

    [Test]
    public async ValueTask Empty_link_uses_its_explicit_text_color(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/empty", static p => p.Locator("[data-slot='empty']").First);
        var example = page.Locator("[data-slot='empty']").First;
        var muted = await example.Locator("[data-slot='empty-description']").EvaluateAsync<string>("e => getComputedStyle(e).color");
        await Assertions.Expect(example.GetByRole(AriaRole.Link, new() { Name = "Learn More" })).ToHaveCSSAsync("color", muted);
    }

    [Test]
    public async ValueTask Dropdown_submenu_restores_focus_to_its_parent_trigger(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/dropdown-menu", static p => p.GetByRole(AriaRole.Heading, new() { Name = "Submenu", Exact = true }));
        var submenu = page.Locator("section").Filter(new() { Has = page.GetByRole(AriaRole.Heading, new() { Name = "Submenu", Exact = true }) }).Last;
        await submenu.GetByRole(AriaRole.Button, new() { Name = "Open", Exact = true }).PressAsync("Enter");
        var invite = page.GetByRole(AriaRole.Menuitem, new() { Name = "Invite users" });
        await invite.PressAsync("ArrowRight");
        var more = page.GetByRole(AriaRole.Menuitem, new() { Name = "More options" });
        await more.PressAsync("ArrowRight");
        var slack = page.GetByRole(AriaRole.Menuitem, new() { Name = "Slack" });
        await Assertions.Expect(slack).ToBeVisibleAsync();
        await slack.PressAsync("ArrowLeft");
        await Assertions.Expect(more).ToBeFocusedAsync();
    }

    [Test]
    public async ValueTask Dropdown_icons_marks_logout_as_destructive(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/dropdown-menu", static p => p.GetByRole(AriaRole.Heading, new() { Name = "Icons", Exact = true }));

        var icons = page.Locator("section").Filter(new() { Has = page.GetByRole(AriaRole.Heading, new() { Name = "Icons", Exact = true }) }).Last;
        await icons.GetByRole(AriaRole.Button, new() { Name = "Open", Exact = true }).PressAsync("Enter");
        await Assertions.Expect(page.GetByRole(AriaRole.Menuitem, new() { Name = "Log out" })).ToHaveAttributeAsync("data-variant", "destructive");
    }
}
