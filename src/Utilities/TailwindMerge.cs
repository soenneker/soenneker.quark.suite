using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Soenneker.Quark.Utilities;

/// <summary>
/// Provides Tailwind CSS class conflict resolution logic.
/// Intelligently merges Tailwind utility classes by identifying conflicts
/// and keeping only the last occurrence of conflicting utilities.
/// </summary>
public static class TailwindMerge
{
    private static readonly Dictionary<string, string> TailwindGroups = new()
    {
        ["p"] = "padding", ["px"] = "padding-x", ["py"] = "padding-y",
        ["pt"] = "padding-top", ["pr"] = "padding-right", ["pb"] = "padding-bottom", ["pl"] = "padding-left",
        ["m"] = "margin", ["mx"] = "margin-x", ["my"] = "margin-y",
        ["mt"] = "margin-top", ["mr"] = "margin-right", ["mb"] = "margin-bottom", ["ml"] = "margin-left",
        ["w"] = "width", ["min-w"] = "min-width", ["max-w"] = "max-width",
        ["h"] = "height", ["min-h"] = "min-height", ["max-h"] = "max-height",
        ["text-xs"] = "font-size", ["text-sm"] = "font-size", ["text-base"] = "font-size",
        ["text-lg"] = "font-size", ["text-xl"] = "font-size", ["text-2xl"] = "font-size",
        ["text-3xl"] = "font-size", ["text-4xl"] = "font-size", ["text-5xl"] = "font-size",
        ["text-6xl"] = "font-size", ["text-7xl"] = "font-size", ["text-8xl"] = "font-size", ["text-9xl"] = "font-size",
        ["font-thin"] = "font-weight", ["font-extralight"] = "font-weight", ["font-light"] = "font-weight",
        ["font-normal"] = "font-weight", ["font-medium"] = "font-weight", ["font-semibold"] = "font-weight",
        ["font-bold"] = "font-weight", ["font-extrabold"] = "font-weight", ["font-black"] = "font-weight",
        ["block"] = "display", ["inline-block"] = "display", ["inline"] = "display",
        ["flex"] = "display", ["inline-flex"] = "display", ["grid"] = "display", ["inline-grid"] = "display", ["hidden"] = "display",
        ["static"] = "position", ["fixed"] = "position", ["absolute"] = "position", ["relative"] = "position", ["sticky"] = "position",
        ["gap"] = "gap", ["gap-x"] = "gap-x", ["gap-y"] = "gap-y",
        ["flex-row"] = "flex-direction", ["flex-row-reverse"] = "flex-direction", ["flex-col"] = "flex-direction", ["flex-col-reverse"] = "flex-direction",
        ["justify-start"] = "justify-content", ["justify-end"] = "justify-content", ["justify-center"] = "justify-content",
        ["justify-between"] = "justify-content", ["justify-around"] = "justify-content", ["justify-evenly"] = "justify-content",
        ["items-start"] = "align-items", ["items-end"] = "align-items", ["items-center"] = "align-items", ["items-baseline"] = "align-items", ["items-stretch"] = "align-items",
        ["border-solid"] = "border-style", ["border-dashed"] = "border-style", ["border-dotted"] = "border-style", ["border-double"] = "border-style", ["border-none"] = "border-style",
        ["rounded-none"] = "border-radius", ["rounded-sm"] = "border-radius", ["rounded"] = "border-radius",
        ["rounded-md"] = "border-radius", ["rounded-lg"] = "border-radius", ["rounded-xl"] = "border-radius",
        ["rounded-2xl"] = "border-radius", ["rounded-3xl"] = "border-radius", ["rounded-full"] = "border-radius",
        ["opacity"] = "opacity", ["z"] = "z-index",
        ["cursor-default"] = "cursor", ["cursor-pointer"] = "cursor", ["cursor-not-allowed"] = "cursor",
        ["cursor-crosshair"] = "cursor", ["cursor-grab"] = "cursor", ["cursor-grabbing"] = "cursor",
        ["cursor-col-resize"] = "cursor", ["cursor-row-resize"] = "cursor", ["cursor-e-resize"] = "cursor", ["cursor-w-resize"] = "cursor",
        ["cursor-wait"] = "cursor", ["cursor-text"] = "cursor", ["cursor-move"] = "cursor", ["cursor-help"] = "cursor", ["cursor-none"] = "cursor", ["cursor-auto"] = "cursor",
        ["pointer-events-none"] = "pointer-events", ["pointer-events-auto"] = "pointer-events",
    };

