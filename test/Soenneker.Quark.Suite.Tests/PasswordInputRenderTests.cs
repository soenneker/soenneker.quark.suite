using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void PasswordInput_renders_password_field_and_toggle()
    {
        var cut = Render<PasswordInput>(parameters => parameters
            .Add(p => p.Id, "password")
            .Add(p => p.Placeholder, "Enter your password")
            .Add(p => p.Value, "secret"));

        var root = cut.Find("[data-slot='password-input']");
        root.ClassList.Should().Contain("inline-flex");
        root.ClassList.Should().Contain("gap-2");

        var input = cut.Find("input");
        input.GetAttribute("id").Should().Be("password");
        input.GetAttribute("type").Should().Be("password");
        input.GetAttribute("placeholder").Should().Be("Enter your password");
        input.GetAttribute("autocomplete").Should().Be("current-password");
        input.ClassList.Should().Contain("hide-password-toggle");
        input.ClassList.Should().Contain("flex-1");
        input.ClassList.Should().NotContain("pr-10");

        var toggle = cut.Find("button[aria-label='Show password']");
        toggle.ClassList.Should().Contain("shrink-0");
        toggle.ClassList.Should().NotContain("absolute");
    }

    [Test]
    public async Task PasswordInput_toggles_between_password_and_text()
    {
        var cut = Render<PasswordInput>(parameters => parameters
            .Add(p => p.Value, "secret"));

        await cut.Find("button[aria-label='Show password']").ClickAsync(new MouseEventArgs());
        cut.Find("input").GetAttribute("type").Should().BeNull();
        cut.Find("button[aria-label='Hide password']").Should().NotBeNull();

        await cut.Find("button[aria-label='Hide password']").ClickAsync(new MouseEventArgs());
        cut.Find("input").GetAttribute("type").Should().Be("password");
    }

    [Test]
    public void PasswordInput_disables_toggle_when_empty_by_default()
    {
        var cut = Render<PasswordInput>(parameters => parameters
            .Add(p => p.Value, string.Empty));

        cut.Find("button[aria-label='Show password']")
            .HasAttribute("disabled")
            .Should()
            .BeTrue();
    }
}
