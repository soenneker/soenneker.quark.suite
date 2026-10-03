namespace Soenneker.Quark;

/// <summary>The position returned by the floating window JavaScript module.</summary>
internal readonly struct FloatingWindowPosition
{
    /// <summary>Gets or sets the horizontal position.</summary>
    public int X { get; init; }

    /// <summary>Gets or sets the vertical position.</summary>
    public int Y { get; init; }
}
