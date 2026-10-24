using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
namespace Soenneker.Quark.Suite.Playwrights.Tests;
[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkWorkflowPlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    public async ValueTask Editor_formats_text_and_sanitizes_active_content(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/rich-text-editor", p => p.Locator("[contenteditable='true']"));
        var editor = page.GetByRole(AriaRole.Textbox, new() { Name = "Message", Exact = true });
        await editor.FillAsync("A formatted update");
        await editor.PressAsync("ControlOrMeta+a");
        await page.GetByRole(AriaRole.Button, new() { Name = "Bold", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[role='document'] strong, [role='document'] b")).ToHaveTextAsync("A formatted update");
        var safe = await page.EvaluateAsync<bool>("""
            async () => {
                const { sanitize } = await import('/_content/Soenneker.Quark.Suite/js/richtextinterop.js');
                const html = sanitize('<p onclick="evil()">safe<script>evil()</script><img src=x onerror="evil()"><a href="javascript:evil()">bad</a><svg onload="evil()"></svg><a href="https://example.com">good</a></p>');
                const node = document.createElement('div'); node.innerHTML = html;
                return !node.querySelector('script,img,svg,[onclick],[onerror]') && !node.querySelector('a').hasAttribute('href') && node.querySelectorAll('a')[1].getAttribute('href') === 'https://example.com';
            }
            """);
        await Assert.That(safe).IsTrue();
    }

    [Test]
    public async ValueTask Saved_view_restores_filters_after_browser_reload(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/datatables", p => p.Locator("[data-slot='datatable-data']").First);
        var grid = page.Locator("[data-slot='datatable-data']").First;
        await grid.GetByRole(AriaRole.Textbox, new() { Name = "Search", Exact = true }).First.FillAsync("Contact 75");
        await Assertions.Expect(grid.Locator("tbody tr")).ToHaveCountAsync(1);
        await grid.GetByRole(AriaRole.Textbox, new() { Name = "Name", Exact = true }).FillAsync("Last contact");
        await grid.GetByRole(AriaRole.Button, new() { Name = "Save view", Exact = true }).ClickAsync();
        await page.ReloadAsync();
        var views = grid.GetByRole(AriaRole.Combobox, new() { Name = "Saved views", Exact = true });
        await Assertions.Expect(views.Locator("option")).ToHaveCountAsync(2);
        await views.SelectOptionAsync(new SelectOptionValue { Label = "Last contact" });
        await Assertions.Expect(grid.Locator("tbody tr")).ToHaveCountAsync(1);
        await Assertions.Expect(grid.Locator("tbody")).ToContainTextAsync("Contact 75");
        await grid.GetByRole(AriaRole.Button, new() { Name = "Expand", Exact = true }).ClickAsync();
        await Assertions.Expect(grid.Locator("tbody")).ToContainTextAsync("works at");
        await grid.GetByRole(AriaRole.Button, new() { Name = "Edit Company", Exact = true }).ClickAsync();
        await grid.GetByRole(AriaRole.Textbox, new() { Name = "Company", Exact = true }).FillAsync("Updated company");
        await grid.GetByRole(AriaRole.Button, new() { Name = "Save", Exact = true }).ClickAsync();
        await Assertions.Expect(grid.Locator("tbody")).ToContainTextAsync("Updated company");
    }

    [Test]
    public async ValueTask Searchable_select_remote_supports_keyboard_selection_and_resolves_saved_values(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/searchable-select", p => p.GetByRole(AriaRole.Combobox, new() { Name = "Contacts", Exact = true }));
        await Assertions.Expect(page.Locator("[data-slot='combobox']").Last).ToContainTextAsync("Contact 75");
        var input = page.GetByRole(AriaRole.Combobox, new() { Name = "Contacts", Exact = true });
        await input.FillAsync("Contact 120");
        await Assertions.Expect(page.GetByRole(AriaRole.Option)).ToHaveCountAsync(1);
        await input.ClickAsync();
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-expanded", "true");
        await input.PressAsync("Enter");
        await Assertions.Expect(page.Locator("[data-slot='combobox-chip']").Filter(new() { HasText = "Contact 120" })).ToBeVisibleAsync();
        await input.PressAsync("Escape");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-expanded", "false");
        await input.ClickAsync();
        await input.FillAsync("");
        await Assertions.Expect(input).ToHaveValueAsync("");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(page.GetByRole(AriaRole.Option)).ToHaveCountAsync(10);
        await input.PressAsync("End");
        await Assertions.Expect(page.GetByRole(AriaRole.Option).Last).ToHaveAttributeAsync("data-highlighted", "");
        await Assertions.Expect(page.GetByRole(AriaRole.Option).Last).ToBeInViewportAsync();
        await input.PressAsync("ArrowDown");
        await Assertions.Expect(page.GetByRole(AriaRole.Option)).ToHaveCountAsync(20);
        await Assertions.Expect(page.GetByRole(AriaRole.Option).Nth(10)).ToHaveAttributeAsync("data-highlighted", "");
    }

    [Test]
    public async ValueTask Scheduler_creates_and_edits_an_appointment(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/scheduler", p => p.Locator("[data-slot='scheduler']"));
        await page.GetByRole(AriaRole.Button, new() { Name = "New event", Exact = true }).ClickAsync();
        await page.GetByLabel("Title", new() { Exact = true }).FillAsync("Planning session");
        await page.GetByRole(AriaRole.Button, new() { Name = "Save", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-slot='scheduler']")).ToContainTextAsync("Planning session");
        await page.GetByRole(AriaRole.Button, new() { NameRegex = new System.Text.RegularExpressions.Regex(@"^\d.*Planning session$") }).ClickAsync();
        await page.GetByLabel("Title", new() { Exact = true }).FillAsync("Updated session");
        await page.GetByRole(AriaRole.Button, new() { Name = "Save", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-slot='scheduler']")).ToContainTextAsync("Updated session");
    }

    [Test]
    public async ValueTask Notifications_and_localization_respond_to_user_actions(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/notification-center", p => p.Locator("[data-slot='notification-center'] article").First);
        await page.GetByRole(AriaRole.Button, new() { Name = "Mark all read", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-slot='notification-center'] [role='status']")).ToHaveTextAsync("0 Unread");
        await page.GetByRole(AriaRole.Button, new() { Name = "Load more", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-slot='notification-center'] article")).ToHaveCountAsync(10);
        await page.GotoAndWaitForReady($"{BaseUrl}components/localization", p => p.Locator("[data-slot='filter-builder']"));
        await page.GetByRole(AriaRole.Button, new() { Name = "Español", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Añadir filtro", Exact = true })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask DataTable_virtualizes_rows_and_supports_resize_and_mixed_selection(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/datatables", p => p.GetByRole(AriaRole.Table, new() { Name = "Virtual contacts", Exact = true }));
        var grid = page.Locator("[data-slot='datatable-data']").First;
        await grid.GetByRole(AriaRole.Checkbox, new() { Name = "Select 1", Exact = true }).CheckAsync();
        await Assertions.Expect(grid.GetByRole(AriaRole.Checkbox, new() { Name = "Select all rows", Exact = true })).ToHaveJSPropertyAsync("indeterminate", true);
        var handle = grid.GetByRole(AriaRole.Separator, new() { Name = "Name Width", Exact = true });
        await handle.PressAsync("ArrowRight");
        await Assertions.Expect(handle).ToHaveAttributeAsync("aria-valuenow", "188");
        var table = page.GetByRole(AriaRole.Table, new() { Name = "Virtual contacts", Exact = true });
        await Assertions.Expect(table.Locator("tr[data-virtual-key]").First).ToBeVisibleAsync();
        await Assert.That(await table.Locator("tr[data-virtual-key]").CountAsync()).IsLessThan(100);
        for (var i = 0; i < 3; i++)
        {
            await table.EvaluateAsync("table => table.parentElement.scrollTop = table.parentElement.scrollHeight");
            await Assertions.Expect(table.Locator($"tr[data-virtual-key='{(i + 1) * 25 + 1}']")).ToBeAttachedAsync();
        }
        await table.Locator("xpath=ancestor::*[@data-slot='datatable-data']").GetByRole(AriaRole.Textbox, new() { Name = "Search", Exact = true }).FillAsync("Virtual contact 1000");
        await Assertions.Expect(table.GetByText("Virtual contact 1000", new() { Exact = true })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Editor_tracks_formatting_undo_and_toolbar_keyboard_navigation(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/rich-text-editor", p => p.Locator("[contenteditable='true']"));
        var editor = page.GetByRole(AriaRole.Textbox, new() { Name = "Message", Exact = true });
        await editor.FillAsync("Hello"); await editor.PressAsync("ControlOrMeta+a");
        var bold = page.GetByRole(AriaRole.Button, new() { Name = "Bold", Exact = true });
        await bold.ClickAsync();
        await Assertions.Expect(bold).ToHaveAttributeAsync("aria-pressed", "true");
        await page.GetByRole(AriaRole.Button, new() { Name = "Undo", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[contenteditable='true'] b, [contenteditable='true'] strong")).ToHaveCountAsync(0);
        await bold.FocusAsync(); await bold.PressAsync("ArrowRight");
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Italic", Exact = true })).ToBeFocusedAsync();
    }

    [Test]
    public async ValueTask Editor_edits_and_removes_existing_links(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/rich-text-editor", p => p.Locator("[contenteditable='true']"));
        var editor = page.GetByRole(AriaRole.Textbox, new() { Name = "Message", Exact = true });
        await editor.FillAsync("Website"); await editor.PressAsync("ControlOrMeta+a");
        await page.GetByRole(AriaRole.Button, new() { Name = "Link", Exact = true }).ClickAsync();
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Link", Exact = true }).FillAsync("https://example.com/old");
        await page.GetByRole(AriaRole.Button, new() { Name = "Insert", Exact = true }).ClickAsync();
        await Assertions.Expect(editor.Locator("a")).ToHaveAttributeAsync("href", "https://example.com/old");
        await editor.Locator("a").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Link", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Textbox, new() { Name = "Link", Exact = true })).ToHaveValueAsync("https://example.com/old");
        await page.GetByRole(AriaRole.Textbox, new() { Name = "Link", Exact = true }).FillAsync("https://example.com/new");
        await page.GetByRole(AriaRole.Button, new() { Name = "Insert", Exact = true }).ClickAsync();
        await Assertions.Expect(editor.Locator("a")).ToHaveAttributeAsync("href", "https://example.com/new");
        await editor.Locator("a").ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Remove link", Exact = true }).ClickAsync();
        await Assertions.Expect(editor.Locator("a")).ToHaveCountAsync(0);
        await Assertions.Expect(editor).ToHaveTextAsync("Website");
    }
}
