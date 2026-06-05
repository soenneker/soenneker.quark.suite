namespace Soenneker.Quark;

/// <summary>
/// Represents the quark preset definition.
/// </summary>
public sealed class QuarkPresetDefinition
{
    public QuarkPresetDefinition(QuarkPresetToken token)
    {
        Token = token;
    }

    /// <summary>
    /// Gets token.
    /// </summary>
    public QuarkPresetToken Token { get; }

    /// <summary>
    /// Gets or sets name.
    /// </summary>
    public string Name => Token.Name;
}
