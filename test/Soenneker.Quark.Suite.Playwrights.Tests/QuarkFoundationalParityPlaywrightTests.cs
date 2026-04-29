using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkFoundationalParityPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkFoundationalParityPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Button_demo_preserves_shadcn_size_variant_focus_loading_and_as_child_contracts()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = CaptureConsoleErrors(page);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}buttons",
            static p => p.Locator("[data-slot='button']").First,
            expectedTitle: "Buttons - Quark Suite");

        var defaultButton = page.Locator("[data-slot='button'][data-variant='default']").Filter(new LocatorFilterOptions { HasText = "Button" }).First;
        await Assertions.Expect(defaultButton).ToHaveAttributeAsync("data-slot", "button");
        await Assertions.Expect(defaultButton).ToHaveAttributeAsync("data-variant", "default");
        await Assertions.Expect(defaultButton).ToHaveAttributeAsync("data-size", "default");

        var defaultProbe = await defaultButton.EvaluateAsync<ButtonProbe>(
            @"element => {
                const style = getComputedStyle(element);
                const rect = element.getBoundingClientRect();
                return {
                    display: style.display,
                    alignItems: style.alignItems,
                    justifyContent: style.justifyContent,
                    height: rect.height,
                    borderRadius: parseFloat(style.borderTopLeftRadius),
                    whiteSpace: style.whiteSpace
                };
            }");

        defaultProbe.display.Should().BeOneOf("inline-flex", "flex");
        defaultProbe.alignItems.Should().Be("center");
        defaultProbe.justifyContent.Should().Be("center");
        defaultProbe.height.Should().BeInRange(31, 33);
        defaultProbe.borderRadius.Should().BeGreaterThanOrEqualTo(8);
        defaultProbe.whiteSpace.Should().Be("nowrap");

        await defaultButton.FocusAsync();
        var focusedBoxShadow = await defaultButton.EvaluateAsync<string>("element => getComputedStyle(element).boxShadow");
        focusedBoxShadow.Should().NotBe("none");

        var iconButton = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }).First;
        var iconSize = await iconButton.EvaluateAsync<SizeProbe>(
            @"element => {
                const rect = element.getBoundingClientRect();
                const icon = element.querySelector('svg');
                const iconRect = icon?.getBoundingClientRect();
                return { width: rect.width, height: rect.height, iconWidth: iconRect?.width ?? 0, iconHeight: iconRect?.height ?? 0 };
            }");
        iconSize.width.Should().BeInRange(31, 33);
        iconSize.height.Should().BeInRange(31, 33);
        iconSize.iconWidth.Should().BeInRange(15, 17);
        iconSize.iconHeight.Should().BeInRange(15, 17);

        var loadingButton = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Please wait", Exact = true });
        await Assertions.Expect(loadingButton).ToHaveAttributeAsync("aria-busy", "true");
        await Assertions.Expect(loadingButton.Locator("[role='status']")).ToHaveAttributeAsync("aria-hidden", "true");
        await Assertions.Expect(loadingButton.Locator("[role='status']")).ToBeVisibleAsync();

        var asChildLink = page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Open GitHub", Exact = true });
        await Assertions.Expect(asChildLink).ToHaveAttributeAsync("data-slot", "button");
        await Assertions.Expect(asChildLink).ToHaveAttributeAsync("href", "https://github.com/soenneker");

        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Button_group_demo_preserves_joined_edges_orientation_and_popup_behavior()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = CaptureConsoleErrors(page);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}button-groups",
            static p => p.Locator("[data-slot='button-group']").First,
            expectedTitle: "Button Groups - Quark Suite");

        var messageActions = page.Locator("[data-slot='button-group'][aria-label='Message actions']").First;
        await Assertions.Expect(messageActions).ToHaveAttributeAsync("role", "group");

        var archiveGroup = messageActions.Locator(":scope > [data-slot='button-group']").Filter(new LocatorFilterOptions { HasText = "Archive" }).First;
        var joinedProbe = await archiveGroup.Locator("[data-slot='button']").EvaluateAllAsync<JoinedButtonProbe[]>(
            @"elements => elements.slice(0, 3).map(element => {
                const style = getComputedStyle(element);
                const rect = element.getBoundingClientRect();
                return {
                    left: rect.left,
                    right: rect.right,
                    topLeftRadius: parseFloat(style.borderTopLeftRadius),
                    topRightRadius: parseFloat(style.borderTopRightRadius),
                    borderLeftWidth: style.borderLeftWidth
                };
            })");

        joinedProbe.Length.Should().BeGreaterThanOrEqualTo(2);
        joinedProbe[1].topLeftRadius.Should().Be(0);
        joinedProbe[1].borderLeftWidth.Should().Be("0px");
        joinedProbe[1].left.Should().BeApproximately(joinedProbe[0].right, 1);

        var verticalGroup = page.Locator("[data-slot='button-group'][data-orientation='vertical']").First;
        await Assertions.Expect(verticalGroup).ToHaveAttributeAsync("aria-label", "Media controls");
        var verticalProbe = await verticalGroup.Locator("[data-slot='button']").EvaluateAllAsync<VerticalButtonProbe[]>(
            @"elements => elements.map(element => {
                const style = getComputedStyle(element);
                const rect = element.getBoundingClientRect();
                return {
                    top: rect.top,
                    bottom: rect.bottom,
                    topLeftRadius: parseFloat(style.borderTopLeftRadius),
                    borderTopWidth: style.borderTopWidth
                };
            })");
        verticalProbe.Length.Should().Be(2);
        verticalProbe[1].topLeftRadius.Should().Be(0);
        verticalProbe[1].borderTopWidth.Should().Be("0px");
        verticalProbe[1].top.Should().BeApproximately(verticalProbe[0].bottom, 1);

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "More Options", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "Mark as Read", Exact = true })).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Textarea_demo_preserves_shadcn_field_disabled_invalid_rtl_and_binding_behavior()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = CaptureConsoleErrors(page);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}textareas",
            static p => p.Locator("[data-slot='textarea']").First,
            expectedTitle: "Textarea - Quark Suite");

        var textarea = page.GetByPlaceholder("Type your message here.").First;
        await Assertions.Expect(textarea).ToHaveAttributeAsync("data-slot", "textarea");
        var textareaProbe = await textarea.EvaluateAsync<TextAreaProbe>(
            @"element => {
                const style = getComputedStyle(element);
                const rect = element.getBoundingClientRect();
                return {
                    display: style.display,
                    width: rect.width,
                    minHeight: style.minHeight,
                    resize: style.resize,
                    borderTopWidth: style.borderTopWidth,
                    borderRadius: parseFloat(style.borderTopLeftRadius)
                };
            }");

        textareaProbe.display.Should().Be("flex");
        textareaProbe.width.Should().BeGreaterThan(150);
        textareaProbe.minHeight.Should().Be("64px");
        textareaProbe.resize.Should().NotBe("none");
        textareaProbe.borderTopWidth.Should().Be("1px");
        textareaProbe.borderRadius.Should().BeGreaterThanOrEqualTo(6);

        var disabled = page.Locator("#textarea-disabled");
        await Assertions.Expect(disabled).ToBeDisabledAsync();

        var invalid = page.Locator("#textarea-invalid");
        await Assertions.Expect(invalid).ToHaveAttributeAsync("aria-invalid", "true");

        var rtl = page.Locator("#feedback");
        var rtlDirection = await rtl.EvaluateAsync<string>("element => getComputedStyle(element).direction");
        rtlDirection.Should().Be("rtl");

        var bound = page.GetByPlaceholder("Type your message...");
        await bound.FillAsync("Production ready");
        await Assertions.Expect(page.Locator("text=Value: Production ready")).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Static_foundational_components_render_visible_shadcn_surfaces()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = CaptureConsoleErrors(page);

        await page.GotoAndWaitForReady($"{BaseUrl}badges", static p => p.Locator("[data-slot='badge']").First, expectedTitle: "Badge Component - Quark Suite");
        var badgeProbe = await page.Locator("[data-slot='badge']").First.EvaluateAsync<StaticSurfaceProbe>(
            @"element => {
                const style = getComputedStyle(element);
                const rect = element.getBoundingClientRect();
                return { display: style.display, width: rect.width, height: rect.height, borderRadius: parseFloat(style.borderTopLeftRadius), animationName: style.animationName };
            }");
        badgeProbe.display.Should().BeOneOf("inline-flex", "flex");
        badgeProbe.width.Should().BeGreaterThan(35);
        badgeProbe.height.Should().BeGreaterThan(18);
        badgeProbe.borderRadius.Should().BeGreaterThanOrEqualTo(6);

        await page.GotoAndWaitForReady($"{BaseUrl}skeletons", static p => p.Locator("[data-slot='skeleton']").First, expectedTitle: "Skeleton - Quark Suite");
        var skeletonProbe = await page.Locator("[data-slot='skeleton']").First.EvaluateAsync<StaticSurfaceProbe>(
            @"element => {
                const style = getComputedStyle(element);
                const rect = element.getBoundingClientRect();
                return { display: style.display, width: rect.width, height: rect.height, borderRadius: parseFloat(style.borderTopLeftRadius), animationName: style.animationName };
            }");
        skeletonProbe.width.Should().BeInRange(47, 49);
        skeletonProbe.height.Should().BeInRange(47, 49);
        skeletonProbe.animationName.Should().Contain("pulse");

        await page.GotoAndWaitForReady($"{BaseUrl}spinners", static p => p.Locator("[data-slot='spinner']").First, expectedTitle: "Spinners - Quark Suite");
        var spinnerProbe = await page.Locator("[data-slot='spinner']").First.EvaluateAsync<StaticSurfaceProbe>(
            @"element => {
                const style = getComputedStyle(element);
                const rect = element.getBoundingClientRect();
                return { display: style.display, width: rect.width, height: rect.height, borderRadius: parseFloat(style.borderTopLeftRadius), animationName: style.animationName };
            }");
        spinnerProbe.width.Should().BeGreaterThan(0);
        spinnerProbe.height.Should().BeGreaterThan(0);
        spinnerProbe.animationName.Should().Contain("spin");

        await page.GotoAndWaitForReady($"{BaseUrl}kbd", static p => p.Locator("[data-slot='kbd']").First, expectedTitle: "Kbd - Quark Suite");
        await Assertions.Expect(page.Locator("[data-slot='kbd']").First).ToBeVisibleAsync();

        await page.GotoAndWaitForReady($"{BaseUrl}typography", static p => p.Locator("[data-slot='h1']").First, expectedTitle: "Typography - Quark Suite");
        await Assertions.Expect(page.Locator("[data-slot='h1']").First).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[data-slot='paragraph']").First).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[data-slot='blockquote']").First).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Typography_and_direction_routes_preserve_semantics_and_interactions()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = CaptureConsoleErrors(page);

        await page.GotoAndWaitForReady($"{BaseUrl}anchors", static p => p.GetByRole(AriaRole.Link, new() { Name = "External Link", Exact = true }), expectedTitle: "Anchors - Quark Suite");
        var external = page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "External Link", Exact = true });
        await Assertions.Expect(external).ToHaveAttributeAsync("href", "https://example.com");
        await Assertions.Expect(external).ToHaveAttributeAsync("target", "_blank");

        await page.GotoAndWaitForReady($"{BaseUrl}codes", static p => p.Locator("[data-slot='code']").First, expectedTitle: "Code - Quark Suite");
        await Assertions.Expect(page.Locator("[data-slot='code']").First).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[data-slot='pre-code']").First).ToBeVisibleAsync();

        await page.GotoAndWaitForReady($"{BaseUrl}blockquotes", static p => p.Locator("[data-slot='blockquote']").First, expectedTitle: "Blockquotes - Quark Suite");
        var blockquoteBorder = await page.Locator("[data-slot='blockquote']").First.EvaluateAsync<string>("element => getComputedStyle(element).borderLeftWidth");
        blockquoteBorder.Should().Be("4px");

        await page.GotoAndWaitForReady($"{BaseUrl}paragraphs", static p => p.Locator("[data-slot='paragraph']").First, expectedTitle: "Paragraph - Quark Suite");
        var clickableParagraph = page.GetByTitle("Click me!");
        await Assertions.Expect(clickableParagraph).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[data-slot='paragraph']").Filter(new LocatorFilterOptions { HasText = "Click count:" })).ToBeVisibleAsync();

        await page.GotoAndWaitForReady($"{BaseUrl}small", static p => p.Locator("[data-slot='small']").First, expectedTitle: "Small - Quark Suite");
        await Assertions.Expect(page.Locator("[data-slot='small']").First).ToBeVisibleAsync();

        await page.GotoAndWaitForReady($"{BaseUrl}strong", static p => p.Locator("[data-slot='strong']").First, expectedTitle: "Strong - Quark Suite");
        var strongWeight = await page.Locator("[data-slot='strong']").First.EvaluateAsync<string>("element => getComputedStyle(element).fontWeight");
        int.Parse(strongWeight).Should().BeGreaterThanOrEqualTo(600);

        await page.GotoAndWaitForReady($"{BaseUrl}directions", static p => p.Locator("[data-slot='direction-provider']").First, expectedTitle: "Direction - Quark Suite");
        var directionCard = page.Locator("[data-slot='card'][dir='rtl']").First;
        await Assertions.Expect(directionCard).ToBeVisibleAsync();

        await page.Locator("[data-name='language-selector']").ClickAsync();
        await page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "English", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-slot='card'][dir='ltr']").First).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
    }

    private static List<string> CaptureConsoleErrors(IPage page)
    {
        var consoleErrors = new List<string>();
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        return consoleErrors;
    }

    private sealed class ButtonProbe
    {
        public string? display { get; set; }
        public string? alignItems { get; set; }
        public string? justifyContent { get; set; }
        public double height { get; set; }
        public double borderRadius { get; set; }
        public string? whiteSpace { get; set; }
    }

    private sealed class SizeProbe
    {
        public double width { get; set; }
        public double height { get; set; }
        public double iconWidth { get; set; }
        public double iconHeight { get; set; }
    }

    private sealed class JoinedButtonProbe
    {
        public double left { get; set; }
        public double right { get; set; }
        public double topLeftRadius { get; set; }
        public double topRightRadius { get; set; }
        public string? borderLeftWidth { get; set; }
    }

    private sealed class VerticalButtonProbe
    {
        public double top { get; set; }
        public double bottom { get; set; }
        public double topLeftRadius { get; set; }
        public string? borderTopWidth { get; set; }
    }

    private sealed class TextAreaProbe
    {
        public string? display { get; set; }
        public double width { get; set; }
        public string? minHeight { get; set; }
        public string? resize { get; set; }
        public string? borderTopWidth { get; set; }
        public double borderRadius { get; set; }
    }

    private sealed class StaticSurfaceProbe
    {
        public string? display { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double borderRadius { get; set; }
        public string? animationName { get; set; }
    }
}
