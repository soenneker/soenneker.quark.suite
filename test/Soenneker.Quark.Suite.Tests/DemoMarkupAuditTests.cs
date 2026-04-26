using AwesomeAssertions;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Soenneker.Quark.Suite.Tests;

public sealed class DemoMarkupAuditTests
{
    private static readonly Regex _disallowedRawTagRegex = new(
        @"<(div|span|nav|footer|img|input|textarea|blockquote|table|code|pre|ul|li|h4)\b",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    [Test]
    public void Demo_component_pages_do_not_use_disallowed_raw_markup_primitives()
    {
        var demoPagesRoot = Path.Combine(GetSuiteRoot(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components");

        var offenders = Directory.GetFiles(demoPagesRoot, "*.razor", SearchOption.AllDirectories)
                                 .Where(file => !string.Equals(Path.GetFileName(file), "Typography.razor", StringComparison.Ordinal))
                                 .Where(file =>
                                 {
                                     var contents = File.ReadAllText(file);
                                     return _disallowedRawTagRegex.IsMatch(contents);
                                 })
                                 .Select(static file => Path.GetFileName(file)!)
                                 .OrderBy(static file => file, StringComparer.Ordinal)
                                 .ToArray();

        offenders.Should().BeEmpty();
    }

    private static string GetSuiteRoot()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
