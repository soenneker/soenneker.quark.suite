using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Lightweight carousel state API exposed by <see cref="Carousel"/>.
/// </summary>
public sealed class CarouselApi
{
    internal Func<int>? SelectedIndexAccessor { get; set; }
    internal Func<IReadOnlyList<int>>? ScrollSnapListAccessor { get; set; }

    /// <summary>
    /// Raised when the selected slide changes.
    /// </summary>
    public event Action? Select;

    /// <summary>
    /// Gets the zero-based selected slide index.
    /// </summary>
    public int SelectedScrollSnap()
    {
        return SelectedIndexAccessor?.Invoke() ?? 0;
    }

    /// <summary>
    /// Gets a list of slide indices that can be scrolled to.
    /// </summary>
    public IReadOnlyList<int> ScrollSnapList()
    {
        return ScrollSnapListAccessor?.Invoke() ?? Array.Empty<int>();
    }

    internal void NotifySelect()
    {
        Select?.Invoke();
    }
}
