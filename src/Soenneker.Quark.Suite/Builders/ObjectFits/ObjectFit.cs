using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Simplified object-fit utility with fluent API and Bootstrap-first approach.
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

    /// <summary>
    /// Gets an object fit builder with inherit keyword.
    /// </summary>
    public static ObjectFitBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets an object fit builder with initial keyword.
    /// </summary>
    public static ObjectFitBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets an object fit builder with revert keyword.
    /// </summary>
    public static ObjectFitBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets an object fit builder with revert-layer keyword.
    /// </summary>
    public static ObjectFitBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets an object fit builder with unset keyword.
    /// </summary>
    public static ObjectFitBuilder Unset => new(GlobalKeyword.UnsetValue);
}
