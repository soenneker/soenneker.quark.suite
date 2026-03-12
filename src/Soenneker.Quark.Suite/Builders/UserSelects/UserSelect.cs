using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating user select builders with predefined values.
/// </summary>
public static class UserSelect
{
    /// <summary>
    /// Gets a user select builder with none value (text cannot be selected).
    /// </summary>
    public static UserSelectBuilder None => new(UserSelectKeyword.NoneValue);
    /// <summary>
    /// Gets a user select builder with auto value (browser default selection behavior).
    /// </summary>
    public static UserSelectBuilder Auto => new(UserSelectKeyword.AutoValue);
    /// <summary>
    /// Gets a user select builder with all value (clicking selects all text).
    /// </summary>
    public static UserSelectBuilder All => new(UserSelectKeyword.AllValue);

    /// <summary>
    /// Gets a user select builder with inherit keyword.
    /// </summary>
    public static UserSelectBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a user select builder with initial keyword.
    /// </summary>
    public static UserSelectBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a user select builder with revert keyword.
    /// </summary>
    public static UserSelectBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a user select builder with revert-layer keyword.
    /// </summary>
    public static UserSelectBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a user select builder with unset keyword.
    /// </summary>
    public static UserSelectBuilder Unset => new(GlobalKeyword.UnsetValue);
}
