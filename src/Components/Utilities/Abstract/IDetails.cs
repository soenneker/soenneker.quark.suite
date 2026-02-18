using System;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a details element for creating disclosure widgets.
/// </summary>
public interface IDetails : IElement
{
    /// <summary>
    /// Gets or sets whether the details element is open (expanded).
    /// </summary>
    bool Open { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the open state changes.
    /// </summary>
    EventCallback<EventArgs> OnToggle { get; set; }
}

