using AwesomeAssertions;
using Soenneker.Quark.Suite.Demo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed class RouteAuditParityTests
{
    private static readonly IReadOnlyDictionary<string, string> _shadcnBackedRoutes = new Dictionary<string, string>(StringComparer.Ordinal)
    {
        ["accordions"] = "docs/components/base/accordion/index.html",
        ["alerts"] = "docs/components/base/alert/index.html",
        ["alert-dialogs"] = "docs/components/base/alert-dialog/index.html",
        ["anchors"] = "docs/components/base/typography/index.html",
        ["aspect-ratios"] = "docs/components/base/aspect-ratio/index.html",
        ["avatars"] = "docs/components/base/avatar/index.html",
        ["badges"] = "docs/components/base/badge/index.html",
        ["blockquotes"] = "docs/components/base/typography/index.html",
        ["breadcrumbs"] = "docs/components/base/breadcrumb/index.html",
        ["button-groups"] = "docs/components/base/button-group/index.html",
        ["buttons"] = "docs/components/base/button/index.html",
        ["calendars"] = "docs/components/base/calendar/index.html",
        ["cards"] = "docs/components/base/card/index.html",
        ["carousels"] = "docs/components/base/carousel/index.html",
        ["checks"] = "docs/components/base/checkbox/index.html",
        ["codes"] = "docs/components/base/typography/index.html",
        ["collapsibles"] = "docs/components/base/collapsible/index.html",
        ["comboboxes"] = "docs/components/base/combobox/index.html",
        ["commands"] = "docs/components/base/command/index.html",
        ["context-menus"] = "docs/components/base/context-menu/index.html",
        ["datepickers"] = "docs/components/base/date-picker/index.html",
        ["dialogs"] = "docs/components/base/dialog/index.html",
        ["directions"] = "docs/components/base/direction/index.html",
        ["drawers"] = "docs/components/base/drawer/index.html",
        ["dropdowns"] = "docs/components/base/dropdown-menu/index.html",
        ["empties"] = "docs/components/base/empty/index.html",
        ["fields"] = "docs/components/base/field/index.html",
        ["hovercards"] = "docs/components/base/hover-card/index.html",
        ["inputs"] = "docs/components/base/input/index.html",
        ["input-groups"] = "docs/components/base/input-group/index.html",
        ["input-otp"] = "docs/components/base/input-otp/index.html",
        ["items"] = "docs/components/base/item/index.html",
        ["kbd"] = "docs/components/base/kbd/index.html",
        ["labels"] = "docs/components/base/label/index.html",
        ["menubars"] = "docs/components/base/menubar/index.html",
        ["native-selects"] = "docs/components/base/native-select/index.html",
        ["navigationmenus"] = "docs/components/base/navigation-menu/index.html",
        ["paginations"] = "docs/components/base/pagination/index.html",
        ["paragraphs"] = "docs/components/base/typography/index.html",
        ["popovers"] = "docs/components/base/popover/index.html",
        ["progresses"] = "docs/components/base/progress/index.html",
        ["radiogroups"] = "docs/components/base/radio-group/index.html",
        ["resizables"] = "docs/components/base/resizable/index.html",
        ["scroll-area"] = "docs/components/base/scroll-area/index.html",
        ["selects"] = "docs/components/base/select/index.html",
        ["separators"] = "docs/components/base/separator/index.html",
        ["sheets"] = "docs/components/base/sheet/index.html",
        ["sidebars"] = "docs/components/base/sidebar/index.html",
        ["skeletons"] = "docs/components/base/skeleton/index.html",
        ["sliders"] = "docs/components/base/slider/index.html",
        ["small"] = "docs/components/base/typography/index.html",
        ["sonner"] = "docs/components/base/sonner/index.html",
        ["spinners"] = "docs/components/base/spinner/index.html",
        ["strong"] = "docs/components/base/typography/index.html",
        ["switches"] = "docs/components/base/switch/index.html",
        ["tablesbasic"] = "docs/components/base/table/index.html",
        ["tabs"] = "docs/components/base/tabs/index.html",
        ["textareas"] = "docs/components/base/textarea/index.html",
        ["texts"] = "docs/components/base/typography/index.html",
        ["toggles"] = "docs/components/base/toggle/index.html",
        ["toggle-groups"] = "docs/components/base/toggle-group/index.html",
        ["tooltips"] = "docs/components/base/tooltip/index.html",
        ["typography"] = "docs/components/base/typography/index.html"
    };

    private static readonly IReadOnlySet<string> _intentionalQuarkOnlyRoutes = new HashSet<string>(StringComparer.Ordinal)
    {
        "br",
        "container",
        "attachments",
        "currency-inputs",
        "divs",
        "fieldsets",
        "figures",
        "grids",
        "headers",
        "icons",
        "margins",
        "model-selectors",
        "padding",
        "prompt-inputs",
        "sections",
        "semantic-html",
        "sortables",
        "spans",
        "stack",
        "steps",
        "suggestions",
        "threads",
        "timelines",
        "validation"
    };

    [Test]
    public void Public_routes_are_all_explicitly_audited()
    {
        string[] publicRoutes = DocsNavigation.ComponentLinks
            .Concat(DocsNavigation.PrimitiveLinks)
            .Concat(DocsNavigation.SectionLinks.Where(item => item.Href is "validation"))
            .Select(item => item.Href)
            .Where(static href => !string.IsNullOrWhiteSpace(href))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(static href => href, StringComparer.Ordinal)
            .ToArray()!;

        var auditedRoutes = _shadcnBackedRoutes.Keys
                                               .Concat(_intentionalQuarkOnlyRoutes)
                                               .OrderBy(static route => route, StringComparer.Ordinal)
                                               .ToArray();

        auditedRoutes.Except(publicRoutes, StringComparer.Ordinal).Should().BeEmpty();
        publicRoutes.Except(auditedRoutes, StringComparer.Ordinal).Should().BeEmpty();
    }

    [Test]
    public void Audited_public_routes_exist_in_the_app()
    {
        var pagesRoot = Path.Combine(GetSuiteRoot(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components");

        var discoveredRoutes = Directory.GetFiles(pagesRoot, "*.razor", SearchOption.AllDirectories)
                                        .SelectMany(GetRoutes)
                                        .Where(static route => !string.IsNullOrWhiteSpace(route))
                                        .Distinct(StringComparer.Ordinal)
                                        .OrderBy(static route => route, StringComparer.Ordinal)
                                        .ToArray();

        string[] publicRoutes = DocsNavigation.ComponentLinks
            .Concat(DocsNavigation.PrimitiveLinks)
            .Concat(DocsNavigation.SectionLinks.Where(item => item.Href is "validation"))
            .Select(item => item.Href)
            .Where(static href => !string.IsNullOrWhiteSpace(href))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(static href => href, StringComparer.Ordinal)
            .ToArray()!;

        foreach (var route in publicRoutes)
        {
            discoveredRoutes.Should().Contain(route);
        }
    }

    private static IEnumerable<string> GetRoutes(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var trimmed = line.Trim();

            if (!trimmed.StartsWith("@page \"", StringComparison.Ordinal))
                continue;

            var start = "@page \"".Length;
            var end = trimmed.IndexOf('"', start);

            if (end <= start)
                continue;

            var route = trimmed[start..end].TrimStart('/');

            if (route.Length > 0)
                yield return route;
        }
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
