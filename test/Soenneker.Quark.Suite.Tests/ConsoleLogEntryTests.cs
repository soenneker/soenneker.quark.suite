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
}
