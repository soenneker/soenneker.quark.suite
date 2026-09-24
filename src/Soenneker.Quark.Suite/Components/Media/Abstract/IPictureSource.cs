using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>A native picture source with responsive candidates and optional Quark theme selection.</summary>
public interface IPictureSource : IComponent
{
    /// <summary>Image URL used directly, or as the filename for automatic width candidates.</summary>
    string? Source { get; set; }

    /// <summary>Optional replacement extension; defaults to avif with AutoSrcSet. Preserves URL queries and fragments.</summary>
    string? Extension { get; set; }

    /// <summary>Explicit source candidates. Overrides automatic generation.</summary>
    string? SrcSet { get; set; }

    /// <summary>Generate width variants using Image conventions. Callers must supply existing assets.</summary>
    bool AutoSrcSet { get; set; }

    /// <summary>Positive variant widths, defaulting to 480, 960 and 1440. Duplicates are omitted.</summary>
    IReadOnlyList<int> SrcSetWidths { get; set; }

    /// <summary>Rendered image width for candidate selection. Defaults to 100vw with automatic candidates.</summary>
    string? Sizes { get; set; }

    /// <summary>Media condition for this source, such as (max-width: 639px).</summary>
    string? Media { get; set; }

    /// <summary>Image MIME type, such as image/avif.</summary>
    string? Type { get; set; }

    /// <summary>Original image width. With AutoSrcSet, includes the original URL and omits variants at or above this width.</summary>
    int? IntrinsicWidth { get; set; }

    /// <summary>Original image height, used with IntrinsicWidth to reserve the correct aspect ratio.</summary>
    int? IntrinsicHeight { get; set; }

    /// <summary>Optional light or dark theme condition. System or null imposes no theme restriction. Native color-scheme queries work before startup; load js/picture.js before parsing images to honor the Quark root theme in static snapshots.</summary>
    ThemeMode? Theme { get; set; }

}
