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
    internal Func<bool>? CanScrollPreviousAccessor { get; set; }
    internal Func<bool>? CanScrollNextAccessor { get; set; }
    internal Action? ScrollPreviousAction { get; set; }
    internal Action? ScrollNextAction { get; set; }

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

    /// <summary>
    /// Gets whether the carousel can scroll to the previous slide.
    /// </summary>
    public bool CanScrollPrev()
    {
        return CanScrollPreviousAccessor?.Invoke() ?? false;
    }

    /// <summary>
    /// Gets whether the carousel can scroll to the next slide.
    /// </summary>
    public bool CanScrollNext()
    {
        return CanScrollNextAccessor?.Invoke() ?? false;
    }

    /// <summary>
    /// Scrolls to the previous slide.
    /// </summary>
    public void ScrollPrev()
    {
        ScrollPreviousAction?.Invoke();
    }

    /// <summary>
    /// Scrolls to the next slide.
    /// </summary>
    public void ScrollNext()
    {
        ScrollNextAction?.Invoke();
    }

    internal void NotifySelect()
    {
        Select?.Invoke();
    }
}
