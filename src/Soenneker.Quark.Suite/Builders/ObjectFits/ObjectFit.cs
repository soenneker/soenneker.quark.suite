namespace Soenneker.Quark;

/// <summary>
/// Simplified object-fit utility with fluent API and Tailwind/shadcn-aligned fluent API.
/// </summary>
public static class ObjectFit
{
    /// <summary>
    /// object-fit: contain.
    /// </summary>
    public static ObjectFitBuilder Contain => new(ObjectFitKeyword.ContainValue);

    /// <summary>
    /// object-fit: cover.
    /// </summary>
    public static ObjectFitBuilder Cover => new(ObjectFitKeyword.CoverValue);

    /// <summary>
    /// object-fit: fill.
    /// </summary>
    public static ObjectFitBuilder Fill => new(ObjectFitKeyword.FillValue);

    /// <summary>
    /// object-fit: scale-down.
    /// </summary>
    public static ObjectFitBuilder ScaleDown => new(ObjectFitKeyword.ScaleDownValue);

    /// <summary>
    /// object-fit: none.
    /// </summary>
    public static ObjectFitBuilder None => new(ObjectFitKeyword.NoneValue);
}
