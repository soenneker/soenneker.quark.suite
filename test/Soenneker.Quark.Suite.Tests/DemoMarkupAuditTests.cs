using AwesomeAssertions;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed class DemoMarkupAuditTests
{
    private static readonly Regex _disallowedRawTagRegex = new(
        @"<(div|span|nav|footer|img|input|textarea|blockquote|table|code|pre|ul|li|h4)\b",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    [Fact]
    public void Demo_component_pages_do_not_use_disallowed_raw_markup_primitives()
    {
        string demoPagesRoot = Path.Combine(GetSuiteRoot(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components");

        string[] offenders = Directory.GetFiles(demoPagesRoot, "*.razor", SearchOption.AllDirectories)
            .Where(file =>
            {
                string contents = File.ReadAllText(file);
                return _disallowedRawTagRegex.IsMatch(contents);
            })
            .Select(static file => Path.GetFileName(file)!)
            .OrderBy(static file => file, StringComparer.Ordinal)
            .ToArray();

        offenders.Should().BeEmpty();
    }

    private static string GetSuiteRoot()
    {
        string directory = AppContext.BaseDirectory;

        for (int i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
