using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the page header component.
/// </summary>
public interface IPageHeader
{
    /// <summary>
    /// Presets applied to the component parts.
    /// </summary>
    PageHeaderAppearance Appearance { get; set; }

    /// <summary>
    /// Gets or sets eyebrow.
    /// </summary>
    string? Eyebrow { get; set; }

    /// <summary>
    /// Gets or sets show eyebrow.
    /// </summary>
    bool ShowEyebrow { get; set; }

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets show title.
    /// </summary>
    bool ShowTitle { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    string? Description { get; set; }

    /// <summary>
    /// Gets or sets description content.
    /// </summary>
    RenderFragment? DescriptionContent { get; set; }

    /// <summary>
    /// Gets or sets show description.
    /// </summary>
    bool ShowDescription { get; set; }

    /// <summary>
    /// Custom action content.
    /// </summary>
    RenderFragment? Actions { get; set; }

    /// <summary>
    /// Gets or sets inline actions.
    /// </summary>
    RenderFragment? InlineActions { get; set; }

    /// <summary>
    /// Gets or sets show actions menu.
    /// </summary>
    bool ShowActionsMenu { get; set; }

    /// <summary>
    /// Additional attributes applied to the root element.
    /// </summary>
    IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
}
