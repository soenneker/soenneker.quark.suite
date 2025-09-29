namespace Soenneker.Quark.Builders.Interactions;

/// <summary>
/// Simplified interaction utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Interaction
{
    /// <summary>
    /// No user selection and no pointer events.
    /// </summary>
    public static InteractionBuilder None => new("none", "none");

    /// <summary>
    /// Allow user selection and pointer events.
    /// </summary>
    public static InteractionBuilder All => new("auto", "auto");

    /// <summary>
    /// No user selection but allow pointer events.
    /// </summary>
    public static InteractionBuilder NoSelect => new("none", "auto");

    /// <summary>
    /// Allow user selection but no pointer events.
    /// </summary>
    public static InteractionBuilder NoPointer => new("auto", "none");

    /// <summary>
    /// Text selection only.
    /// </summary>
    public static InteractionBuilder Text => new("text", "auto");

    /// <summary>
    /// All text selection.
    /// </summary>
    public static InteractionBuilder AllText => new("all", "auto");
}
