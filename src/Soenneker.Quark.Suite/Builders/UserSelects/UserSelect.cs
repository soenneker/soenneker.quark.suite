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
}
