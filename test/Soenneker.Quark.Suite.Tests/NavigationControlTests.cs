using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Navigation_controls_follow_the_active_route()
    {
        var cut = Render<BottomNavigation>(p => p.AddChildContent<BottomNavigationItem>(item =>
            item.Add(c => c.Href, "/inbox").Add(c => c.Label, "Inbox")));
        cut.Find("nav").GetAttribute("aria-label").Should().Be("Primary navigation");
        cut.Find("a").HasAttribute("aria-current").Should().BeFalse();
        Services.GetRequiredService<NavigationManager>().NavigateTo("/inbox");
        cut.WaitForAssertion(() => cut.Find("a").GetAttribute("aria-current").Should().Be("page"));
    }

    [Test]
    public void Navigation_controls_speed_dial_exposes_a_named_menu_trigger()
    {
        var cut = Render<SpeedDial>(p => p.Add(c => c.Label, "Create").AddChildContent<DropdownItem>(
            item => item.AddChildContent("New task")));
        var trigger = cut.Find("button");
        trigger.GetAttribute("aria-label").Should().Be("Create");
        trigger.HasAttribute("aria-haspopup").Should().BeTrue();
        trigger.GetAttribute("type").Should().Be("button");
    }

    [Test]
    public async Task Navigation_controls_scroll_button_invokes_page_scrolling()
    {
        var interop = (FakeScrollspyInterop)Services.GetRequiredService<IScrollspyInterop>();
        var cut = Render<ScrollToTop>(p => p.Add(c => c.Smooth, false));
        await cut.Find("button[aria-label='Scroll to top']").ClickAsync();
        interop.LastScrollSmooth.Should().BeFalse();
    }
}
