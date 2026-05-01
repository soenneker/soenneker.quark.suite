namespace Soenneker.Quark;

internal sealed class InputOtpContext
{
    public string Value { get; set; } = string.Empty;

    public int Length { get; set; }

    public int ActiveIndex { get; set; }

    public bool Focused { get; set; }

    public char? GetCharacter(int index)
    {
        return index >= 0 && index < Value.Length ? Value[index] : null;
    }

    public bool IsActive(int index)
    {
        return Focused && index == ActiveIndex;
    }
}
