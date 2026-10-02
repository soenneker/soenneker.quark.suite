using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    private const string ConsoleExceptionText = "Job failed\r\nSystem.InvalidOperationException: <unsafe>\r\n   at Example.Run() in C:\\src\\Example.cs:line 12\r\n ---> System.Exception: Inner failure\r\n   at Example.Inner()\r\n   --- End of inner exception stack trace ---";

    [Test]
    public void Console_exception_formatting_toggles_without_losing_text()
    {
        var cut = Render<ConsoleLogEntry>(p => p.Add(c => c.Text, ConsoleExceptionText));
        cut.FindAll("details").Should().BeEmpty();
        cut.Find(".console-message").TextContent.Should().Be(ConsoleExceptionText.Replace("\r\n", "\n"));

        cut.Render(p => p.Add(c => c.FormatExceptions, true));
        cut.Find("details").HasAttribute("open").Should().BeFalse();
        cut.FindAll("unsafe").Should().BeEmpty();
        cut.Find(".console-location").TextContent.Should().Contain("C:\\src\\Example.cs:line 12");
        cut.Find(".console-trace").TextContent.Should().Contain("Inner failure").And.Contain("End of inner exception stack trace");

        cut.Render(p => p.Add(c => c.FormatExceptions, false));
        cut.FindAll("details").Should().BeEmpty();
        cut.Find(".console-message").TextContent.Should().Be(ConsoleExceptionText.Replace("\r\n", "\n"));
    }

    [Test]
    public void Console_exception_formatting_leaves_ordinary_multiline_logs_alone()
    {
        const string text = "Working\n   at home\nFinished";
        var cut = Render<ConsoleLogEntry>(p => p.Add(c => c.Text, text).Add(c => c.FormatExceptions, true));
        cut.FindAll("details").Should().BeEmpty();
        cut.Find(".console-message").TextContent.Should().Be(text);
    }

    [Test]
    public void Console_exception_formatting_inherits_and_observes_panel_preference()
    {
        Services.AddQuarkConsolePanelAsScoped();
        var cut = Render<ConsolePanel>(p => p.Add(c => c.ShowCopy, false).Add(c => c.ShowDownload, false)
            .Add(c => c.AutoScroll, false).Add(c => c.FormatExceptions, true)
            .AddChildContent<ConsoleLogEntry>(entry => entry.Add(c => c.Text, ConsoleExceptionText)));
        cut.FindAll("details").Should().HaveCount(1);
        cut.Render(p => p.Add(c => c.FormatExceptions, false));
        cut.FindAll("details").Should().BeEmpty();
    }
    [Test]
    public void Console_json_formats_escaped_payload_and_preserves_context()
    {
        const string text = """
[Debug] Request: "{\"name\":\"<unsafe>\",\"items\":[1,null,true]}" LeadId: "abc"
""";
        var cut = Render<ConsoleLogEntry>(p => p.Add(c => c.Text, text));
        cut.Find(".console-message").TextContent.Should().Be(text);
        cut.Render(p => p.Add(c => c.FormatJson, true));
        string formatted = cut.Find(".console-message").TextContent;
        formatted.Should().Be("[Debug] Request: { … } LeadId: \"abc\"");
        cut.Find("details").HasAttribute("open").Should().BeFalse();
        cut.Find(".console-json-payload").TextContent.Should().Contain("\"items\": [");
        cut.FindAll(".json-node").Should().HaveCount(2);
        cut.Find(".json-children .json-node").HasAttribute("open").Should().BeTrue();
        cut.FindAll("unsafe").Should().BeEmpty();
        cut.Render(p => p.Add(c => c.FormatJson, false));
        cut.Find(".console-message").TextContent.Should().Be(text);
    }

    [Test]
    public void Console_json_preserves_invalid_payloads_and_formats_multiple_containers()
    {
        const string text = """[Information] {invalid} {"nested":{"value":"a } [ b"}} then [1,2]""";
        var cut = Render<ConsoleLogEntry>(p => p.Add(c => c.Text, text).Add(c => c.FormatJson, true));
        cut.Find(".console-message").TextContent.Should().Be("[Information] {invalid} { … } then [ … ]");
        cut.FindAll(".console-json-payload").Should().HaveCount(2);
        cut.FindAll(".console-json-payload")[0].TextContent.Should().Contain("a } [ b");
        cut.FindAll(".console-json-payload")[1].QuerySelectorAll(".json-leaf").Should().HaveCount(2);
        const string malformed = """Request: "{\"broken\":}" [Debug] done""";
        cut.Render(p => p.Add(c => c.Text, malformed));
        cut.Find(".console-message").TextContent.Should().Be(malformed);
    }

    [Test]
    public void Console_json_inherits_panel_setting_and_entry_can_override()
    {
        Services.AddQuarkConsolePanelAsScoped();
        var cut = Render<ConsolePanel>(p => p.Add(c => c.ShowCopy, false).Add(c => c.ShowDownload, false)
            .Add(c => c.AutoScroll, false).Add(c => c.FormatJson, true)
            .AddChildContent<ConsoleLogEntry>(entry => entry.Add(c => c.Text, "{\"id\":1}")));
        cut.Find(".console-message").TextContent.Should().Be("{ … }");
        cut.Find("details").HasAttribute("open").Should().BeFalse();
        cut.Render(p => p.Add(c => c.FormatJson, false));
        cut.Find(".console-message").TextContent.Should().Be("{\"id\":1}");
        cut.Render(p => p.Add(c => c.FormatJson, true)
            .AddChildContent<ConsoleLogEntry>(entry => entry.Add(c => c.Text, "{\"id\":1}").Add(c => c.FormatJson, false)));
        cut.Find(".console-message").TextContent.Should().Be("{\"id\":1}");
    }
    [Test]
    public async System.Threading.Tasks.Task Console_wrapping_can_be_toggled_or_fixed_by_configuration()
    {
        var cut = Render<ConsoleLogEntry>(p => p.Add(c => c.Text, ConsoleExceptionText).Add(c => c.FormatExceptions, true));
        cut.Find(".console-trace").ClassList.Should().Contain("console-wrap");
        await cut.InvokeAsync(() => cut.FindComponent<Check>().Instance.CheckedChanged.InvokeAsync(false));
        cut.Find(".console-trace").ClassList.Should().NotContain("console-wrap");
        cut.Render(p => p.Add(c => c.ShowWrapToggle, false));
        cut.FindAll(".console-tools").Should().BeEmpty();
        cut.Find(".console-trace").ClassList.Should().Contain("console-wrap");
        cut.Render(p => p.Add(c => c.WrapLines, false));
        cut.Find(".console-trace").ClassList.Should().NotContain("console-wrap");
    }

    [Test]
    public void Console_wrapping_inherits_panel_configuration_and_allows_entry_overrides()
    {
        Services.AddQuarkConsolePanelAsScoped();
        var cut = Render<ConsolePanel>(p => p.Add(c => c.ShowCopy, false).Add(c => c.ShowDownload, false)
            .Add(c => c.AutoScroll, false).Add(c => c.FormatJson, true)
            .Add(c => c.WrapLines, false).Add(c => c.ShowWrapToggle, false)
            .AddChildContent<ConsoleLogEntry>(entry => entry.Add(c => c.Text, "{}")));
        cut.FindAll(".console-tools").Should().BeEmpty();
        cut.Find(".console-json-payload").ClassList.Should().NotContain("console-wrap");
        cut.Render(p => p.Add(c => c.WrapLines, true));
        cut.Find(".console-json-payload").ClassList.Should().Contain("console-wrap");
        cut.Render(p => p.AddChildContent<ConsoleLogEntry>(entry => entry.Add(c => c.Text, "{}")
            .Add(c => c.WrapLines, false).Add(c => c.ShowWrapToggle, true)));
        cut.FindAll(".console-tools").Should().HaveCount(1);
        cut.Find(".console-json-payload").ClassList.Should().NotContain("console-wrap");
    }
}