    private static readonly Regex SpacingRegex = new(@"^(p|px|py|pt|pr|pb|pl|m|mx|my|mt|mr|mb|ml)-(\d+\.?\d*|auto)$", RegexOptions.Compiled);
    private static readonly Regex SizingRegex = new(@"^(w|h|min-w|min-h|max-w|max-h)-(.+)$", RegexOptions.Compiled);
    private static readonly Regex GapRegex = new(@"^(gap|gap-x|gap-y)-(\d+\.?\d*)$", RegexOptions.Compiled);
    private static readonly Regex TextColorRegex = new(@"^text-([a-z]+)(?:-(\d+))?$", RegexOptions.Compiled);
    private static readonly Regex BgColorRegex = new(@"^bg-([a-z]+)(?:-(\d+))?$", RegexOptions.Compiled);
    private static readonly Regex BorderColorRegex = new(@"^border-(?!l-|r-|t-|b-|x-|y-)([a-z]+)(?:-(\d+))?$", RegexOptions.Compiled);
    private static readonly Regex BorderWidthRegex = new(@"^border(-\d+)?$", RegexOptions.Compiled);
    private static readonly Regex BorderSideWidthRegex = new(@"^border-([lrtbxy])-(\d+)$", RegexOptions.Compiled);
    private static readonly Regex OpacityRegex = new(@"^opacity-(\d+)$", RegexOptions.Compiled);
    private static readonly Regex ZIndexRegex = new(@"^z-(\d+|auto)$", RegexOptions.Compiled);
    private static readonly Regex GridColsRegex = new(@"^grid-cols-(\d+|none)$", RegexOptions.Compiled);
    private static readonly Regex GridRowsRegex = new(@"^grid-rows-(\d+|none)$", RegexOptions.Compiled);

    private static readonly ConcurrentDictionary<string, string?> _utilityGroupCache = new();
    private static readonly Regex ValidClassNameRegex = new(@"^[a-zA-Z0-9_\-:/.[\]()%!@#&>+~=]+$", RegexOptions.Compiled);

    private static bool IsValidClassName(string className)
    {
        if (string.IsNullOrWhiteSpace(className) || className.Length > 200) return false;
        if (className.Contains("expression", StringComparison.OrdinalIgnoreCase) ||
            className.Contains("javascript", StringComparison.OrdinalIgnoreCase) ||
            className.Contains("url(", StringComparison.OrdinalIgnoreCase) ||
            className.Contains("import", StringComparison.OrdinalIgnoreCase))
            return false;
        return ValidClassNameRegex.IsMatch(className);
    }

    /// <summary>
    /// Merges an array of CSS class strings, resolving Tailwind utility conflicts.
    /// Later classes in the array take precedence over earlier ones.
    /// </summary>
    public static string Merge(string[] classes)
    {
        if (classes == null || classes.Length == 0) return string.Empty;

        var groupedClasses = new Dictionary<string, (string className, int index)>();
        var unGroupedClasses = new List<(string className, int index)>();

        for (var i = 0; i < classes.Length; i++)
        {
            var className = classes[i];
            if (string.IsNullOrWhiteSpace(className)) continue;
            if (!IsValidClassName(className)) continue;

            var group = GetUtilityGroup(className);
            if (!string.IsNullOrEmpty(group))
                groupedClasses[group] = (className, i);
            else
                unGroupedClasses.Add((className, i));
        }

        var allClasses = groupedClasses.Values.Concat(unGroupedClasses).OrderBy(x => x.index).Select(x => x.className);
        return string.Join(" ", allClasses);
    }

    private static string? GetUtilityGroup(string className) =>
        _utilityGroupCache.GetOrAdd(className, ComputeUtilityGroup);

    private static string? ComputeUtilityGroup(string className)
    {
        if (TailwindGroups.TryGetValue(className, out var group)) return group;
        var spacingMatch = SpacingRegex.Match(className);
        if (spacingMatch.Success) { var prefix = spacingMatch.Groups[1].Value; return TailwindGroups.TryGetValue(prefix, out var g) ? g : null; }
        var sizingMatch = SizingRegex.Match(className);
        if (sizingMatch.Success) { var prefix = sizingMatch.Groups[1].Value; return TailwindGroups.TryGetValue(prefix, out var g) ? g : null; }
        var gapMatch = GapRegex.Match(className);
        if (gapMatch.Success) { var prefix = gapMatch.Groups[1].Value; return TailwindGroups.TryGetValue(prefix, out var g) ? g : null; }
        if (TextColorRegex.IsMatch(className)) return "text-color";
        if (BgColorRegex.IsMatch(className)) return "background-color";
        if (BorderColorRegex.IsMatch(className)) return "border-color";
        if (BorderWidthRegex.IsMatch(className)) return "border-width";
        var borderSideMatch = BorderSideWidthRegex.Match(className);
        if (borderSideMatch.Success) return $"border-{borderSideMatch.Groups[1].Value}-width";
        if (OpacityRegex.IsMatch(className)) return "opacity";
        if (ZIndexRegex.IsMatch(className)) return "z-index";
        if (GridColsRegex.IsMatch(className)) return "grid-cols";
        if (GridRowsRegex.IsMatch(className)) return "grid-rows";
        return null;
    }
}
