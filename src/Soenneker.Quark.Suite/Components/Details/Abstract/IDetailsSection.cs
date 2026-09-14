using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the details section component.
/// </summary>
public interface IDetailsSection
{
    /// <summary>
    /// Presets applied to the component parts.
    /// </summary>
    DetailsSectionAppearance Appearance { get; set; }

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets flat.
    /// </summary>
    bool Flat { get; set; }

    /// <summary>
    /// Content rendered inside the component.
    /// </summary>
    RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the root element.
    /// </summary>
    Dictionary<string, object>? AdditionalAttributes { get; set; }
}
