namespace Soenneker.Quark;

public sealed class QuarkPresetDefinition
{
    public QuarkPresetDefinition(QuarkPresetToken token)
    {
        Token = token;
    }

    public QuarkPresetToken Token { get; }

    public string Name => Token.Name;
}
